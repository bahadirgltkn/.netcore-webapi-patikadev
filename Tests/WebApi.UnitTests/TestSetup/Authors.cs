using System;
using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup
{
    public static class Authors
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {
            context.Authors.AddRange(
                new Author
                {
                    Name = "Cahit",LastName = "Zarifoglu",Birthday = new DateTime(1943,06,22)
                },
                new Author
                {
                    Name = "İlber",LastName = "Ortaylı",Birthday = new DateTime(1954,04,14)
                }
            );

        }
    }
}