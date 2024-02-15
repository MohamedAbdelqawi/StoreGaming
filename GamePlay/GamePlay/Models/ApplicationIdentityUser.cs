using Microsoft.AspNetCore.Identity;


namespace GamePlay.Models
{
    public class ApplicationIdentityUser:IdentityUser
    {
        public string Address { get; set; }
    }
}
