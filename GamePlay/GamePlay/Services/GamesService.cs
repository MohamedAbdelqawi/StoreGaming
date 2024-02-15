using Gameplay.Data;
using GamePlay.Models;
using GamePlay.Settings;
using GamePlay.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace GamePlay.Services
{
    public class GamesService : IGamesService

    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly String _imagespath;

        public GamesService(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _imagespath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagesPath}";
        }
        public IEnumerable<Game> GetAll()
        {
            return _context.Games.Include(g=>g.category).Include(g=>g.Devices).ThenInclude(c=>c.Device).AsNoTracking().ToList();
        }
        public async Task Create(CreateGameFormVM game)
        {

            var covername = await savecover(game.Cover);
           

            Game game1 = new Game()
            {
                Name = game.Name,
                Description = game.Description,
                categoryId = game.categoryId,
                Cover = covername,
                Devices = game.SelectedDevices.Select(x => new GameDevice { DeviceId = x }).ToList()

            };
            _context.Add(game1);
             _context.SaveChanges();
        }

        public Game? GetById(int id)
        {
            return _context.Games.Include(g => g.category).Include(g => g.Devices).ThenInclude(c => c.Device).AsNoTracking().SingleOrDefault(g=>g.Id==id);

        }

        public async Task<Game?> Update(EditFormGameVM model)
        {
            var game = _context.Games.Include(g=>g.Devices).SingleOrDefault(g=>g.Id==model.Id);
            if (game == null) return null;
            var oldcover = game.Cover;

            var HasNewCover = model.Cover is not null;

            game.Name = model.Name;
            game.Description = model.Description;
            game.categoryId = model.categoryId;
            game.Devices = model.SelectedDevices.Select(s => new GameDevice { DeviceId = s }).ToList();

            if (HasNewCover)
            {
                game.Cover = await savecover(model.Cover!);
            }

           var EffectedRows= _context.SaveChanges();
            if (EffectedRows > 0)
            {
                if (HasNewCover)
                {
                    var cover = Path.Combine(_imagespath, oldcover);
                    File.Delete(cover);
                }
                return game;
            }
            else
            {
                var cover = Path.Combine(_imagespath, game.Cover);
                File.Delete(cover);
                return null;
            };

        }
        public async Task<string> savecover(IFormFile model)
        {
            var covername = $"{Guid.NewGuid()}{Path.GetExtension(model.FileName)}";
            var path = Path.Combine(_imagespath, covername);

            using var stream = File.Create(path);
            await model.CopyToAsync(stream);
            return covername;
        }

        public bool Delete( int id)
        {
            var isDeleted = false;
            var game = _context.Games.Find(id);
            if(game is null)
            return isDeleted;

            _context.Games.Remove(game);
            var effectedRows = _context.SaveChanges();

            if (effectedRows > 0)
            {
                isDeleted = true;

                var cover = Path.Combine(_imagespath, game.Cover);
                File.Delete(cover);
            }

            return isDeleted;
        }

        
    }
}
