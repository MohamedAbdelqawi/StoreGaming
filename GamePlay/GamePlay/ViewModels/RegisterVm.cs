namespace GamePlay.ViewModels
{
    public class RegisterVm
    {
       
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        public string Address { get; set; }
    }
}
