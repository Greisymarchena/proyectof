using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proyectof.Models;

namespace Proyectof.Controllers
{
    public class AltaMedicaController : Controller
    {
        private HospitalEntities db = new HospitalEntities();

        // GET: AltaMedica
        public ActionResult Index()
        {
            var altaMedica = db.AltaMedica.Include(a => a.Habitaciones).Include(a => a.Ingresos).Include(a => a.Pacientes);
            return View(altaMedica.ToList());
        }

        public ActionResult NewIndex()
        {
            var ingresos = db.Ingresos.Include(i => i.Habitaciones).Include(i => i.Pacientes);
            return View(ingresos.ToList());
        }

        // GET: AltaMedica/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AltaMedica altaMedica = db.AltaMedica.Find(id);
            if (altaMedica == null)
            {
                return HttpNotFound();
            }
            return View(altaMedica);
        }

        public ActionResult Alta(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingresos ingresos = db.Ingresos.Find(id);
            if (ingresos == null)
            {
                return HttpNotFound();
            }
            ViewBag.idHabitacion = new SelectList(db.Habitaciones, "idHabitacion", "numero", ingresos.idHabitacion);
            ViewBag.idPaciente = new SelectList(db.Pacientes, "idPaciente", "cedula", ingresos.idPaciente);
            return View(ingresos);
        }

        // GET: AltaMedica/Create
        public ActionResult Create()
        {
            ViewBag.idHabitacion = new SelectList(db.Habitaciones, "idHabitacion", "idHabitacion");
            ViewBag.idIngreso = new SelectList(db.Ingresos, "idIngreso", "idIngreso");
            ViewBag.idPaciente = new SelectList(db.Pacientes, "idPaciente", "cedula");
            return View();
        }

        // POST: AltaMedica/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idAlta,idIngreso,idPaciente,idHabitacion,fechaIngreso,fechaSalida,monto")] AltaMedica altaMedica)
        {
            if (ModelState.IsValid)
            {
                db.AltaMedica.Add(altaMedica);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idHabitacion = new SelectList(db.Habitaciones, "idHabitacion", "idHabitacion", altaMedica.idHabitacion);
            ViewBag.idIngreso = new SelectList(db.Ingresos, "idIngreso", "idIngreso", altaMedica.idIngreso);
            ViewBag.idPaciente = new SelectList(db.Pacientes, "idPaciente", "cedula", altaMedica.idPaciente);
            return View(altaMedica);
        }

        // GET: AltaMedica/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AltaMedica altaMedica = db.AltaMedica.Find(id);
            if (altaMedica == null)
            {
                return HttpNotFound();
            }
            ViewBag.idHabitacion = new SelectList(db.Habitaciones, "idHabitacion", "idHabitacion", altaMedica.idHabitacion);
            ViewBag.idIngreso = new SelectList(db.Ingresos, "idIngreso", "idIngreso", altaMedica.idIngreso);
            ViewBag.idPaciente = new SelectList(db.Pacientes, "idPaciente", "cedula", altaMedica.idPaciente);
            return View(altaMedica);
        }

        // POST: AltaMedica/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idAlta,idIngreso,idPaciente,idHabitacion,fechaIngreso,fechaSalida,monto")] AltaMedica altaMedica)
        {
            if (ModelState.IsValid)
            {
                db.Entry(altaMedica).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idHabitacion = new SelectList(db.Habitaciones, "idHabitacion", "idHabitacion", altaMedica.idHabitacion);
            ViewBag.idIngreso = new SelectList(db.Ingresos, "idIngreso", "idIngreso", altaMedica.idIngreso);
            ViewBag.idPaciente = new SelectList(db.Pacientes, "idPaciente", "cedula", altaMedica.idPaciente);
            return View(altaMedica);
        }

        // GET: AltaMedica/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AltaMedica altaMedica = db.AltaMedica.Find(id);
            if (altaMedica == null)
            {
                return HttpNotFound();
            }
            return View(altaMedica);
        }

        // POST: AltaMedica/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AltaMedica altaMedica = db.AltaMedica.Find(id);
            db.AltaMedica.Remove(altaMedica);
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
