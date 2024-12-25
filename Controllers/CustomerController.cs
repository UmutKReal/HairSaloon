using BarberSaloon.Data;
using BarberSaloon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BarberSaloon.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CustomerServis()
        {
            return View();
        }

        // Varsayılan olarak bir veritabanı context'i (Entity Framework vb.)
        private readonly BarberSaloonDBContext barberSaloonDB;
        public CustomerController(BarberSaloonDBContext context)
        {
            barberSaloonDB = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet]
        public ActionResult SelectServiceAndBarber()
        {
            // 1) Hizmet listesini çekip DropDown için SelectList oluşturuyoruz
            var services = barberSaloonDB.Services.ToList();
            ViewBag.ServiceList = new SelectList(services, "ServiceID", "ServiceName");

            // 2) Berber listesini çekip DropDown için SelectList oluşturuyoruz
            var barbers = barberSaloonDB.Employees.ToList();
            ViewBag.BarberList = new SelectList(barbers, "EmployeeID", "Name");

            return View();
        }

        [HttpPost]
        public ActionResult SelectServiceAndBarber(int ServiceID, int EmployeeID)
        {
            TempData["SelectedServiceId"] = ServiceID;
            TempData["SelectedEmployeeId"] = EmployeeID;

            return RedirectToAction("SelectTimeSlot");
        }

        private List<DateTime> GenerateTimeSlots(int durationInMinutes, Employee employee, int serviceID)
        {
            var appointments = barberSaloonDB.appointmentDateTimeEmployees
                .Where(a => a.EmployeeID == employee.EmployeeID)
                .ToList();

            if (appointments != null)
            {
                var result = new List<DateTime>();
                DateTime today = DateTime.Today;

                List<DateTime> DateTimeListesi = barberSaloonDB.appointmentDateTimeEmployees
                    .Where(a => a.EmployeeID == employee.EmployeeID)
                    .Select(a => a.AppointmentDate)
                    .ToList();

                List<Service> ServiceListesi = barberSaloonDB.appointmentDateTimeEmployees
                    .Where(a => a.EmployeeID == employee.EmployeeID)
                    .Select(a => a.Service)
                    .ToList();

                List<int> DurationTimeListesi = barberSaloonDB.appointmentDateTimeEmployees
                    .Where(a => a.EmployeeID == employee.EmployeeID)
                    .Select(d => d.Service.ServiceDuration)
                    .ToList();

                int index = 0;
                bool bayrak = false;

                for (int day = 0; day < 2; day++)
                {
                    var currentDate = today.AddDays(day);

                    for (int hour = 10; hour < 20; hour++)
                    {
                        //alınan randevu 1 saatlik mi 2 saatlik mi onu da öğren
                        if (durationInMinutes == 30)
                        {
                            var slot1 = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, hour, 0, 0);
                            var slot2 = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, hour, 30, 0);
                            if (!bayrak)
                            {
                                if (DateTimeListesi.Contains(slot1))//ya 1 saatlik randevuysa
                                {
                                    index = DateTimeListesi.IndexOf(slot1);
                                    if (DurationTimeListesi[index] == 60)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        result.Add(slot2);
                                    }
                                }
                                else if (DateTimeListesi.Contains(slot2))//30 geçe randevu var
                                {
                                    index = DateTimeListesi.IndexOf(slot2);
                                    if (DurationTimeListesi[index] == 60)
                                    {
                                        result.Add(slot1);
                                        bayrak = true;
                                        continue;
                                    }
                                    else
                                    {
                                        result.Add(slot1);
                                    }
                                }
                                else
                                {
                                    result.Add(slot1);
                                    result.Add(slot2);
                                }
                            }
                            else
                            {
                                if (DateTimeListesi.Contains(slot2))
                                {
                                    index = DateTimeListesi.IndexOf(slot2);
                                    if (DurationTimeListesi[index] == 30)
                                    {
                                        result.Add(slot1);
                                        bayrak = false;
                                        continue;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                else
                                {
                                    result.Add(slot2);
                                    bayrak = false;
                                }
                            }
                        }
                        else//Duration 60 DK
                        {
                            var slot1 = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, hour, 0, 0);
                            var slot2 = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, hour, 30, 0);
                            if (!bayrak)
                            {
                                if (DateTimeListesi.Contains(slot1))
                                {
                                    index = DateTimeListesi.IndexOf(slot1);
                                    if (DurationTimeListesi[index] == 30)
                                    {
                                        result.Add(slot2);
                                        bayrak = true;
                                        continue;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                else if (DateTimeListesi.Contains(slot2))
                                {
                                    index = DateTimeListesi.IndexOf(slot2);
                                    if (DurationTimeListesi[index] == 30)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        bayrak = true;
                                        continue;
                                    }
                                }
                                else
                                    result.Add(slot1);
                            }
                            else
                            {
                                if (DateTimeListesi.Contains(slot2))
                                {
                                    index = DateTimeListesi.IndexOf(slot2);
                                    if (DurationTimeListesi[index] == 30)
                                    {
                                        bayrak = false;
                                        continue;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                else
                                {
                                    bool bayrak2= false;
                                    foreach(DateTime dateTime in DateTimeListesi)
                                    {
                                        if (slot2.AddMinutes(30) == dateTime)
                                        {
                                            bayrak2 = true;
                                            bayrak = false;
                                        }
                                    }
                                    if(!bayrak2)
                                            result.Add(slot2);
                                }
                            }
                        }
                    }
                }

                return result;
            }
            else
            {
                var result = new List<DateTime>();
                DateTime today = DateTime.Today;
                for (int day = 0; day < 2; day++)
                {
                    var currentDate = today.AddDays(day);

                    for (int hour = 10; hour < 20; hour++)
                    {
                        //alınan randevu 1 saatlik mi 2 saatlik mi onu da öğren
                        if (durationInMinutes == 30)
                        {
                            var slot1 = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, hour, 0, 0);
                            var slot2 = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, hour, 30, 0);
                            result.Add(slot1);
                            result.Add(slot2);
                        }
                        else
                        {
                            var slot1 = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, hour, 0, 0);
                            result.Add(slot1);
                        }
                    }
                }
                return result;
            }
        }

        [HttpGet]
        public ActionResult SelectTimeSlot()
        {
            // TempData'dan servis ve berber ID'leri alalım.
            // (Eğer TempData'yı okuduğumuzda silinsin istemiyorsak "Keep" yapmalıyız.)
            var serviceIdObj = TempData["SelectedServiceId"];
            var barberIdObj = TempData["SelectedEmployeeId"];

            if (serviceIdObj == null || barberIdObj == null)
            {
                // Kullanıcı adım 1'i geçmemiş ya da TempData temizlenmiş
                return RedirectToAction("SelectServiceAndBarber");
            }

            int serviceId = (int)serviceIdObj;
            int barberId = (int)barberIdObj;

            // Tekrar TempData'ya koyuyoruz ki View'da da kullanabilelim (sonraki POST'ta da lazım olacak).
            TempData["SelectedServiceId"] = serviceId;
            TempData["SelectedEmployeeId"] = barberId;

            // Servis bilgisi
            var service = barberSaloonDB.Services.Find(serviceId);
            if (service == null)
            {
                return RedirectToAction("SelectServiceAndBarber");
            }

            ViewBag.ServiceName = service.ServiceName;
            ViewBag.Duration = service.ServiceDuration;

            // Berber bilgisi
            var barber = barberSaloonDB.Employees.Find(barberId);
            ViewBag.BarberName = barber.Name;

            // Servisin süresine göre zaman dilimlerini oluştur (örneğin önümüzdeki 3 gün, 10:00 - 20:00)
            var timeSlots = GenerateTimeSlots(service.ServiceDuration, barberSaloonDB.Employees.Find(barberId), service.ServiceID);
            ViewBag.AvailableSlots = timeSlots;

            return View();
        }

        //public ActionResult Index()
        //{
        //    var model = barberSaloonDB.Appointments
        //        .Include("ServiceID")
        //        .Include("EmployeeID")
        //        .ToList();
        //    return View(model);
        //}

        [HttpPost]
        public ActionResult SelectTimeSlot(DateTime selectedTime)
        {
            // Session veya TempData üzerinden servis/berber ID'yi tekrar alıyoruz
            var serviceIdObj = TempData["SelectedServiceId"];
            var barberIdObj = TempData["SelectedEmployeeId"];
            if (serviceIdObj == null || barberIdObj == null)
            {
                return RedirectToAction("SelectServiceAndBarber");
            }
            int serviceId = (int)serviceIdObj;
            int barberID = (int)barberIdObj;

            var id = TempData["CustomerID"];
            int userId = (int)id;
            //burada kullanıcı kontrolü yapman lazım kullanıcının appointmentı yoksa oluştur
            CheckAndCreateAppointment(userId);
            int appointmentId = barberSaloonDB.Appointments
                       .Where(a => a.CustomerID == userId)
                       .Select(a => a.AppointmentID)
                       .FirstOrDefault();

            var barber = barberSaloonDB.Employees
                .Include(e => e.AppointmentDateTimes) // İlişkili verileri yükle
                .FirstOrDefault(e => e.EmployeeID == barberID);

            var service = barberSaloonDB.Services
                .Include(s => s.AppointmentDateTimes)
                .FirstOrDefault(s => s.ServiceID == serviceId);

            var appointmentData = barberSaloonDB.Appointments
                .Include(a => a.AppointmentDateTimes)
                .FirstOrDefault(a => a.AppointmentID == appointmentId);

            // Eğer ilişkili verileri hemen yüklemek istiyorsanız, Include kullanarak bağlayabilirsiniz
            var appointment = new AppointmentDateTime
            {
                AppointmentID = appointmentId,
                ServiceID = serviceId,
                Service = barberSaloonDB.Services.Find(serviceId),
                Appointment = barberSaloonDB.Appointments.Find(appointmentId),
                AppointmentDate = selectedTime
            };

            var appointmentEmployee = new AppointmentDateTimeEmployee
            {
                AppointmentDate = selectedTime,
                Service = barberSaloonDB.Services.Find(serviceId),
                ServiceID = serviceId,
                Employee = barberSaloonDB.Employees.Find(barberID),
                EmployeeID = barberID
            };

            barberSaloonDB.AppointmentDateTimes.Add(appointment);
            barberSaloonDB.appointmentDateTimeEmployees.Add(appointmentEmployee);
            barberSaloonDB.SaveChanges();

            // Kaydedildi, Index'e dönelim (veya bir onay sayfasına)
            return RedirectToAction("Index");
        }

        public void CheckAndCreateAppointment(int userId)
        {
            // 1. Kullanıcının zaten bir randevusu var mı diye kontrol et
            bool exists = barberSaloonDB.Appointments.Any(a => a.AppointmentID == userId);

            // 2. Yoksa yeni bir randevu ekle
            if (!exists)
            {
                var newAppointment = new Appointment
                {
                    CustomerID = userId,
                };

                barberSaloonDB.Appointments.Add(newAppointment);
                barberSaloonDB.SaveChanges();
            }
        }
    }
}
