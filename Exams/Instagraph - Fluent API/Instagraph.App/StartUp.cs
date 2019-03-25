using System;
using System.IO;

using AutoMapper;

using Instagraph.Data;
using Instagraph.DataProcessor;
using System.Text;

namespace Instagraph.App
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            Mapper.Initialize(options => options.AddProfile<InstagraphProfile>());

            //Console.WriteLine(ResetDatabase());

            //Console.WriteLine(ImportData());

            ExportData();
        }

        private static string ImportData()
        {
            StringBuilder builder = new StringBuilder();

            using (var context = new InstagraphContext())
            {
                string picturesJson = File.ReadAllText("../../../files/input/pictures.json");

                builder.AppendLine(Deserializer.ImportPictures(context, picturesJson));

                string usersJson = File.ReadAllText("../../../files/input/users.json");

                builder.AppendLine(Deserializer.ImportUsers(context, usersJson));

                string followersJson = File.ReadAllText("../../../files/input/users_followers.json");

                builder.AppendLine(Deserializer.ImportFollowers(context, followersJson));

                string postsXml = File.ReadAllText("../../../files/input/posts.xml");

                builder.AppendLine(Deserializer.ImportPosts(context, postsXml));

                string commentsXml = File.ReadAllText("../../../files/input/comments.xml");

                builder.AppendLine(Deserializer.ImportComments(context, commentsXml));
            }

            string result = builder.ToString().Trim();
            return result;
        }

        private static void ExportData()
        {
            using (var context = new InstagraphContext())
            {
                //string uncommentedPostsOutput = Serializer.ExportUncommentedPosts(context);

                //File.WriteAllText("../../../files/output/UncommentedPosts.json", uncommentedPostsOutput);

                //string usersOutput = Serializer.ExportPopularUsers(context);

                //File.WriteAllText("../../../files/output/PopularUsers.json", usersOutput);

                string commentsOutput = Serializer.ExportCommentsOnPosts(context);

                File.WriteAllText("../../../files/output/CommentsOnPosts.xml", commentsOutput);
            }
        }

        private static string ResetDatabase()
        {
            using (var context = new InstagraphContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            return $"Database reset succsessfully.";
        }
    }
}
