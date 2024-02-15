using Gameplay.Data;
using GamePlay.Models;
using GamePlay.Services;
using GamePlay.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GamePlay.Controllers
{
    public class GamesController : Controller
    {
       
        private readonly ICategoriesService _categoriesService;
        private readonly IDevicesService _DevicesService;
        private readonly IGamesService _GamesServicese;

        public GamesController( ICategoriesService CategoriesService , IDevicesService DevicesService, IGamesService GamesService)
        {
            
            _categoriesService = CategoriesService;
            _DevicesService = DevicesService;
            _GamesServicese = GamesService;

        }

        [Authorize]
        public IActionResult Index()
        {
            var games = _GamesServicese.GetAll();
            return View(games);
        }
        public IActionResult Details(int id)
        {
            var game = _GamesServicese.GetById(id);
            if (game is null) return NotFound();
            return View(game);
        }
        [HttpGet]
        public IActionResult Create()
        {
            CreateGameFormVM vm = new()
            {
                Categories = _categoriesService.GetSelectList(),
           Devices= _DevicesService.GetSelectList()
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Create(CreateGameFormVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _categoriesService.GetSelectList();
                model.Devices = _DevicesService.GetSelectList();


                return View(model);
            }
          await  _GamesServicese.Create(model);

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var game=_GamesServicese.GetById(id);
            if (game is null) return NotFound();
            EditFormGameVM viewMode = new()
            {
                Id = id,
                Name = game.Name,
                Description = game.Description,
                categoryId = game.categoryId,
                SelectedDevices = game.Devices.Select(d => d.DeviceId).ToList(),
                Categories = _categoriesService.GetSelectList(),
                Devices = _DevicesService.GetSelectList(),
                currentcover = game.Cover
            };
            return View(viewMode);
        
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditFormGameVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _categoriesService.GetSelectList();
                model.Devices = _DevicesService.GetSelectList();


                return View(model);
            }
            var game = await _GamesServicese.Update(model);
            if (game is null)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Index));

        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var isDeleted= _GamesServicese.Delete(id);
            return isDeleted? Ok():BadRequest();
        }

    }
}
