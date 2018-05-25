using Appointments.Service.Interfaces;
using Appointments.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Appointments.Web.Controllers
{
    public class PatientsController : Controller
    {
        IPatientService _patientService;
        public PatientsController(IPatientService patientService )
        {
            this._patientService = patientService;
        }
        
        [HttpGet]
        public ActionResult Search()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult Search(AppointmentViewModel model, string txtName, string txtCode)
        //{
        //    var result = _patientService.Search(model);
        //    return RedirectToAction("PatientsList", result);
        //}

        public ActionResult PatientsList(List<PatientViewModel> list)
        {
            return View(list);
        }

        // GET: Patients/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
    }
}
