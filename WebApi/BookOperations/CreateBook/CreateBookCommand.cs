using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.CreateBook
{
    public class CreateBookCommand
    {
        public CreateBookModel Model { get; set; }
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateBookCommand(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Title == Model.Title);
            
            if(book is not null)
                throw new InvalidOperationException("Kitap zaten mevcut");
            
            book = _mapper.Map<Book>(Model);
            
            //book = new Book();
            // book.Title = Model.Title;
            // book.PageCount = Model.PageCount;
            // book.PublishDate = Model.PublishDate;
            // book.GenreId = Model.GenreId;

            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();

        }
    }

    public class CreateBookModel
    {
        // Kitap dışarıdan oluşturulmak istendiğinde hangi bilgiler alınacak

        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}