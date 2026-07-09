using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace DevSpot.Models
{
    public class JobPosting
    {
        public int Id {get; set;} //giving name ID, ef will automtically set it as PK
        [Required]
        public string Title {get; set;} = string.Empty;
        [Required]
        public string Description {get; set;} = string.Empty;
        [Required]
        public string Company {get; set;} = string.Empty;
        [Required]
        public string Location {get; set;} = string.Empty;
        public DateTime PostedDate {get; set;} = DateTime.UtcNow;
        public bool IsApproved {get;set;}
        
        [Required]
        public string UserId {get; set;}

        [ForeignKey(nameof(UserId))]
        public IdentityUser User {get; set;}
    }
}