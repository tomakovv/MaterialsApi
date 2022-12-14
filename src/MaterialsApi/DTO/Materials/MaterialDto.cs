namespace MaterialsApi.DTO.Materials
{
    public class MaterialDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string AuthorId { get; set; }
        public string TypeId { get; set; }
        public string PublishDate { get; set; }
        public IEnumerable<string> TextReviews { get; set; }
        public IEnumerable<int> NumericRating { get; set; }
    }
}