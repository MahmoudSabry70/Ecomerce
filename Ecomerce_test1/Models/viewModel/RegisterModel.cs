using System.ComponentModel.DataAnnotations;

namespace Ecomerce_test1.Models.viewModel
{
    public class RegisterModel
    {
        public string Id { get; set; }

        [Required]
        public string UserName { get; set; }

      
        public string imge { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email   { get; set; }

        [Required]
        [DataType(DataType.Password)]
        
        public string Password { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public string userimg { get; set; }


    }
}
