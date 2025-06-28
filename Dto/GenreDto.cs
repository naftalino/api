namespace pd.Dto
{
    public class GenreDto
    {
        public string Name { get; set; } = string.Empty;
        public record GenresDto(int Id, string Name, int Total, int subgenresTotal);
    }
}
