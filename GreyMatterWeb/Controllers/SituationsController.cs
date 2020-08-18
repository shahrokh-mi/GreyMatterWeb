using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GreyMatterWeb.Domain;

namespace GreyMatterWeb.Controllers
{
    public class SituationsController : Controller
    {
        private GreyDataEntities db = new GreyDataEntities();

        // GET: Situations
        public ActionResult Index()
        {
            var situations = db.Situations.Include(s => s.Picture).Include(s => s.User);
            return View(situations.ToList());
        }

        // GET: Situations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Situation situation = db.Situations.Find(id);
            if (situation == null)
            {
                return HttpNotFound();
            }
            return View(situation);
        }

        // GET: Situations/Create
        public ActionResult Create()
        {
            ViewBag.PictureId = new SelectList(db.Pictures, "Id", "MimeType");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username");
            return View();
        }

        // POST: Situations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,CreatedOn,UpdatedOn,UserId,PictureId")] Situation situation)
        {
            if (ModelState.IsValid)
            {
                db.Situations.Add(situation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PictureId = new SelectList(db.Pictures, "Id", "MimeType", situation.PictureId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username", situation.UserId);
            return View(situation);
        }

        // GET: Situations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Situation situation = db.Situations.Find(id);
            if (situation == null)
            {
                return HttpNotFound();
            }
            ViewBag.PictureId = new SelectList(db.Pictures, "Id", "MimeType", situation.PictureId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username", situation.UserId);
            return View(situation);
        }

        // POST: Situations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,CreatedOn,UpdatedOn,UserId,PictureId")] Situation situation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(situation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PictureId = new SelectList(db.Pictures, "Id", "MimeType", situation.PictureId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username", situation.UserId);
            return View(situation);
        }

        // GET: Situations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Situation situation = db.Situations.Find(id);
            if (situation == null)
            {
                return HttpNotFound();
            }
            return View(situation);
        }

        // POST: Situations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Situation situation = db.Situations.Find(id);
            db.Situations.Remove(situation);
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
