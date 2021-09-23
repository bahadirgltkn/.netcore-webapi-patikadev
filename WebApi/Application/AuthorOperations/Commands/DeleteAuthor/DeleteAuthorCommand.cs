using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        public int AuthorId { get; set; }
        private readonly BookStoreDbContext _dbContext;

        public DeleteAuthorCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var author = _dbContext.Authors.SingleOrDefault(x => x.Id == AuthorId);
            if(author is null)
                throw new InvalidOperationException("Böyle bir yazar mevcut değildir...");
            
            if(_dbContext.Books.Any(x => x.AuthorId == AuthorId))
                throw new InvalidOperationException("Yazara ait kitap bulunmaktadır. Yazarı silebilmek için öncelikle kitabı silmelisiniz...");
            
            _dbContext.Authors.Remove(author);
            _dbContext.SaveChanges();
        }    
    }
}