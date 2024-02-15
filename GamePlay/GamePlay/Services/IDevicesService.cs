using Microsoft.AspNetCore.Mvc.Rendering;

namespace GamePlay.Services
{
    public interface IDevicesService
    {
        IEnumerable<SelectListItem> GetSelectList();

    }
}
