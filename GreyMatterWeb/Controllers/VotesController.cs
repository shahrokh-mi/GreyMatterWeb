﻿using System;
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
    public class VotesController : Controller
    {
        private GreyDataEntities db = new GreyDataEntities();

        // GET: Votes
        public ActionResult Index()
        {
            var votes = db.Votes.Include(v => v.User).Include(v => v.User1);
            return View(votes.ToList());
        }

        // GET: Votes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vote vote = db.Votes.Find(id);
            if (vote == null)
            {
                return HttpNotFound();
            }
            return View(vote);
        }

        // GET: Votes/Create
        public ActionResult Create()
        {
            ViewBag.VoterId = new SelectList(db.Users, "Id", "Username");
            ViewBag.VoteeId = new SelectList(db.Users, "Id", "Username");
            return View();
        }

        // POST: Votes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,VoterId,EntityId,EntityGroup,CreatedOn,UpdatedOn,VoteeId,Value")] Vote vote)
        {
            if (ModelState.IsValid)
            {
                db.Votes.Add(vote);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.VoterId = new SelectList(db.Users, "Id", "Username", vote.VoterId);
            ViewBag.VoteeId = new SelectList(db.Users, "Id", "Username", vote.VoteeId);
            return View(vote);
        }

        // GET: Votes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vote vote = db.Votes.Find(id);
            if (vote == null)
            {
                return HttpNotFound();
            }
            ViewBag.VoterId = new SelectList(db.Users, "Id", "Username", vote.VoterId);
            ViewBag.VoteeId = new SelectList(db.Users, "Id", "Username", vote.VoteeId);
            return View(vote);
        }

        // POST: Votes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,VoterId,EntityId,EntityGroup,CreatedOn,UpdatedOn,VoteeId,Value")] Vote vote)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vote).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.VoterId = new SelectList(db.Users, "Id", "Username", vote.VoterId);
            ViewBag.VoteeId = new SelectList(db.Users, "Id", "Username", vote.VoteeId);
            return View(vote);
        }

        // GET: Votes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vote vote = db.Votes.Find(id);
            if (vote == null)
            {
                return HttpNotFound();
            }
            return View(vote);
        }

        // POST: Votes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vote vote = db.Votes.Find(id);
            db.Votes.Remove(vote);
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
