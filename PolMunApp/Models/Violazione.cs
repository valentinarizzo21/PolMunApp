﻿using System.ComponentModel.DataAnnotations;

namespace PoliziaMunicipaleApp.Models
{
    public class Violazione
    {
        public int ID { get; set; }

        [Required]
        public string Descrizione { get; set; }

        [Required]
        public decimal Importo { get; set; }

        [Required]
        public int PuntiDecurtati { get; set; }
    }
}