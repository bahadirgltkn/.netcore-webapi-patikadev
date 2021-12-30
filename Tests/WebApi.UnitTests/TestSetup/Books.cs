using System;
using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup
{
    public static class Books
    {
        public static void AddBooks(this BookStoreDbContext context)
        {
            context.Books.AddRange(
             new Book{ 
                 Title = "Lean Startup",GenreId=1,PageCount=221,PublishDate = new DateTime(2001,06,12)
                 },
             new Book{
                 Title = "Herland",GenreId = 2,PageCount = 272,PublishDate = new DateTime(2009,06,23)
                 },
             new Book{
                 Title = "Dune",GenreId = 2,PageCount = 453,PublishDate = new DateTime(2012,07,12)
                 }
            );

        }
    }
}