using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        private BookStoreDbContext _dbContext;
        public int AuthorId { get; set; }
        public UpdateAuthorModel Model { get; set; }

        public UpdateAuthorCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var author = _dbContext.Authors.SingleOrDefault(x => x.Id == AuthorId);
            if(author is null)
                throw new InvalidOperationException("Yazar bulunamadı...");
            
            author.Name = string.IsNullOrEmpty(Model.Name.Trim()) ? author.Name : Model.Name;
            author.LastName = string.IsNullOrEmpty(Model.LastName.Trim()) ? author.LastName : Model.LastName;
            author.Birthday = Model.Birthday != default ? Model.Birthday : author.Birthday;
            _dbContext.SaveChanges();
        }

    }

    public class UpdateAuthorModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
    }
}