using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoliziaMunicipaleApp.Models
{
    public class Verbale
    {
        public int ID { get; set; }

        [Required]
        public int TrasgressoreID { get; set; }

        [Required]
        public int ViolazioneID { get; set; }

        [Required]
        public DateTime DataViolazione { get; set; }

        [Required]
        public decimal Importo { get; set; }

        [ForeignKey("TrasgressoreID")]
        public Trasgressore Trasgressore { get; set; }

        [ForeignKey("ViolazioneID")]
        public Violazione Violazione { get; set; }
    }
}