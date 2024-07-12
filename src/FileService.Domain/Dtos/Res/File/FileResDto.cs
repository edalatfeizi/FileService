namespace FileService.Domain.Dtos.Res.File;

public record FileResDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ContentType { get; set; }
    public long Size { get; set; }
    public string Path { get; set; }
}
