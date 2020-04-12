using Proyectof.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyectof.Controllers
{
    public class ConsultasController : Controller
    {
        private HospitalEntities db = new HospitalEntities();
        // GET: Consultas
        public ActionResult Pacientes()
        {
            return View(db.Pacientes.ToList());
        }
        [HttpPost]
        public ActionResult Pacientes(bool? asegurado, string cedula = null, string nombre = null)
        {
            var busqueda = from s in db.Pacientes select s;
            if (asegurado != null)
            {
                busqueda = busqueda.Where(f => f.asegurado == asegurado);
            }
            if (!string.IsNullOrEmpty(cedula))
            {
                busqueda = busqueda.Where(f => f.cedula.StartsWith(cedula));
            }
            if (!string.IsNullOrEmpty(nombre))
            {
                busqueda = busqueda.Where(f => f.nombre.StartsWith(nombre));
            }
          
          
            return View(busqueda.ToList());
        }


        public ActionResult Medicos()
        {
            return View(db.Medicos.ToList());
        }
        [HttpPost]
        public ActionResult Medicos(string especialidad = null, string nombre = null)
        {
            var busqueda = from s in db.Medicos select s;

            if (!string.IsNullOrEmpty(especialidad))
            {
                busqueda = busqueda.Where(f => f.especialidad.StartsWith(especialidad));
            }
            if (!string.IsNullOrEmpty(nombre))
            {
                busqueda = busqueda.Where(f => f.nombre.StartsWith(nombre));
            }


            return View(busqueda.ToList());
        }


        public ActionResult Habitaciones()
        {
            var habitaciones = db.Habitaciones.Include(h => h.Tipos);
            return View(habitaciones.ToList());
        }
        [HttpPost]
        public ActionResult Habitaciones(string tipo = null)
        {
            var busqueda = from s in db.Habitaciones select s;

            if (!string.IsNullOrEmpty(tipo))
            {
                busqueda = busqueda.Where(f => f.Tipos.descripcion.StartsWith(tipo));
            }

            return View(busqueda.ToList());
        }



        public ActionResult Citas()
        {
            var citas = db.Citas.Include(c => c.Medicos).Include(c => c.Pacientes);
            return View(citas.ToList());
        }
        [HttpPost]
        public ActionResult Citas(DateTime? fecha, string medico = null, string paciente = null)
        {
            var busqueda = from s in db.Citas select s;
            if (fecha != null)
            {
                busqueda = busqueda.Where(f => f.fecha == fecha);
            }
            if (!string.IsNullOrEmpty(medico))
            {
                busqueda = busqueda.Where(f => f.Medicos.nombre.StartsWith(medico));
            }
            if (!string.IsNullOrEmpty(paciente))
            {
                busqueda = busqueda.Where(f => f.Pacientes.nombre.StartsWith(paciente));
            }
            return View(busqueda.ToList());
        }




        public ActionResult Ingresos()
        {
            var ingresos = db.Ingresos.Include(i => i.Habitaciones).Include(i => i.Pacientes);
            return View(ingresos.ToList());
        }
        [HttpPost]
        public ActionResult Ingresos(DateTime? fecha, int? numero)
        {
            var busqueda = from s in db.Ingresos select s;
            if (fecha != null)
            {
                busqueda = busqueda.Where(f => f.fecha == fecha);
            }
            if (numero != null)
            {
                busqueda = busqueda.Where(f => f.Habitaciones.numero == numero);
            }
            return View(busqueda.ToList());
        }


            public ActionResult AltaMedica()
        {
            var altaMedica = db.AltaMedica.Include(a => a.Habitaciones).Include(a => a.Ingresos).Include(a => a.Pacientes);
            return View(altaMedica.ToList());
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