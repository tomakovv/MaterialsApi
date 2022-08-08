using System.ComponentModel.DataAnnotations;

namespace MaterialsApi.DTO.Materials
{
    public class AddMaterialDto
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public int AuthorId { get; set; }

        [Required]
        public int TypeId { get; set; }

        [Required]
        public DateTime PublishDate { get; set; }
    }
}