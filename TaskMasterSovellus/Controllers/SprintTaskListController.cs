using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaskMasterSovellus.Models;
using TaskMasterSovellus.ViewModels;

namespace TaskMasterSovellus.Controllers
{
    public class SprintTaskListController : Controller
    {
        
            private TaskMasterTietokantaEntities db = new TaskMasterTietokantaEntities();




        //Projekt connection lisk ottaa projektin idn ja näyttää kaikki siihen kuuluvia sprinttejä
        public ActionResult SprintsOfPrjekt(int? id)
        {
            if (Session["UserName"] == null)
            {
               return RedirectToAction("login", "home");
            }
            else
            {

                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var projectConnection = db.ProjectConnection.Include(p => p.Projects).Include(p => p.Sprints).Where(p=>p.ProjectId==id);

            return View(projectConnection.ToList());
        }
            }

        // action result palauttaa kaikki sprintiin kuuluvia taskeja
        public ActionResult SprintTasksList(int? id)
            {
            //var sprints = db.Sprints.Include(s => s.Colors).Include(s => s.Colors1).Include(s => s.Users).Include(s => s.SprintTemplate.TemplateTaskConnection.Select(t => t.TaskState.Tasks));
            //    return View(sprints.ToList());

            //if (Session["UserName"] == null)
            //{

            //       return RedirectToAction("login", "home");
            //}
            //else
            //{



            if (id == null)
            {
                return RedirectToAction("SprintsOfPrjekt", "SprintTaskList");
            }

            var tasks = from tas in db.Tasks.Where(a=>a.SprintId==id)
                        join tst in db.TaskState.Include(t1=>t1.Colors) on tas.StateId equals tst.StateId
                            //join cl3 in db.Colors.Include(c=>c.ColorValue) on tst.ColorId equals cl3.ColorId
                            //where tst.ColorId==cl3.ColorId
                            //join sp in db.Sprints on tas.SprintId equals sp.SprintId


                            //join stc in db.SprintTemplateConnection on sp.SprintId equals stc.SprintId
                            //    join stl in db.SprintTemplate on stc.SprintTemplateId equals stl.SprintTemplateId

                            //join ttc in db.TemplateTaskConnection on stl.SprintTemplateId equals ttc.SprintTemplateId /*into tas_ttc*/










                            //join cl in db.Colors on sp.BackgColor equals cl.ColorId
                            //join cl2 in db.Colors on sp.ProcessColor equals cl2.ColorId

                            select new SprintClassList
                        {
                            TaskId=tas.TaskId,
                            StateId=tas.StateId,
                            TaskName= (string)tas.TaskName,
                          TaskDescription = (string)tas.TaskDescription,
                            TaskPoints=tas.TaskPoints,
                            TaskPriority=tas.TaskPriority,
                          SprintId=tas.SprintId,


                            //people connectiontaken out, later when brige to people is built could be included ZA
                            //public virtual ICollection<TaskPeople> TaskPeople { get; set; }
                            //public virtual TaskState TaskState { get; set; }

                            //info from task state table ZA

                            StateName=(string)tst.StateName,
                                StateColor = (string)tst.Colors.ColorValue,
                                //TemplateConnectionId =ttc.TemplateConnectionId,
                                //SprintTemplateId=stl.SprintTemplateId,
                                //SprintName=(string)sp.SprintName,

                                //public Nullable<System.DateTime> StartDate { get; set; }
                                //public Nullable<System.DateTime> EndDate { get; set; }
                                //BackgColor=(string)cl.ColorValue,
                                // ProcessColor=(string)cl2.ColorValue,

                            };

                //tasks = tasks.Where(t => t.SprintId == id);




                //where kat.Class == 1


                //var ans = (from sar in context.StorageAreaRacks
                //           join sa in context.StorageAreas on sar.StorageAreaId equals sa.Id
                //           join sat in context.StorageAreaTypes on sa.StorageAreaTypeId equals sat.Id
                //           join r in context.Racks on sar.RackId equals r.Id
                //           where !sat.IsManual && r.IsEnabled && !r.IsVirtual
                //           select new
                //           {
                //               sat.Name,
                //               sat.Id
                //           }).Distinct().ToList();






                return View(tasks.ToList());
              
            
            
            }
        //}


        // GET: Tasks/Edit/5
        public ActionResult _ModalEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tasks tasks = db.Tasks.Find(id);
            if (tasks == null)
            {
                return HttpNotFound();
            }
            ViewBag.SprintId = new SelectList(db.Sprints, "SprintId", "SprintName", tasks.SprintId);
            ViewBag.StateId = new SelectList(db.TaskState, "StateId", "StateName", tasks.StateId);

            return PartialView("_ModalEdit",tasks);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _ModalEdit([Bind(Include = "TaskId,StateId,TaskName,TaskDescription,TaskPoints,TaskPriority,SprintId")] Tasks tasks)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tasks).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("SprintTasksList", new { id = tasks.SprintId });

            }
            ViewBag.SprintId = new SelectList(db.Sprints, "SprintId", "SprintName", tasks.SprintId);
            ViewBag.StateId = new SelectList(db.TaskState, "StateId", "StateName", tasks.StateId);
            return PartialView("_ModalEdit", tasks);
        }

        //MODAL TASKS DELETE

        // GET: Tasks/Delete/5
        public ActionResult _ModalDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tasks tasks = db.Tasks.Find(id);
            if (tasks == null)
            {
                return HttpNotFound();
            }
            return PartialView("_ModalDelete",tasks);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("_ModalDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult _ModalDeleteConfirmed(int id)
        {
            Tasks tasks = db.Tasks.Find(id);
            int? sprintId = tasks.SprintId;
            db.Tasks.Remove(tasks);
            db.SaveChanges();
            return RedirectToAction("SprintTasksList", new { id = sprintId });
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
