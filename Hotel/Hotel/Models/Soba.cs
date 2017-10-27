using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Hotel.Models
{
    public class Soba
    {
        public int Id { get; set; }
        public int BrojSobe { get; set; }
        public int BrojKreveta { get; set; }

        [Range(0, int.MaxValue)]
        public decimal Cena { get; set; }

        public int SmestajId { get; set; }

        [ForeignKey("SmestajId")]
        public Smestaj Smestaj { get; set; }

        public IList<Rezervacija> Rezervacije { get; set; }
    }
}