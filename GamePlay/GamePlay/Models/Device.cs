
namespace GamePlay.Models
{
    public class Device
    {
        public int Id { get; set; }
        [MaxLength(250)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(50)]
        public string Iocn { get; set; } = string.Empty;
    }
}
