using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarsApplication.Models;

namespace CarsApplication.Controllers
{
    public class JourneyController : Controller
    {
        private CarsAppEntities db = new CarsAppEntities();

        // GET: Journey
        public ActionResult Index()
        {
            var journeys = db.Journeys.Include(j => j.Car).Include(j => j.Point).Include(j => j.Point1);
            return View(journeys.ToList());
        }

        // GET: Journey/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Journey journey = db.Journeys.Find(id);
            if (journey == null)
            {
                return HttpNotFound();
            }
            return View(journey);
        }

        // GET: Journey/Create
        public ActionResult Create()
        {
            ViewBag.car_id = new SelectList(db.Cars, "car_id", "name");
            ViewBag.departure = new SelectList(db.Points, "point_id", "point1");
            ViewBag.destination = new SelectList(db.Points, "point_id", "point1");
            return View();
        }

        // POST: Journey/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "journey_id,journey1,departure,destination,car_id,date,distance")] Journey journey)
        {
            if (ModelState.IsValid)
            {
                db.Journeys.Add(journey);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.car_id = new SelectList(db.Cars, "car_id", "name", journey.car_id);
            ViewBag.departure = new SelectList(db.Points, "point_id", "point1", journey.departure);
            ViewBag.destination = new SelectList(db.Points, "point_id", "point1", journey.destination);
            return View(journey);
        }

        // GET: Journey/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Journey journey = db.Journeys.Find(id);
            if (journey == null)
            {
                return HttpNotFound();
            }
            ViewBag.car_id = new SelectList(db.Cars, "car_id", "name", journey.car_id);
            ViewBag.departure = new SelectList(db.Points, "point_id", "point1", journey.departure);
            ViewBag.destination = new SelectList(db.Points, "point_id", "point1", journey.destination);
            return View(journey);
        }

        // POST: Journey/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "journey_id,journey1,departure,destination,car_id,date,distance")] Journey journey)
        {
            if (ModelState.IsValid)
            {
                db.Entry(journey).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.car_id = new SelectList(db.Cars, "car_id", "name", journey.car_id);
            ViewBag.departure = new SelectList(db.Points, "point_id", "point1", journey.departure);
            ViewBag.destination = new SelectList(db.Points, "point_id", "point1", journey.destination);
            return View(journey);
        }

        // GET: Journey/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Journey journey = db.Journeys.Find(id);
            if (journey == null)
            {
                return HttpNotFound();
            }
            return View(journey);
        }

        // POST: Journey/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Journey journey = db.Journeys.Find(id);
            db.Journeys.Remove(journey);
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
