using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Hotel.Models
{
    public class Rezervacija
    {
        
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string ImeIPrezime { get; set; }
        public DateTime DatumOd { get; set; }
        public DateTime DatumDo { get; set; }

        public bool Otkazana { get; set; }

        public int SobaId { get; set; }

        [ForeignKey("SobaId")]
        public Soba Soba { get; set; }



    }
}