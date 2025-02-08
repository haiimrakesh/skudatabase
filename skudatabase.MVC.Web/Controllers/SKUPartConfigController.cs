using Microsoft.AspNetCore.Mvc;
using skudatabase.domain.Infrastructure.Services;
using skudatabase.domain.Models;
using skudatabase.domain.Services;
using System.Threading.Tasks;

namespace skudatabase.web.Controllers
{
    public class SKUPartConfigController : Controller
    {
        private readonly ISKUPartConfigService _sKUPartConfigService;

        public SKUPartConfigController(ISKUPartConfigService sKUPartConfigService)
        {
            _sKUPartConfigService = sKUPartConfigService;
        }

        public async Task<IActionResult> Index()
        {
            var sKUPartConfigs = await _sKUPartConfigService.GetAllSKUPartConfigs();
            return View(sKUPartConfigs);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SKUPartConfig sKUPartConfig)
        {
            if (ModelState.IsValid)
            {
                await _sKUPartConfigService.AddSKUPartConfig(sKUPartConfig);
                return RedirectToAction(nameof(Index));
            }
            return View(sKUPartConfig);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var sKUPartConfig = await _sKUPartConfigService.GetSKUPartConfigById(id);
            if (sKUPartConfig == null)
            {
                return NotFound();
            }
            return View(sKUPartConfig);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _sKUPartConfigService.DeleteSKUPartConfig(id);
            return RedirectToAction(nameof(Index));
        }
    }
}