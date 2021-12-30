using System;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommand
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateAuthorModel Model;

        public CreateAuthorCommand(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {

            var author = _dbContext.Authors.SingleOrDefault(x => x.Name == Model.Name && x.LastName == Model.LastName);
            if(author is not null)
                throw new InvalidOperationException("Bu yazar mevcuttur...");
            
            Author newAuthor = _mapper.Map<Author>(Model);
            _dbContext.Authors.Add(newAuthor);
            _dbContext.SaveChanges();
            
        }


    }

    public class CreateAuthorModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
    }
}