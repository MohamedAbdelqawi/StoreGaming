using GamePlay.Attributes;
using GamePlay.Settings;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GamePlay.ViewModels
{
    public class CreateGameFormVM
    {
        [MaxLength(250)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(2500)]
        public string Description { get; set; } = string.Empty;
        
        [Display(Name = "Category")]
        public int categoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }= Enumerable.Empty<SelectListItem>();
        [Display(Name ="Supported Devices")]
        public IEnumerable<SelectListItem> Devices { get; set; } = Enumerable.Empty<SelectListItem>();

        public List<int> SelectedDevices { get; set; } = default!;
        [AllowedExtensions(FileSettings.AllowedExtentions),
          MaxFileSize(FileSettings.MaxFileSizeInBytes)  ]
        public IFormFile Cover { get; set; } = default!;
    }
}
