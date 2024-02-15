using Microsoft.AspNetCore.Mvc.Rendering;

namespace GamePlay.Services
{
    public interface ICategoriesService
    {

        IEnumerable<SelectListItem> GetSelectList();
    }
}
