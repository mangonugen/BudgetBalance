using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransactionDataModels
{
    public class User
    {
        //[ForeignKey("BlogId")]
        [Key]        
        public Guid UserId { get; set; }

        [Required]
        [MaxLength(50)]
        [Index("idx_username", IsUnique = true)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; }

        [Required]
        [MaxLength(100)]
        public string EmailAddress { get; set; }
                
        [MaxLength(50)]
        public string DefaultPage { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime DateLastLogin { get; set; }
    }
}
