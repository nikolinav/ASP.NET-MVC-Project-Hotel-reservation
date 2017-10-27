using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hotel.Models;
using Microsoft.Ajax.Utilities;
using PagedList;

namespace Hotel.Controllers
{
    public class SmestajController : Controller
    {
        private SmestajContext db = new SmestajContext();
        private int SmestajPerPage = 2;

        public enum SortTypes
        {
            Naziv,
            Oceni,

        };

        public static Dictionary<string, SortTypes> SortTypeDict = new Dictionary<string, SortTypes>
        {
            {"Po nazivu" , SortTypes.Naziv },
            {"Po oceni" , SortTypes.Oceni },

        };

        // GET: Smestaj
        public ActionResult Index(string sortType = "Po nazivu", int page = 1)
        {
            SortTypes sortBy = SortTypeDict[sortType];

            IQueryable<Smestaj> smastaj = db.Smestaji.Include(p => p.Soba);

            switch (sortBy)
            {
                case SortTypes.Naziv:
                    smastaj = smastaj.OrderBy(x => x.Naziv);
                    break;
                case SortTypes.Oceni:
                    smastaj = smastaj.OrderBy(x => x.Ocena);
                    break;
              

            }

            ViewBag.sortTypes = new SelectList(SortTypeDict, "Key", "Key", sortType);
            ViewBag.CurrentSortType = sortType;

            return View(smastaj.ToPagedList(page, SmestajPerPage));
        }

        // GET: Smestaj/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smestaj smestaj = db.Smestaji.Find(id);
            if (smestaj == null)
            {
                return HttpNotFound();
            }
            return View(smestaj);
        }

        // GET: Smestaj/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Smestaj/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Naziv,Opis,Adresa,Ocena")] Smestaj smestaj)
        {
            if (ModelState.IsValid)
            {
                db.Smestaji.Add(smestaj);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(smestaj);
        }

        // GET: Smestaj/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smestaj smestaj = db.Smestaji.Find(id);
            if (smestaj == null)
            {
                return HttpNotFound();
            }
            return View(smestaj);
        }

        // POST: Smestaj/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Naziv,Opis,Adresa,Ocena")] Smestaj smestaj)
        {
            if (ModelState.IsValid)
            {
                db.Entry(smestaj).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(smestaj);
        }

        // GET: Smestaj/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smestaj smestaj = db.Smestaji.Find(id);
            if (smestaj == null)
            {
                return HttpNotFound();
            }
            return View(smestaj);
        }

        // POST: Smestaj/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Smestaj smestaj = db.Smestaji.Find(id);
            db.Smestaji.Remove(smestaj);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Filter(SmestajFilter filter)
        {
            var smestaj = db.Smestaji.Include(p => p.Soba);

            if (!filter.SmestajName.IsNullOrWhiteSpace())
            {
                smestaj = smestaj.Where(p => p.Naziv.Contains(filter.SmestajName));
            }

            if (!filter.AdresaName.IsNullOrWhiteSpace())
            {
                smestaj = smestaj.Where(p => p.Adresa.Contains(filter.AdresaName));
            }


            return View(smestaj.ToList());
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
