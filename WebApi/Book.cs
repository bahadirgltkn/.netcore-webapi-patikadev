using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi
{
    public class Book{
        // Even without this attribute, the id value will increase by one
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}