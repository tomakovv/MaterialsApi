namespace MaterialsApi.Data.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public int MaterialId { get; set; }
        public Material Material { get; set; }
        public string TextReview { get; set; }
        public int NumericRating { get; set; }
    }
}