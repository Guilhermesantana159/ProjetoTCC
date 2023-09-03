namespace Aplication.Models.Request.Utils;

public class FeedbackRequest
{
    public int Rating { get; set; } 
    public string? Comentario { get; set; }
    public int IdUsuarioCadastro { get; set; }
}