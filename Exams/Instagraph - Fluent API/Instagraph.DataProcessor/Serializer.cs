using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Instagraph.Data;
using Instagraph.DataProcessor.Dtos.Export;
using Instagraph.Models;
using Newtonsoft.Json;

namespace Instagraph.DataProcessor
{
    public class Serializer
    {
        public static string ExportUncommentedPosts(InstagraphContext context)
        {
            var posts = context.Posts
                .Where(p => p.Comments.Count() == 0)
                .OrderBy(p => p.Id)
                .Select(p => new
                {
                    p.Id,
                    Picture = p.Picture.Path,
                    User = p.User.Username
                })
                .ToArray();

            var jsonOutput = JsonConvert.SerializeObject(posts, Formatting.Indented);

            return jsonOutput;
        }

        public static string ExportPopularUsers(InstagraphContext context)
        {
            var users = context.Users
                .Where(u => u.Posts
                                .Any(p => p.Comments
                                    .Any(c => u.Followers
                                        .Any(f => f.FollowerId == c.UserId))))
                .OrderBy(u => u.Id)
                .ToArray();

            string jsonOutput = JsonConvert.SerializeObject(users, Formatting.Indented);

            return jsonOutput;
        }

        public static string ExportCommentsOnPosts(InstagraphContext context)
        {
            var users = context.Users
                .Select(u => new
                {
                    u.Username,
                    Posts = u.Posts.Select(p => p.Comments.Count).ToArray()
                })
                .ToArray();

            var validUsers = new List<UserTopCommentsDto>();

            foreach (var user in users)
            {
                int mostComments = 0;

                if (user.Posts.Any())
                {
                    mostComments = user.Posts.OrderByDescending(x => x).First();
                }

                UserTopCommentsDto userDto = new UserTopCommentsDto
                {
                    Username = user.Username,
                    MostComments = mostComments
                };

                validUsers.Add(userDto);
            }

            var xDoc = new XDocument();

            xDoc.Add(new XElement("users"));

            foreach (var user in validUsers.OrderByDescending(u => u.MostComments).ThenBy(u => u.Username))
            {
                xDoc.Root.Add(new XElement("user", new XElement("Username", user.Username), new XElement("MostComments", user.MostComments)));
            }

            string xmlOutput = xDoc.ToString();

            return xmlOutput;
        }
    }
}
