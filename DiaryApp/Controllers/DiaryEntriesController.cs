using DiaryApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DiaryApp.Controllers
{
    public class DiaryEntriesController: Controller
    {
        private readonly ApplicationDbContext _db;

        public DiaryEntriesController(ApplicationDbContext db){
            _db = db;
        }
        public IActionResult Index()
        {
            List<DiaryEntry> objDiaryEntryList = _db.DiaryEntries.ToList();
            return View(objDiaryEntryList);
        }

        public IActionResult Create(){
            return View();
        }

        [HttpPost]
        public IActionResult Create(DiaryEntry objDiaryEntry)
        {
            if (objDiaryEntry != null && objDiaryEntry.Title.Length < 3){
                //ModelState is property of controllers
                ModelState.AddModelError("Title","Title too short");
            }

            if(ModelState.IsValid){
                _db.DiaryEntries.Add(objDiaryEntry); //adds entry
                _db.SaveChanges();//saves to db
                return RedirectToAction("Index");
                //return RedirectToAction("Index", "Home"); for Home controller index page
            }
            //same page with the same content and we will get the rror message above shown
            return View(objDiaryEntry);
        }

        [HttpGet]
        public IActionResult Edit(int? id){

            if(id==null || id==0){
                return NotFound();
            }

            //? means can be nullable since id can be nullable
            DiaryEntry? objDiaryEntry = _db.DiaryEntries.Find(id);

            if(objDiaryEntry == null){
                return NotFound();
            }
            return View(objDiaryEntry);
        }

        [HttpPost]
        public IActionResult Edit(DiaryEntry? objDiaryEntry){

            if (objDiaryEntry != null && objDiaryEntry.Title.Length < 3){
                //ModelState is property of controllers
                ModelState.AddModelError("Title","Title too short");
            }

            if(ModelState.IsValid){
                _db.DiaryEntries.Update(objDiaryEntry); //adds entry
                _db.SaveChanges();//saves to db
                return RedirectToAction("Index");
                //return RedirectToAction("Index", "Home"); for Home controller index page
            }
            //same page with the same content and we will get the rror message above shown
            return View(objDiaryEntry);
        }

        [HttpGet]
        public IActionResult Delete(int id){
            if(id==null || id==0){
                return NotFound();
            }
            //? means can be nullable since id can be nullable
            DiaryEntry? objDiaryEntry = _db.DiaryEntries.Find(id);

            if(objDiaryEntry == null){
                return NotFound();
            }
            return View(objDiaryEntry);
        }

        [HttpPost]
        public IActionResult Delete(DiaryEntry? objDiaryEntry){
                _db.DiaryEntries.Remove(objDiaryEntry);
                _db.SaveChanges();//saves to db
                return RedirectToAction("Index");
                //return RedirectToAction("Index", "Home"); for Home controller index page
        }
    }
}