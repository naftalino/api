namespace gacha.Dto
{
    public class SerieDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Genre { get; set; }
        public string? ThumbUrl { get; set; }
        public int Id { get; internal set; }
    }
}