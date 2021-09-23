using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public class DataGenerator
    {
        public  static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if(context.Books.Any())
                {
                    return;
                }

                context.Authors.AddRange(
                    new Author{
                        Name = "Cahit",
                        LastName = "Zarifoglu",
                        Birthday = new DateTime(1943,06,22)
                    },
                    new Author{
                        Name = "İlber",
                        LastName = "Ortaylı",
                        Birthday = new DateTime(1954,04,14)
                    }
                );

                context.Genres.AddRange(
                    new Genre
                    {
                        Name = "Personal Growth"
                    },
                    new Genre
                    {
                        Name = "Science Fiction"
                    },
                    new Genre
                    {
                        Name = "Romance"
                    }
                );

                context.Books.AddRange(
                    new Book{
                        //Id = 1,
                        Title = "Lean Startup",
                        GenreId = 1,
                        PageCount = 221,
                        PublishDate = new DateTime(2001,06,12)
                    },
                    new Book{
                        //Id = 2,
                        Title = "Herland",
                        GenreId = 2,
                        PageCount = 272,
                        PublishDate = new DateTime(2009,06,23)
                    },
                    new Book{
                        //Id = 3,
                        Title = "Dune",
                        GenreId = 2,
                        PageCount = 453,
                        PublishDate = new DateTime(2012,07,12)
                    }
                );

                context.SaveChanges();
            }
        }
    }
}