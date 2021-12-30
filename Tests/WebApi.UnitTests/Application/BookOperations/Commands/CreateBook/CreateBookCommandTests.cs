using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateBookCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }


        [Fact]
        public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange
            var book = new Book(){
                Title = "Test_WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn",
                PageCount = 100,
                PublishDate = new System.DateTime(1990,01,10),
                GenreId = 1
            };
            
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();

            CreateBookCommand command = new CreateBookCommand(_dbContext, _mapper);
            command.Model = new CreateBookModel(){Title = book.Title};


            //act & assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap zaten mevcut.");
                // hatalı bir test olmasını istersek Handle() metodu içerisinde hata mesajını değiştirebiliriz
        }

        [Fact]
        public void WhenValidInputsAreGıven_Book_ShouldBeCreated()
        {
            // Arrange
            CreateBookCommand command = new CreateBookCommand(_dbContext, _mapper);
            CreateBookModel model = new CreateBookModel() {
                Title = "Hobbit",
                PageCount = 1024,
                PublishDate = DateTime.Now.Date.AddYears(-12),
                GenreId = 2
            };
            command.Model = model;

            // Act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // Assert
            var book = _dbContext.Books.SingleOrDefault(book => book.Title == model.Title);
            book.Should().NotBeNull();
            book.PageCount.Should().Be(model.PageCount);
            book.PublishDate.Should().Be(model.PublishDate);
            book.GenreId.Should().Be(model.GenreId); 
        }
    }
}