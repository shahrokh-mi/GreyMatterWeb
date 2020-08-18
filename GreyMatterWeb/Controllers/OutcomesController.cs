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
    public class OutcomesController : Controller
    {
        private GreyDataEntities db = new GreyDataEntities();

        // GET: Outcomes
        public ActionResult Index()
        {
            var outcomes = db.Outcomes.Include(o => o.Option).Include(o => o.Picture).Include(o => o.Situation).Include(o => o.User);
            return View(outcomes.ToList());
        }

        // GET: Outcomes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Outcome outcome = db.Outcomes.Find(id);
            if (outcome == null)
            {
                return HttpNotFound();
            }
            return View(outcome);
        }

        // GET: Outcomes/Create
        public ActionResult Create()
        {
            ViewBag.OptionId = new SelectList(db.Options, "Id", "Name");
            ViewBag.PictureId = new SelectList(db.Pictures, "Id", "MimeType");
            ViewBag.SituationId = new SelectList(db.Situations, "Id", "Name");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username");
            return View();
        }

        // POST: Outcomes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,OptionId,Name,Description,PictureId,UserId,SituationId")] Outcome outcome)
        {
            if (ModelState.IsValid)
            {
                db.Outcomes.Add(outcome);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OptionId = new SelectList(db.Options, "Id", "Name", outcome.OptionId);
            ViewBag.PictureId = new SelectList(db.Pictures, "Id", "MimeType", outcome.PictureId);
            ViewBag.SituationId = new SelectList(db.Situations, "Id", "Name", outcome.SituationId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username", outcome.UserId);
            return View(outcome);
        }

        // GET: Outcomes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Outcome outcome = db.Outcomes.Find(id);
            if (outcome == null)
            {
                return HttpNotFound();
            }
            ViewBag.OptionId = new SelectList(db.Options, "Id", "Name", outcome.OptionId);
            ViewBag.PictureId = new SelectList(db.Pictures, "Id", "MimeType", outcome.PictureId);
            ViewBag.SituationId = new SelectList(db.Situations, "Id", "Name", outcome.SituationId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username", outcome.UserId);
            return View(outcome);
        }

        // POST: Outcomes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,OptionId,Name,Description,PictureId,UserId,SituationId")] Outcome outcome)
        {
            if (ModelState.IsValid)
            {
                db.Entry(outcome).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OptionId = new SelectList(db.Options, "Id", "Name", outcome.OptionId);
            ViewBag.PictureId = new SelectList(db.Pictures, "Id", "MimeType", outcome.PictureId);
            ViewBag.SituationId = new SelectList(db.Situations, "Id", "Name", outcome.SituationId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username", outcome.UserId);
            return View(outcome);
        }

        // GET: Outcomes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Outcome outcome = db.Outcomes.Find(id);
            if (outcome == null)
            {
                return HttpNotFound();
            }
            return View(outcome);
        }

        // POST: Outcomes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Outcome outcome = db.Outcomes.Find(id);
            db.Outcomes.Remove(outcome);
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
