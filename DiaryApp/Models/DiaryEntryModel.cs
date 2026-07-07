using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Models
{
    public class DiaryEntry
    {
        //annotation key -> unique identifier
        [Key]
        public int Id { get; set;}
        [Required(ErrorMessage = "Please enter a title")]
        [StringLength(100, MinimumLength = 3,ErrorMessage = "Title must be btwn 3-100 characters")]
        public string Title { get; set;} = string.Empty;
        [Required]
        public string Content { get; set;} = string.Empty;
        [Required]
        public  DateTime Created { get; set;} = DateTime.Now;
    }
}