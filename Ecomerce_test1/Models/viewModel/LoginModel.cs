using System.ComponentModel.DataAnnotations;

namespace Ecomerce_test1.Models.viewModel
{
    public class LoginModel
    {
        [Required]
        public string UserName { get; set; }

       

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool isPersist { get; set; }

    }
}
