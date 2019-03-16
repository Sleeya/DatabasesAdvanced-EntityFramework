using AutoMapper.QueryableExtensions;
using PhotoShare.Data;
using PhotoShare.Models;
using PhotoShare.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PhotoShare.Services
{
    public class TownService : ITownService
    {
        private readonly PhotoShareContext context;

        public TownService(PhotoShareContext context)
        {
            this.context = context;
        }

        public Town Add(string townName, string countryName)
        {
            var town = new Town
            {
                Name = townName,
                Country = countryName
            };

            this.context.Towns.Add(town);

            this.context.SaveChanges();

            return town;
        }

        public TModel ById<TModel>(int id)
        {
            return this.By<TModel>(x=>x.Id == id).SingleOrDefault();
        }

        public TModel ByName<TModel>(string name)
        {
            return this.By<TModel>(x => x.Name == name).SingleOrDefault();
        }

        public bool Exists(int id)
        {
            return this.ById<Town>(id) != null;
        }

        public bool Exists(string name)
        {
            return this.ByName<Town>(name) != null;
        }

        private IEnumerable<TModel> By<TModel>(Func<Town, bool> predicate)
        {
            return this.context.Towns
                .Where(predicate)
                .AsQueryable()
                .ProjectTo<TModel>();
        }
    }
}
