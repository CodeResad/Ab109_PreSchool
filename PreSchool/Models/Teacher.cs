using System.ComponentModel.DataAnnotations.Schema;
using PreSchool.Models.Base;

namespace PreSchool.Models;

public class Teacher : BaseEntity
{
    public string FullName { get; set; }
    public string Designation { get; set; }
    public string? ImgUrl { get; set; }
    [NotMapped]
    public IFormFile File { get; set; }
}