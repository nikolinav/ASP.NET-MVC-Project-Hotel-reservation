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
    public class SobaController : Controller
    {
        private SmestajContext db = new SmestajContext();
        private int SobePerPage = 2;

        public enum SortTypes
        {
            BrojKreveta,
            Ceni,
            CeniOpadajucoj,
            
        };

        public static Dictionary<string, SortTypes> SortTypeDict = new Dictionary<string, SortTypes>
        {
            {"BrojKreveta" , SortTypes.BrojKreveta },
            {"Po ceni rastucoj" , SortTypes.Ceni },
            {"Po ceni opadajucoj" , SortTypes.CeniOpadajucoj },
           

        };
        // GET: Soba
        public ActionResult Index(string sortType = "BrojKreveta", int page = 1)
        {
            SortTypes sortBy = SortTypeDict[sortType];

            IQueryable<Soba> sobe = db.Sobe.Include(p => p.Smestaj);

            switch (sortBy)
            {
                case SortTypes.BrojKreveta:
                    sobe = sobe.OrderBy(x => x.BrojKreveta);
                    break;
                case SortTypes.Ceni:
                    sobe = sobe.OrderBy(x => x.Cena);
                    break;
                case SortTypes.CeniOpadajucoj:
                    sobe = sobe.OrderByDescending(x => x.Cena);
                    break;
     
            }

            ViewBag.sortTypes = new SelectList(SortTypeDict, "Key", "Key", sortType);
            ViewBag.CurrentSortType = sortType;

            return View(sobe.ToPagedList(page, SobePerPage));
        }

        // GET: Soba/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Soba soba = db.Sobe.Find(id);
            if (soba == null)
            {
                return HttpNotFound();
            }
            return View(soba);
        }

        // GET: Soba/Create
        public ActionResult Create()
        {
            ViewBag.SmestajId = new SelectList(db.Smestaji, "Id", "Naziv");
            return View();
        }

        // POST: Soba/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BrojSobe,BrojKreveta,Cena,SmestajId")] Soba soba)
        {
            if (ModelState.IsValid)
            {
                db.Sobe.Add(soba);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SmestajId = new SelectList(db.Smestaji, "Id", "Naziv", soba.SmestajId);
            return View(soba);
        }

        // GET: Soba/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Soba soba = db.Sobe.Find(id);
            if (soba == null)
            {
                return HttpNotFound();
            }
            ViewBag.SmestajId = new SelectList(db.Smestaji, "Id", "Naziv", soba.SmestajId);
            return View(soba);
        }

        // POST: Soba/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BrojSobe,BrojKreveta,Cena,SmestajId")] Soba soba)
        {
            if (ModelState.IsValid)
            {
                db.Entry(soba).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SmestajId = new SelectList(db.Smestaji, "Id", "Naziv", soba.SmestajId);
            return View(soba);
        }

        // GET: Soba/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Soba soba = db.Sobe.Find(id);
            if (soba == null)
            {
                return HttpNotFound();
            }
            return View(soba);
        }

        // POST: Soba/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Soba soba = db.Sobe.Find(id);
            db.Sobe.Remove(soba);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Filter(SobaFilter filter)
        {
            var soba = db.Sobe.Include(p => p.Smestaj);

            if (!filter.SmestajName.IsNullOrWhiteSpace())
            {
                soba = soba.Where(p => p.Smestaj.Naziv.Contains(filter.SmestajName));
            }

            if (filter.BrojKreveta != null)
            {
                soba = soba.Where(p => p.BrojKreveta >= filter.BrojKreveta);
            }

            if (filter.PriceFrom != null)
            {
                soba = soba.Where(p => p.Cena >= filter.PriceFrom);
            }

            if (filter.PriceTo != null)
            {
                soba = soba.Where(p => p.Cena <= filter.PriceFrom);
            }


            return View(soba.ToList());
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
