using Appointments.Service.Interfaces;
using Appointments.Service.ViewModels;
using Appointments.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Appointments.Web.Common;
using Appointments.Service;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace Appointments.Web.Controllers
{
    [Authorize]
    public class AppointmentsController : Controller
    {

        IAppointmentService _appointmentService;
        IHelperService _helperService;
        IPatientService _patientService;
        public AppointmentsController(IAppointmentService appointmentService, IHelperService helperService, IPatientService patientService)
        {
            this._appointmentService = appointmentService;
            this._helperService = helperService;
            this._patientService = patientService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Appointments_Read([DataSourceRequest]DataSourceRequest request, DateTime start, DateTime end)
        {
            if (start == end)
            {
                end = end.AddDays(1);
            }

            var events = _appointmentService.GetEvents(start, end);

            var model = from e in events
                        select new EventViewModel()
                        {
                            AppointmentId = e.ID,
                            Title = e.PatientName + " - " + e.Status,
                            Start = DateTime.SpecifyKind(e.Start, DateTimeKind.Utc),
                            End = DateTime.SpecifyKind(e.End, DateTimeKind.Utc),
                            Color = e.Color,
                            IsAllDay = false,
                            TypeId = e.TypeId
                        };

            return Json(model.ToDataSourceResult(request));
        }

        public ActionResult Appointment(long id = 0, DateTime? start = null, DateTime? end = null)
        {
            AppointmentViewModel model;
            var currentModel = TempData["model"];
            if (currentModel != null)
            {
                model = currentModel as AppointmentViewModel;
                TempData["model"] = null;
            }
            else
            {
                model = new AppointmentViewModel();
                if (id != 0)
                {
                    model = _appointmentService.GetAppointment(id);
                }
            }
            SetViewData(model);

            return View(model);
        }

        [HttpPost]
        public ActionResult Appointment(AppointmentViewModel model, string btnName, string txtName, string txtCode, string txtBirthdate)
        {
            switch (btnName)
            {
                case "Search":
                    TempData["model"] = model;
                    PatientViewModel patient = new PatientViewModel() { Code = txtCode, FullName = txtName, Birthdate = txtBirthdate };
                    var result = _patientService.Search(patient);
                    return View("PatientsList", result);
                    break;
                default:
                    break;
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _appointmentService.SaveAppointment(model);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    //ViewBag.ExceptionMessages = ex.Message;
                    ModelState.AddModelError("", ex.Message);
                }
            }
            SetViewData(model);
            return View(model);
        }

        public ActionResult SelectPatient(long id)
        {
            var model = TempData["model"] as AppointmentViewModel;
            var patient = _patientService.GetPatient(id);
            model.Address = patient.Address;
            model.BirthDate = patient.Birthdate;
            model.Code = patient.Code;
            model.CountryId = patient.CountryId;
            model.FullName = patient.FullName;
            model.Gender = patient.Gender;
            model.Phone = patient.Phone;
            model.PatientId = patient.Id;

            return RedirectToAction("Appointment");
        }
        
        public ActionResult BackToAppointment()
        {
            var model = TempData["model"] as AppointmentViewModel;
            return RedirectToAction("Appointment");
        }

        public ActionResult Delete(long id)
        {
            try
            {
                _appointmentService.DeleteAppointment(id);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
            return Content("true");
        }

        public ActionResult ChangeStatus(long id, string status)
        {
            try
            {
                switch (status)
                {
                    case "Scheduled":
                        _appointmentService.ChangeAppointmentStatus(id, (int)AppointmentStatuses.Scheduled);
                        break;
                    case "Arrived":
                        _appointmentService.ChangeAppointmentStatus(id, (int)AppointmentStatuses.Arrived);
                        break;
                    case "Cancelled":
                        _appointmentService.ChangeAppointmentStatus(id, (int)AppointmentStatuses.Cancelled);
                        break;
                    case "Completed":
                        _appointmentService.ChangeAppointmentStatus(id, (int)AppointmentStatuses.Completed);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
            return Content("true");
        }

        void SetViewData(AppointmentViewModel model)
        {
            var cachedStatuses = AppDomain.CurrentDomain.GetData("appointmentStatuses") as List<AppointmentStatusViewModel>;
            if (cachedStatuses == null)
            {
                cachedStatuses = _helperService.GetStatuses();
                AppDomain.CurrentDomain.SetData("appointmentStatuses", cachedStatuses);
            }
            ViewBag.StatusId = new SelectList(cachedStatuses, "Id", "Name", model.StatusId);

            var cachedTypes = AppDomain.CurrentDomain.GetData("appointmentTypes") as List<AppointmentTypeViewModel>;
            if (cachedTypes == null)
            {
                cachedTypes = _helperService.GetTypes();
                AppDomain.CurrentDomain.SetData("appointmentTypes", cachedTypes);
            }
            ViewBag.TypeId = new SelectList(cachedTypes, "Id", "Name", model.TypeId);

            ViewBag.Gender = new SelectList(Enum.GetValues(typeof(Genders)).Cast<Genders>().Select(e => new SelectListItem
            {
                Text = e.ToString(),
                Value = ((char)e).ToString()
            }), "Value", "Text", model.Gender);

            var cachedCountries = AppDomain.CurrentDomain.GetData("countries") as List<CountryViewModel>;
            if (cachedCountries == null)
            {
                cachedCountries = _helperService.GetCountries();
                AppDomain.CurrentDomain.SetData("countries", cachedCountries);
            }

            ViewBag.CountryId = new SelectList(cachedCountries, "Id", "Name", model.CountryId);
        }
        public JsonResult GetTypes()
        {
            var data = _helperService.GetTypes();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}