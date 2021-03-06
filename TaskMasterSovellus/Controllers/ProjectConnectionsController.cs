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
    public class ProjectConnectionsController : Controller
    {
        private TaskMasterTietokantaEntities db = new TaskMasterTietokantaEntities();

        // GET: ProjectConnections
        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("index", "home");
            }
            else
            {

            var projectConnection = db.ProjectConnection.Include(p => p.Projects).Include(p => p.Sprints);
            return View(projectConnection.ToList());
            }
        }

       

        // GET: ProjectConnections/Create
        public ActionResult Create(int? selectedProject, int? selectedSprint)

        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("index", "home");
            }
            else
            {

            //seuraava kaks tarkastus tsekkaa tuliko parametri jota käytätään select listojen selected optionina
            //jos ei tullut parametri, niin laitetaan eka sprint/projekti selected elementiksi
            if (selectedSprint==null)
	        {
                selectedSprint = db.Sprints.FirstOrDefault().SprintId;
	        }
            if (selectedProject == null)
            {
                selectedProject = db.Projects.FirstOrDefault().ProjectId;
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectName", selectedProject);
            ViewBag.SprintId = new SelectList(db.Sprints, "SprintId", "SprintName", selectedSprint);
            return View();
            }
        }

        // POST: ProjectConnections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjectConnectionId,SprintId,ProjectId")] ProjectConnection projectConnection)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("index", "home");
            }
            else
            {

            if (ModelState.IsValid)
            {
                db.ProjectConnection.Add(projectConnection);
                db.SaveChanges();
                return RedirectToAction("SprintsOfPrjekt", "SprintTaskList", new {id=projectConnection.ProjectId});
                
            }

            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectName", projectConnection.ProjectId);
            ViewBag.SprintId = new SelectList(db.Sprints, "SprintId", "SprintName", projectConnection.SprintId);
            return View(projectConnection);
            }
        }

        // GET: ProjectConnections/Edit/5
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
            ProjectConnection projectConnection = db.ProjectConnection.Find(id);
            if (projectConnection == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectName", projectConnection.ProjectId);
            ViewBag.SprintId = new SelectList(db.Sprints, "SprintId", "SprintName", projectConnection.SprintId);
            return View(projectConnection);
            }
        }

        // POST: ProjectConnections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProjectConnectionId,SprintId,ProjectId")] ProjectConnection projectConnection)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("index", "home");
            }
            else
            {

            if (ModelState.IsValid)
            {
                db.Entry(projectConnection).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("SprintsOfPrjekt", "SprintTaskList", new { id = projectConnection.ProjectId });
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectName", projectConnection.ProjectId);
            ViewBag.SprintId = new SelectList(db.Sprints, "SprintId", "SprintName", projectConnection.SprintId);
            return View(projectConnection);
            }
        }

        // GET: ProjectConnections/Delete/5
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
            ProjectConnection projectConnection = db.ProjectConnection.Find(id);
            if (projectConnection == null)
            {
                return HttpNotFound();
            }
            return View(projectConnection);
            }
        }

        // POST: ProjectConnections/Delete/5
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

            ProjectConnection projectConnection = db.ProjectConnection.Find(id);
            db.ProjectConnection.Remove(projectConnection);
            db.SaveChanges();
            return RedirectToAction("SprintsOfPrjekt", "SprintTaskList", new { id = projectConnection.ProjectId });
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
