using System.ComponentModel.DataAnnotations;

namespace ScreeenSound.Web.Requests;
public record ArtistaRequest([Required] string nome, [Required] string bio);

