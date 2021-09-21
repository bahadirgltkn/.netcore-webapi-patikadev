using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBookById
{
    public class GetBookByIdQuery
    {
        public int BookId {get; set;}
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetBookByIdQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public BookIdViewModel Handle()
        {
            var book = _dbContext.Books.Where(book => book.Id == BookId).SingleOrDefault();

            if(book is null)
                throw new InvalidOperationException("İlgili id'ye ait kitap bulunamadı...");

            BookIdViewModel vm = _mapper.Map<BookIdViewModel>(book);
            
            // BookIdViewModel vm = new BookIdViewModel();
            // vm.Title = book.Title;
            // vm.Genre = ((GenreEnum)book.GenreId).ToString();
            // vm.PageCount = book.PageCount;
            // vm.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyy");
            
            return vm;
        }
    }

    public class BookIdViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string Genre { get; set; }
        public string PublishDate { get; set; }
    }
}