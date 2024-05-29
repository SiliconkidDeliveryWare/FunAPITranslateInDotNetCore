using System.ComponentModel.DataAnnotations;

public class FunAPITranslate
{
    public int Id { get; set; }
    [Required] 
    public string? textSource { get; set; }
    [Required] 
    public string? textTarget { get; set; }
    [Required] 
    public string? textAPI { get; set; }
    public DateTime Downdated { get; set; } = DateTime.UtcNow;
    public DateTime Updated { get; set; } = default!;

}