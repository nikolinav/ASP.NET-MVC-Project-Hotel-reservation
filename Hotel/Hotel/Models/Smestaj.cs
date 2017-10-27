using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hotel.Models
{
    public class Smestaj
    {
        public int Id { get; set; }

        [Required]
        public string Naziv { get; set; }

        public string Opis { get; set; }
        public string Adresa { get; set; }

        [Range(1.0, 5.0)]
        public double Ocena { get; set; }
        public IList<Soba> Soba { get; set; }


    }
}