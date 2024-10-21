namespace Backend.DTOs
{
    public class PostDto
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public string? Tittle { get; set; }
        public String? Body { get; set; }
    }
}
