namespace MaterialsApi.Data.Entities
{
    public class Material
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public Author? Author { get; set; }
        public MaterialType? Type { get; set; }
        public DateTime PublishDate { get; set; }
        public IEnumerable<Review>? Reviews { get; set; } = new List<Review>();
    }
}