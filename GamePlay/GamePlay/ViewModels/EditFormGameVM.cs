using GamePlay.Attributes;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GamePlay.ViewModels
{
    public class EditFormGameVM
    {
        public int Id { get; set; }
        [MaxLength(250)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(2500)]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Category")]
        public int categoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();
        [Display(Name = "Supported Devices")]
        public IEnumerable<SelectListItem> Devices { get; set; } = Enumerable.Empty<SelectListItem>();

        public List<int> SelectedDevices { get; set; } = default!;
        [AllowedExtensions(FileSettings.AllowedExtentions),
          MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile? Cover { get; set; } = default!;

       public string? currentcover { get; set; }
    }
}
