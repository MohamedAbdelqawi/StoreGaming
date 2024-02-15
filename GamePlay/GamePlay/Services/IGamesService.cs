using GamePlay.Models;
using GamePlay.ViewModels;

namespace GamePlay.Services
{
    public interface IGamesService
    {
        IEnumerable<Game> GetAll();
        Game? GetById(int id);
        Task Create (CreateGameFormVM game);
        Task<Game?> Update (EditFormGameVM game);
        bool Delete (int id);
    }
}
