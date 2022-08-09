using System.ComponentModel.DataAnnotations;

namespace MaterialsApi.DTO.Reviews
{
    public class AddReviewDto
    {
        [Required]
        public int MaterialId { get; set; }

        [Required]
        public string TextReview { get; set; }

        [Required]
        [Range(1, 10)]
        public int NumericRating { get; set; }
    }
}