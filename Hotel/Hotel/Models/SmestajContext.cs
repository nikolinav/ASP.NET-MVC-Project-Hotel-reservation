using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Hotel.Models
{
    public class SmestajContext :DbContext
    {

        public virtual DbSet<Smestaj> Smestaji { get; set; }
        public virtual DbSet<Soba> Sobe { get; set; }
        public virtual DbSet<Rezervacija> Rezervacije { get; set; }


        public SmestajContext() : base ("name=SmestajContext")
        {



        }

    }
}