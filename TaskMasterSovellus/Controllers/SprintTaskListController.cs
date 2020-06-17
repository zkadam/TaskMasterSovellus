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




        //-------------------------------------------------Projekt connection list ottaa projektin idn ja näyttää kaikki siihen kuuluvia sprinttejä
        public ActionResult SprintsOfPrjekt(int? id)
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
            var projectConnection = db.ProjectConnection.Include(p => p.Projects).Include(p => p.Sprints).Where(p=>p.ProjectId==id);
                //project idn laitetaan sessioniin että pystyy käyttää backpainiketta
                Session["CurrentProject"] = id;

                //sending a viewbag element so we can use first element for header, but with checking viewbag in view , program wont crash on null objects ZA
                if (projectConnection.ToList().Count>0)
                {
                    ViewBag.projectCount = projectConnection.ToList().Count();
                }
                else
                {
                    ViewBag.projectCount = 0;

                }
                return View(projectConnection.ToList());
        }
            }

        // ----------------------------------------------------------------action result palauttaa kaikki sprintiin kuuluvia taskeja-----------------------------------------------------------
        public ActionResult SprintTasksList(int? id, string sprintname)
            {


            if (Session["UserName"] == null)
            {

                return RedirectToAction("login", "home");
            }
            else
            {



                ViewBag.SprintName = sprintname;
            ViewBag.SprintId = id;

            if (id == null)
            {
                return RedirectToAction("SprintsOfPrjekt", "SprintTaskList");
            }

            var tasks = from tas in db.Tasks.Where(a=>a.SprintId==id)
                        join tst in db.TaskState.Include(t1=>t1.Colors) on tas.StateId equals tst.StateId

                        //join cl3 in db.Colors.Include(c=>c.ColorValue) on tst.ColorId equals cl3.ColorId
                        //where tst.ColorId==cl3.ColorId
                        join sp in db.Sprints on tas.SprintId equals sp.SprintId


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
                            SprintName = (string)sp.SprintName,

                            //public Nullable<System.DateTime> StartDate { get; set; }
                            //public Nullable<System.DateTime> EndDate { get; set; }
                            //BackgColor=(string)cl.ColorValue,
                            // ProcessColor=(string)cl2.ColorValue,

                        };

                //double ordering so tasks come by they state and then by priority order
                //tasks = tasks.OrderBy(state => state.TaskId).ThenBy(state => state.TaskPriority);
                tasks = tasks.OrderBy(state => state.StateId).ThenBy(state => state.TaskPriority);

                ViewBag.TaskSum =tasks.Sum(s=>s.TaskPoints);
                ViewBag.DoneSum = tasks.Where(td => td.StateId == 1003).Sum(td => td.TaskPoints);
                ViewBag.TaskLeft = tasks.Sum(s => s.TaskPoints) - tasks.Where(td => td.StateId == 1003).Sum(td => td.TaskPoints);

                return View(tasks.ToList());
              
            
            
            }
        }


        // -----------------------------------------------------------------------Modal Edit-----------------------------------------------------------------------------------------/5
        public ActionResult _ModalEdit(int? id)
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
            Tasks tasks = db.Tasks.Find(id);
            if (tasks == null)
            {
                return HttpNotFound();
            }
            ViewBag.SprintId = new SelectList(db.Sprints, "SprintId", "SprintName", tasks.SprintId);
            ViewBag.StateId = new SelectList(db.TaskState, "StateId", "StateName", tasks.StateId);

            return PartialView("_ModalEdit",tasks);
            }
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _ModalEdit([Bind(Include = "TaskId,StateId,TaskName,TaskDescription,TaskPoints,TaskPriority,SprintId")] Tasks tasks)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("index", "home");
            }
            else
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
        }

        //--------------------------------------------------------------------------MODAL TASKS DELETE---------------------------------------------------------------------------------

        // GET: Tasks/Delete/5
        public ActionResult _ModalDelete(int? id)
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
            Tasks tasks = db.Tasks.Find(id);
            if (tasks == null)
            {
                return HttpNotFound();
            }
            return PartialView("_ModalDelete",tasks);
            }
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("_ModalDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult _ModalDeleteConfirmed(int id)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("index", "home");
            }
            else
            {

            Tasks tasks = db.Tasks.Find(id);

            //tallenetaan sprintidn että pystymmä sprintin task listaan palauttaa
            int? sprintId = tasks.SprintId;
            db.Tasks.Remove(tasks);
            db.SaveChanges();
            //palataan sprintiin sprintid parametrin avulla

            return RedirectToAction("SprintTasksList", "SprintTaskList", new { id = sprintId });
            }
        }

        //--------------------------------------------------------------------------MODAL  NEW TASK---------------------------------------------------------------------------------
        // GET: Tasks/Create
        public ActionResult _ModalCreate(int? sprintId)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("index", "home");
            }
            else
            {
            ViewBag.SprintId = new SelectList(db.Sprints, "SprintId", "SprintName", sprintId);
            ViewBag.StateId = new SelectList(db.TaskState, "StateId", "StateName");
            ViewBag.BackSprint = sprintId;
            return PartialView();

            }
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _ModalCreate([Bind(Include = "TaskId,StateId,TaskName,TaskDescription,TaskPoints,TaskPriority,SprintId")] Tasks tasks)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("index", "home");
            }
            else
            {

            if (ModelState.IsValid)
            {
                db.Tasks.Add(tasks);
                //tallenetaan sprintidn että pystymmä sprintin task listaan palauttaa
                int? sprintId = tasks.SprintId;
                db.SaveChanges();
                //palataan sprintiin sprintid parametrin avulla
                return RedirectToAction("SprintTasksList", "SprintTaskList", new { id = sprintId });
            }

            ViewBag.SprintId = new SelectList(db.Sprints, "SprintId", "SprintName", tasks.SprintId);
            ViewBag.StateId = new SelectList(db.TaskState, "StateId", "StateName", tasks.StateId);
            return PartialView("_ModalCreate", tasks);
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
