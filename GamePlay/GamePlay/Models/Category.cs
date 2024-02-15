
namespace GamePlay.Models
{
    public class Category
    {
        public int Id { get; set; }
        [MaxLength(250)]
        public string Name { get; set; } = string.Empty;
        public ICollection<Game>Games { get; set; } =new List<Game>();
    }
}
