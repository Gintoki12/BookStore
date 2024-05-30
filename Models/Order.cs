using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    public class Order
    {
        public int Id { get; set; }

        // Foreign key for the Book entity
        [ForeignKey("Book")]
        public int BookId { get; set; }
        
        // Navigation property to the Book entity
        public Book Book { get; set; }

        // Other properties
        public string UserId { get; set; }
        public string Status { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
    }
}
