using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hotel.Models;
using PagedList;

namespace Hotel.Controllers
{
    public class RezervacijaController : Controller
    {
        private SmestajContext db = new SmestajContext();
        private int RezervacijePerPage = 2;

        public enum SortTypes
        {
            ImenuIPrezimenu,
            BrojSobe,
            

        };

        public static Dictionary<string, SortTypes> SortTypeDict = new Dictionary<string, SortTypes>
        {
            {"Po Imenu i Prezimenu" , SortTypes.ImenuIPrezimenu },
            {"Po broju sobe" , SortTypes.BrojSobe},
          


        };

        // GET: Rezervacija
        public ActionResult Index(string sortType = "Po Imenu i Prezimenu", int page = 1)
        {
            SortTypes SortBy = SortTypeDict[sortType];

            IQueryable<Rezervacija> rezervacija = db.Rezervacije.Include(p => p.Soba);

            switch (SortBy)
            {
                case SortTypes.ImenuIPrezimenu:
                    rezervacija = rezervacija.OrderBy(x => x.ImeIPrezime);
                    break;
                case SortTypes.BrojSobe:
                    rezervacija = rezervacija.OrderBy(x => x.Soba.BrojSobe);
                    break;
               

            }

            ViewBag.sortTypes = new SelectList(SortTypeDict, "Key", "Key", sortType);
            ViewBag.CurrentSortType = sortType;

            return View(rezervacija.ToPagedList(page, RezervacijePerPage));

        }

        // GET: Rezervacija/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rezervacija rezervacija = db.Rezervacije.Find(id);
            if (rezervacija == null)
            {
                return HttpNotFound();
            }
            return View(rezervacija);
        }

        // GET: Rezervacija/Create
        public ActionResult Create()
        {
            ViewBag.SobaId = new SelectList(db.Sobe, "Id", "BrojSobe");
            return View();
        }

        // POST: Rezervacija/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ImeIPrezime,DatumOd,DatumDo,Otkazana,SobaId")] Rezervacija rezervacija)
        {
            if (ModelState.IsValid)
            {
                db.Rezervacije.Add(rezervacija);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SobaId = new SelectList(db.Sobe, "Id", "BrojSobe", rezervacija.SobaId);
            return View(rezervacija);
        }

        // GET: Rezervacija/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rezervacija rezervacija = db.Rezervacije.Find(id);
            if (rezervacija == null)
            {
                return HttpNotFound();
            }
            ViewBag.SobaId = new SelectList(db.Sobe, "Id", "BrojSobe", rezervacija.SobaId);
            return View(rezervacija);
        }

        // POST: Rezervacija/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ImeIPrezime,DatumOd,DatumDo,Otkazana,SobaId")] Rezervacija rezervacija)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rezervacija).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SobaId = new SelectList(db.Sobe, "Id", "BrojSobe", rezervacija.SobaId);
            return View(rezervacija);
        }

        
        public ActionResult Cancel(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rezervacija rezervacija = db.Rezervacije.Find(id);
            if (rezervacija == null)
            {
                return HttpNotFound();
            }
            ViewBag.SobaId = new SelectList(db.Sobe, "Id", "BrojSobe", rezervacija.SobaId);
            return View(rezervacija);
        }
        [HttpPost]
        public ActionResult Cancel([Bind(Include = "Id,ImeIPrezime,DatumOd,DatumDo,Otkazana,SobaId")] Rezervacija rezervacija)
        {
                
                db.Entry(rezervacija).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
