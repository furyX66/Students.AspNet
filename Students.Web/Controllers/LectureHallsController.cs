using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Students.Common.Data;
using Students.Common.Models;
using Students.Interfaces;

namespace Students.Web.Controllers
{
    public class LectureHallsController : Controller
    {
        private readonly IDatabaseService _databaseService;


        public LectureHallsController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        // GET: LectureHalls
        public async Task<IActionResult> Index()
        {
            IActionResult result = View();
            var model = await _databaseService.IndexHall();
            result = View(model);
            return result;
        }

        //GET: LectureHalls/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var lectureHall = await _databaseService.DetailsHall(id);
            return View(lectureHall);
        }

        //GET: LectureHalls/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LectureHalls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HallNumber,Floor")] LectureHall lectureHall)
        {
            if (ModelState.IsValid)
            {
                lectureHall = await _databaseService.CreateHall(lectureHall);
                return RedirectToAction(nameof(Index));
            }
            return View(lectureHall);
        }

        // GET: LectureHalls/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var lectureHall = await _databaseService.EditHall(id);
            return View(lectureHall);
        }
        // POST: LectureHalls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HallNumber,Floor")] LectureHall lectureHall)
        {
            if (id != lectureHall.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    lectureHall = await _databaseService.EditHall(id, lectureHall);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LectureHallExists(lectureHall.Id))
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
            return View(lectureHall);
        }

        // GET: LectureHalls/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var lectureHall = await _databaseService.DeleteHall(id);
            return View(lectureHall);
        }

        // POST: LectureHalls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            IActionResult result = View();
            try
            {
                var hall = await _databaseService.DeleteHallConfirmed(id);
                result = RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught: " + ex.Message);
            }
            return result;
        }

        private bool LectureHallExists(int id)
        {
            var result = _databaseService.CheckHallExist(id);
            return result;
        }
    }
}
