using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinProject.Models.FormData
{
    public class DynamicDataModel
    {
        [Key]
        public int Id { get; set; }
        public string? Data_Type { get; set; }
        public string? ColumnName { get; set; }
        public string? Examples { get; set; }
        public string? option { get; set; }
        [NotMapped]
        public ICollection<string>? optionts { get; set; }
    }
}
