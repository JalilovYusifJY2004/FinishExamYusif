using FinalExamYusif.Areas.Admin.ViewModels.Setting;
using FinalExamYusif.DAL;
using FinalExamYusif.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalExamYusif.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SettingController : Controller
    {
        private readonly AppDbContext _context;

        public SettingController(AppDbContext context)
        {
            _context = context;
        }
        public async  Task<IActionResult> Index()
        {
            List<Setting> settings = await _context.Settings.ToListAsync();
            return View(settings);
        }
        public async Task<IActionResult> Update(int id)
        {
            if (id<=0)
            {
                return BadRequest();
            }
            Setting setting= await _context.Settings.FirstOrDefaultAsync(x => x.Id == id);
            if (setting==null)
            {
                return NotFound();
            }
            UpdateSettingVM updateSettingVM = new UpdateSettingVM
            {
                Key = setting.Key,
                Value = setting.Value,
            };
            return View(updateSettingVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id ,UpdateSettingVM updateSettingVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Setting setting = await _context.Settings.FirstOrDefaultAsync(x => x.Id == id);
            if (setting == null)
            {
                return NotFound();
            }
            bool result= await _context.Settings.AnyAsync(x => x.Key.Trim().ToLower() == updateSettingVM.Key.Trim().ToLower());
            if (result)
            {
                ModelState.AddModelError("Key", "this key is exists");
                return View();
            }

            setting.Key= updateSettingVM.Value;
            setting.Value= updateSettingVM.Value;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }
    }
}
