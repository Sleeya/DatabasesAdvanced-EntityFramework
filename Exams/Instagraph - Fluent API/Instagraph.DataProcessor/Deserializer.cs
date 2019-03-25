using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;

using Newtonsoft.Json;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

using Instagraph.Data;
using Instagraph.Models;
using Instagraph.DataProcessor.Dtos.Import;

namespace Instagraph.DataProcessor
{
    public class Deserializer
    {
        public static string ImportPictures(InstagraphContext context, string jsonString)
        {
            PictureImportDto[] pictureDtos = JsonConvert.DeserializeObject<PictureImportDto[]>(jsonString);

            StringBuilder builder = new StringBuilder();

            List<Picture> pictures = new List<Picture>();

            foreach (var pictureDto in pictureDtos)
            {
                if (pictureDto.Size == null || pictureDto.Size <= 0 || string.IsNullOrWhiteSpace(pictureDto.Path) || context.Pictures.Any(x => x.Path == pictureDto.Path))
                {
                    builder.AppendLine("Error: Invalid data.");
                }
                else
                {
                    builder.AppendLine($"Successfully imported Picture {pictureDto.Path}.");
                    pictures.Add(Mapper.Map<Picture>(pictureDto));
                }
            }

            context.Pictures.AddRange(pictures);
            context.SaveChanges();

            return builder.ToString().Trim();
        }

        public static string ImportUsers(InstagraphContext context, string jsonString)
        {
            UserImportDto[] userDtos = JsonConvert.DeserializeObject<UserImportDto[]>(jsonString);

            StringBuilder builder = new StringBuilder();

            List<User> users = new List<User>();

            foreach (var userDto in userDtos)
            {
                bool invalidUsername = userDto.Username == null || userDto.Username.Length > 30 || context.Users.Any(x=> x.Username == userDto.Username);
                bool invalidPassword = userDto.Password == null || userDto.Password.Length > 20;
                bool invalidPicture = userDto.ProfilePicture == null;

                if (invalidUsername || invalidPassword || invalidPicture)
                {
                    builder.AppendLine("Error: Invalid data.");
                    continue;
                }

                var profilePicture = context.Pictures.FirstOrDefault(p => p.Path == userDto.ProfilePicture);

                if (profilePicture == null)
                {
                    builder.AppendLine("Error: Invalid data.");
                    continue;
                }

                User user = Mapper.Map<User>(userDto);
                user.ProfilePicture = profilePicture;

                users.Add(user);
                builder.AppendLine($"Successfully imported User {user.Username}.");
            }

            context.Users.AddRange(users);
            context.SaveChanges();

            return builder.ToString().Trim();
        }

        public static string ImportFollowers(InstagraphContext context, string jsonString)
        {
            UserFollowerImportDto[] userFollowerDtos = JsonConvert.DeserializeObject<UserFollowerImportDto[]>(jsonString);

            StringBuilder builder = new StringBuilder();

            List<UserFollower> userFollowers = new List<UserFollower>();

            foreach (var ufDto in userFollowerDtos)
            {
                var user = context.Users.FirstOrDefault(x => x.Username == ufDto.User);
                var follower = context.Users.FirstOrDefault(x => x.Username == ufDto.Follower);

                bool alreadyFollowed = userFollowers.Any(x => x.User.Username == ufDto.User && x.Follower.Username == ufDto.Follower)
                    || context.UsersFollowers.Any(x => x.User.Username == ufDto.User && x.Follower.Username == ufDto.Follower);

                if (user == null || follower == null || alreadyFollowed)
                {
                    builder.AppendLine("Error: Invalid data.");
                    continue;
                }

                UserFollower uf = Mapper.Map<UserFollower>(ufDto);
                uf.User = user;
                uf.Follower = follower;

                userFollowers.Add(uf);
                builder.AppendLine($"Successfully imported Follower {ufDto.Follower} to User {ufDto.User}.");
            }

            context.UsersFollowers.AddRange(userFollowers);
            context.SaveChanges();

            return builder.ToString();
        }

        public static string ImportPosts(InstagraphContext context, string xmlString)
        {

            var xDoc = XDocument.Parse(xmlString);
            List<PostImportDto> postDtos = new List<PostImportDto>();

            foreach (var el in xDoc.Root.Elements())
            {
                postDtos.Add(new PostImportDto
                {
                    Caption = el.Element("caption")?.Value,
                    Picture = el.Element("picture").Value,
                    User = el.Element("user").Value
                });
            }

            StringBuilder builder = new StringBuilder();
            List<Post> posts = new List<Post>();

            foreach (var postDto in postDtos)
            {
                var user = context.Users.FirstOrDefault(x => x.Username == postDto.User);
                var picture = context.Pictures.FirstOrDefault(x => x.Path == postDto.Picture);

                if (user == null || picture == null || string.IsNullOrWhiteSpace(postDto.Caption))
                {
                    builder.AppendLine("Error: Invalid data.");
                    continue;
                }

                Post post = Mapper.Map<Post>(postDto);
                post.User = user;
                post.Picture = picture;

                posts.Add(post);

                builder.AppendLine($"Successfully imported Post {postDto.Caption}.");
            }

            context.Posts.AddRange(posts);
            context.SaveChanges();

            return builder.ToString().Trim();
        }

        public static string ImportComments(InstagraphContext context, string xmlString)
        {
            var xDoc = XDocument.Parse(xmlString);

            List<CommentImportDto> commentDtos = new List<CommentImportDto>();

            foreach (var el in xDoc.Root.Elements())
            {
                commentDtos.Add(new CommentImportDto
                {
                    Content = el.Element("content")?.Value,
                    User = el.Element("user")?.Value,
                    PostId = el.Element("post")?.Attribute("id").Value
                });
            }

            StringBuilder builder = new StringBuilder();
            List<Comment> comments = new List<Comment>();

            foreach (var commentDto in commentDtos)
            {
                if (string.IsNullOrWhiteSpace(commentDto.Content) || commentDto.Content.Length > 250 || string.IsNullOrWhiteSpace(commentDto.PostId) || string.IsNullOrWhiteSpace(commentDto.User))
                {
                    builder.AppendLine("Error: Invalid data.");
                    continue;
                }

                var user = context.Users.FirstOrDefault(x => x.Username == commentDto.User);
                var post = context.Posts.FirstOrDefault(x => x.Id == int.Parse(commentDto.PostId));

                if (user == null || post == null || string.IsNullOrWhiteSpace(commentDto.Content))
                {
                    builder.AppendLine("Error: Invalid data.");
                    continue;
                }

                Comment comment = Mapper.Map<Comment>(commentDto);
                comment.User = user;
                comment.Post = post;

                comments.Add(comment);

                builder.AppendLine($"Successfully imported Comment {commentDto.Content}.");
            }

            context.Comments.AddRange(comments);
            context.SaveChanges();

            return builder.ToString().Trim();
        }
    }
}
