using AutoMapper.QueryableExtensions;
using PhotoShare.Data;
using PhotoShare.Models;
using PhotoShare.Models.Enums;
using PhotoShare.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PhotoShare.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly PhotoShareContext context;

        public AlbumService(PhotoShareContext context)
        {
            this.context = context;
        }


        public TModel ById<TModel>(int id)
        {
            return By<TModel>(x => x.Id == id).SingleOrDefault();
        }

        public TModel ByName<TModel>(string name)
        {
            return By<TModel>(x => x.Name == name).SingleOrDefault();
        }

        public bool Exists(int id)
        {
            return this.ById<Album>(id) != null;
        }

        public bool Exists(string name)
        {
            return this.ByName<Album>(name) != null;
        }

        public Album Create(int userId, string albumTitle, string bgColor, string[] tags)
        {
            var album = new Album
            {
                Name = albumTitle,
                BackgroundColor = Enum.Parse<Color>(bgColor, true)
            };

            this.context.Albums.Add(album);
            this.context.SaveChanges();

            var albumRole = new AlbumRole
            {
                Album = album,
                UserId = userId
            };

            this.context.AlbumRoles.Add(albumRole);
            this.context.SaveChanges();

            foreach (var tag in tags)
            {
                var currentTagId = this.context.Tags.FirstOrDefault(x => x.Name == tag).Id;

                var albumTag = new AlbumTag
                {
                    Album = album,
                    TagId = currentTagId
                };

                this.context.AlbumTags.Add(albumTag);
            }

            this.context.SaveChanges();

            return album;
        }

   
        private IEnumerable<TModel> By<TModel>(Func<Album, bool> predicate)
        {
            return this.context.Albums
                .Where(predicate)
                .AsQueryable()
                .ProjectTo<TModel>();
        }
    }
}
