using System.ComponentModel.DataAnnotations;

namespace TaskMaster.Web.Models
{
    public class UserAccount
    {
        [Key]
        public int UserId { get; set; }

       // [Required(ErrorMessage = "First Name required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name required.")]
        public string LastName { get; set; }

      //  [Required(ErrorMessage = "Email required.")]
        public string Email { get; set; }

       // [Required(ErrorMessage = "Password required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


    }
}