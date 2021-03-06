﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaskMasterSovellus.Models;

namespace TaskMasterSovellus.Controllers
{
    public class SprintsController : Controller
    {
        private TaskMasterTietokantaEntities db = new TaskMasterTietokantaEntities();

        // GET: Sprints
        //public ActionResult Index()
        //{
        //    var sprints = db.Sprints.Include(s => s.Colors).Include(s => s.Colors1).Include(s => s.Users);
        //    return View(sprints.ToList());
        //}



        // GET: Sprints/Create
        public ActionResult Create()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("index", "home");
            }
            else
            {

            ViewBag.BackgColor = new SelectList(db.Colors, "ColorId", "ColorName");
            ViewBag.ProcessColor = new SelectList(db.Colors, "ColorId", "ColorName");
            ViewBag.AdminId = new SelectList(db.Users, "UserId", "Surname");
            return View();
            }
        }

        // POST: Sprints/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SprintId,SprintName,AdminId,StartDate,EndDate,BackgColor,ProcessColor")] Sprints sprints)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("index", "home");
            }
            else
            {

            if (ModelState.IsValid)
            {
                //var param = Session["CurrentProject"];
                db.Sprints.Add(sprints);
                db.SaveChanges();
                return RedirectToAction("Create", "ProjectConnections", new { selectedProject = Session["CurrentProject"], selectedSprint= sprints.SprintId});
            }

            ViewBag.BackgColor = new SelectList(db.Colors, "ColorId", "ColorName", sprints.BackgColor);
            ViewBag.ProcessColor = new SelectList(db.Colors, "ColorId", "ColorName", sprints.ProcessColor);
            ViewBag.AdminId = new SelectList(db.Users, "UserId", "Surname", sprints.AdminId);
            return View(sprints);
            }
        }

        // GET: Sprints/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("index", "home");
            }
            else
            {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sprints sprints = db.Sprints.Find(id);
            if (sprints == null)
            {
                return HttpNotFound();
            }
            ViewBag.BackgColor = new SelectList(db.Colors, "ColorId", "ColorName", sprints.BackgColor);
            ViewBag.ProcessColor = new SelectList(db.Colors, "ColorId", "ColorName", sprints.ProcessColor);
            ViewBag.AdminId = new SelectList(db.Users, "UserId", "Surname", sprints.AdminId);
            return View(sprints);
            }
        }

        // POST: Sprints/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SprintId,SprintName,AdminId,StartDate,EndDate,BackgColor,ProcessColor")] Sprints sprints)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("index", "home");
            }
            else
            {

            if (ModelState.IsValid)
            {
                db.Entry(sprints).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("SprintsOfPrjekt", "SprintTaskList", new { id = Session["CurrentProject"]});

            }
            ViewBag.BackgColor = new SelectList(db.Colors, "ColorId", "ColorName", sprints.BackgColor);
            ViewBag.ProcessColor = new SelectList(db.Colors, "ColorId", "ColorName", sprints.ProcessColor);
            ViewBag.AdminId = new SelectList(db.Users, "UserId", "Surname", sprints.AdminId);
            return View(sprints);
            }
        }

        // GET: Sprints/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("index", "home");
            }
            else
            {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sprints sprints = db.Sprints.Find(id);
            if (sprints == null)
            {
                return HttpNotFound();
            }
            return View(sprints);
            }
        }

        // POST: Sprints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("index", "home");
            }
            else
            {

            Sprints sprints = db.Sprints.Find(id);
            db.Sprints.Remove(sprints);
            db.SaveChanges();
            return RedirectToAction("Index");
            }
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
