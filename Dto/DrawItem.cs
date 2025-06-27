namespace pd.Dto
{
    public class DrawItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Type { get; set; } = ""; // "serie" ou "subgenre"
        public string? ThumbUrl { get; set; }
    }

}
