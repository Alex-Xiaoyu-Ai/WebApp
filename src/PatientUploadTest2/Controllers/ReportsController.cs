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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;

namespace PatientUploadTest2.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private IHostingEnvironment _environment;
        public ReportsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IHostingEnvironment environment)
        {
            _environment = environment;
            _userManager = userManager;
            _context = context;    
        }

        // GET: Reports
        public async Task<IActionResult> Index()
        {
            ViewData["h2"] = "�����б�";
            ViewData["edit"] = "��д";
            ViewData["detail"] = "��ϸ";

            return View(await _context.Report.Include(p=>p.Patient).ToListAsync());
        }

        // GET: Reports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Report.SingleOrDefaultAsync(m => m.Id == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // GET: Reports/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Reports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Auditor,Author,Diagnosis,Observation,PublishingTime,WritingTime,state")] Report report)
        {
            report.WritingTime = DateTime.Now;
            
            if (ModelState.IsValid)
            {

                report.state = ReportState.Written;
                _context.Add(report);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(report);
        }

        // GET: Reports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Report.SingleOrDefaultAsync(m => m.Id == id);
            if (report == null)
            {
                return NotFound();
            }
            
            return View(report);
        }

        // POST: Reports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Auditor,Author,Diagnosis,Observation,PublishingTime,WritingTime,state")] Report report)
        {
            if (id != report.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                // Add Authorized Operation: Autofill based on current user roles.

                if (report.Observation != null && report.Diagnosis != null)
                {
                    if (HttpContext.User.IsInRole("Auditor"))
                    {
                        report.Auditor =  _userManager.GetUserAsync(HttpContext.User).Result.Name;
                        if (report.state == ReportState.Approved)
                        {
                            report.PublishingTime = DateTime.Now;
                        }
                    }
                    report.WritingTime = DateTime.Now;
                    report.state = ReportState.Written;
                }

                try
                {
                    _context.Update(report);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReportExists(report.Id))
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
            return View(report);
        }

        

        // GET: Reports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Report.SingleOrDefaultAsync(m => m.Id == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }



        // POST: Reports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var report = await _context.Report.SingleOrDefaultAsync(m => m.Id == id);
            _context.Report.Remove(report);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ReportExists(int id)
        {
            return _context.Report.Any(e => e.Id == id);
        }


        /* Web APIs for Ajax - jQuery Autocomplete */
        // POST: Get Observation Template by querying the database with the content typed in
        [HttpPost]
        public IActionResult GetObservationTemplate(String Prefix)
        {
            //var results = _context.ReportTemplate.ToListAsync();

            var results2 = (from result in _context.ReportTemplate where result.Observation.Contains(Prefix) select result.Observation).Take(10);
            var result3 = results2.ToListAsync().Result;
            return Json(result3);
            /* Test Data
            string[] result3 = new String[3];
            result3[0] = "Canberra";
            result3[1] = "Wellington";
            result3[2] = "Beijing";
            var result4 = from result in result3 where result.Contains(Prefix) select result;
            return Json(result4);
            */


        }


        [HttpPost]
        // Get: Get Diagnosis Template by querying the database with the content typed in
        public IActionResult GetDiagnosisTemplate(String Prefix)
        {
            //var results = _context.ReportTemplate.ToListAsync();
            var results2 = (from result in _context.ReportTemplate where result.Diagnosis.Contains(Prefix) select result.Diagnosis).Take(10);
            var result3 = results2.ToListAsync().Result;
            return Json(result3);
            /* Test Data
            string[] result3=new String[3];
            result3[0] = "Australia";
            result3[1] = "New Zealand";
            result3[2] = "China";
            var result4 = from result in result3 where result.Contains(Prefix) select result;
            return Json(result4);
            */


        }
    }
}
