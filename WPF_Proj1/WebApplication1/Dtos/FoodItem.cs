namespace WebApplication1.Dtos
{
    public class FoodItemDto
    {
        public string Name { get; set; } = "";
        public string Categ { get; set; } = "";
        public string[] Ingreds { get; set; } = Array.Empty<string>();
        public string[] Allergens { get; set; } = Array.Empty<string>();
        public string[] Tags { get; set; } = Array.Empty<string>();
    }
}
