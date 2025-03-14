using System.ComponentModel.DataAnnotations;

namespace PoliziaMunicipaleApp.Models
{
    public class Trasgressore
    {
        public int ID { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Cognome { get; set; }

        [Required, StringLength(16)]
        public string CodiceFiscale { get; set; }

        public string Indirizzo { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
    }
}