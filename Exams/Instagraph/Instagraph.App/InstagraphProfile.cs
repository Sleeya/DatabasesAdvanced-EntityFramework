using AutoMapper;
using Instagraph.DataProcessor.Dtos.Import;
using Instagraph.Models;

namespace Instagraph.App
{
    public class InstagraphProfile : Profile
    {
        public InstagraphProfile()
        {
            CreateMap<PictureImportDto, Picture>();

            CreateMap<UserImportDto, User>()
                .ForMember(x=> x.ProfilePicture, y=> y.UseValue<Picture>(null));

            CreateMap<UserFollowerImportDto, UserFollower>()
                .ForMember(x => x.User, y => y.UseValue<User>(null))
                .ForMember(x => x.Follower, y => y.UseValue<User>(null));

            CreateMap<PostImportDto, Post>()
               .ForMember(x => x.User, y => y.UseValue<User>(null))
               .ForMember(x => x.Picture, y => y.UseValue<Picture>(null));

            CreateMap<CommentImportDto, Comment>()
               .ForMember(x => x.User, y => y.UseValue<User>(null))
               .ForMember(x => x.Post, y => y.UseValue<Post>(null));
        }
    }
}
