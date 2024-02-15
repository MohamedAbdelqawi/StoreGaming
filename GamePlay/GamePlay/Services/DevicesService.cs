using Gameplay.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GamePlay.Services
{
    public class DevicesService : IDevicesService
    {
        private readonly ApplicationDbContext _context;

        public DevicesService(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<SelectListItem> GetSelectList()
        {
            return _context.Devices.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).OrderBy(x => x.Text).AsNoTracking().ToList();


        }
    }
}
