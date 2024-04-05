using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Students.Common.Models;
using Students.Interfaces;

namespace Students.Web.Controllers
{
    public class FieldsOfStudiesController : Controller
    {
        private readonly ILogger _logger;
        private readonly IDatabaseService _databaseService;

        public FieldsOfStudiesController(ILogger<FieldsOfStudiesController> looger, IDatabaseService databaseService)
        {
            _logger = looger;
            _databaseService = databaseService;
        }

        // GET: FieldsOfStudies
        public async Task<IActionResult> Index()
        {
            IActionResult result = View();
            var model = await _databaseService.IndexFieldOfStudy();
            result = View(model);
            return result;
        }

        // GET: FieldsOfStudies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var fieldOfStudies = await _databaseService.DetailsFieldOfStudies(id);
            return View(fieldOfStudies);
        }

        // GET: FieldsOfStudies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FieldsOfStudies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] FieldOfStudies fieldOfStudy)
        {
            if (ModelState.IsValid)
            {
                fieldOfStudy = await _databaseService.CreateFieldOfStudies(fieldOfStudy);
                return RedirectToAction(nameof(Index));
            }
            return View(fieldOfStudy);
        }

        // GET: FieldsOfStudies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fieldOfStudy = await _databaseService.EditFieldOfStudies(id);
            if (fieldOfStudy == null)
            {
                return NotFound();
            }
            return View(fieldOfStudy);
        }

        // POST: FieldsOfStudies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] FieldOfStudies fieldOfStudy)
        {
            if (id != fieldOfStudy.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    fieldOfStudy = await _databaseService.EditFieldOfStudies(id, fieldOfStudy);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FieldOfStudyExists(fieldOfStudy.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(fieldOfStudy);
        }

        // GET: FieldsOfStudies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fieldOfStudy = await _databaseService.DeleteFieldOfStudies(id);
            if (fieldOfStudy == null)
            {
                return NotFound();
            }

            return View(fieldOfStudy);
        }

        // POST: FieldsOfStudies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            IActionResult result = View();
            try
            {
                var fieldOfStudies = await _databaseService.FieldOfStudiesDeleteConfirm(id);
                result = RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred: " + ex.Message;
                result = RedirectToAction(nameof(Index));
            }
            return result;
        }

        private bool FieldOfStudyExists(int id)
        {
            var result = _databaseService.CheckFieldOfStudiesExist(id);
            return result;
        }
    }
}