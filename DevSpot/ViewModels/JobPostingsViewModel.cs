using System.ComponentModel.DataAnnotations;

namespace DevSpot.ViewModels;

public class JobPostingsViewModel
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
}