using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PatientUploadTest2.Data;
using PatientUploadTest2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.IO;
using PatientUploadTest2.Models.PatientViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;

namespace PatientUploadTest2.Controllers
{
    [Authorize(Roles ="SuperUser, HospitalClient")]
    public class PatientsController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        private IHostingEnvironment _environment;
        private UserManager<ApplicationUser> _userManager;

        private async void createAndSaveFile(IFormFile file, String dir)
        {
            Directory.CreateDirectory(dir);
            FileStream fs = new FileStream(dir, FileMode.Create, FileAccess.Write);
            await file.CopyToAsync(fs);
        }

        public PatientsController(ApplicationDbContext context, IHostingEnvironment environment, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
            _environment = environment;
        }

        // GET: Patients
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "���������Ϣ";
            ViewData["h2"] = "������Ϣ";
            ViewData["create"] = "�����²���";
            ViewData["edit"] = "�༭";
            ViewData["detail"] = "��ϸ";
            ViewData["delete"] = "ɾ��";
            
            var patients = _context.Patient.Include(s => s.Reports).AsNoTracking();
            return View(await patients.ToListAsync());
        }

        // GET: Patients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patient.Include(d=>d.Reports).AsNoTracking().SingleOrDefaultAsync(m => m.id == id);
            
            
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // GET: Patients/Create
        public IActionResult Create()
        {
            ViewData["Title"] = "�����²���";
            ViewData["h2"] = "�����²���";
            ViewData["create"] = "����";
            ViewData["backtolist"] = "���ز����б�";
            ViewData["uploadfile"] = "�ϴ���������";
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,DateOfBirth,HistoryPath,Name,Study,file")] PatientView patientViewModel)
        {
            Patient realPatient = new Patient();
            var path = Path.Combine(_environment.WebRootPath, "uploads", "Patient History", patientViewModel.Name, DateTime.Now.ToString("yyyyMMddHHmmss"));
            realPatient.DateOfBirth = patientViewModel.DateOfBirth;
            
            realPatient.Name = patientViewModel.Name;
            realPatient.Study = patientViewModel.Study;
            
            if (patientViewModel.file.Length > 0)
            {
                //createAndSaveFile(patientViewModel.file, path);
                realPatient.HistoryPath = path;
                
            }

            
            if (ModelState.IsValid)
            {
                /* Automatically create a new report for this patient */
                var currentUser = _userManager.GetUserAsync(HttpContext.User).Result;
                Report report = new Report
                {
                    Patient = realPatient,
                    
                    state = ReportState.Unwritten
                    
                };

                if (currentUser.Role == Roles.HospitalClient)
                {
                    report.HospitalClient = currentUser.Employer;
                }
                
                _context.Add(realPatient);
                _context.Add(report);

                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(patientViewModel);
        }

       
        // GET: Patients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["Title"] = "�༭��������";
            ViewData["h2"] = "�༭��������";
            ViewData["save"] = "����";
            ViewData["backtolist"] = "���ز����б�";
           

            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patient.SingleOrDefaultAsync(m => m.id == id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,DateOfBirth,HistoryPath,Name,Study")] Patient patient)
        {
            if (id != patient.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(patient.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(patient);
        }

        [Authorize(Roles ="SuperUser")]
        // GET: Patients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewData["Title"] = "ɾ����������";
            ViewData["h2"] = "ɾ����������";
            ViewData["h3"] = "��ȷ��Ҫɾ�����²������ϣ�";
            ViewData["delete"] = "ȷ��ɾ��";
            ViewData["backtolist"] = "���ز����б�";

            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patient.SingleOrDefaultAsync(m => m.id == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // POST: Patients/Delete/5
        [Authorize(Roles ="SuperUser")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patient = await _context.Patient.SingleOrDefaultAsync(m => m.id == id);
            _context.Patient.Remove(patient);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PatientExists(int id)
        {
            return _context.Patient.Any(e => e.id == id);
        }
    }
}
