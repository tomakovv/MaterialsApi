namespace MaterialsApi.Data.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<Material> CreatedMaterials { get; set; } = new List<Material>();
        public int NumberOfCreatedMaterials => CreatedMaterials.Count();
    }
}