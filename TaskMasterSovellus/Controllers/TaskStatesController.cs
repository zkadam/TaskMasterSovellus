using System;
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
    public class TaskStatesController : Controller
    {
        private TaskMasterTietokantaEntities db = new TaskMasterTietokantaEntities();

        // GET: TaskStates
        public ActionResult Index()
        {
            return View(db.TaskState.ToList());
        }

        // GET: TaskStates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskState taskState = db.TaskState.Find(id);
            if (taskState == null)
            {
                return HttpNotFound();
            }
            return View(taskState);
        }

        // GET: TaskStates/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TaskStates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StateId,StateName")] TaskState taskState)
        {
            if (ModelState.IsValid)
            {
                db.TaskState.Add(taskState);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(taskState);
        }

        // GET: TaskStates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskState taskState = db.TaskState.Find(id);
            if (taskState == null)
            {
                return HttpNotFound();
            }
            return View(taskState);
        }

        // POST: TaskStates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StateId,StateName")] TaskState taskState)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taskState).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(taskState);
        }

        // GET: TaskStates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskState taskState = db.TaskState.Find(id);
            if (taskState == null)
            {
                return HttpNotFound();
            }
            return View(taskState);
        }

        // POST: TaskStates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaskState taskState = db.TaskState.Find(id);
            db.TaskState.Remove(taskState);
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
