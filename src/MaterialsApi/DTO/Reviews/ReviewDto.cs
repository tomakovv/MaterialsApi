namespace MaterialsApi.DTO.Reviews
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string Material { get; set; }
        public string TextReview { get; set; }
        public int NumericRating { get; set; }
    }
}