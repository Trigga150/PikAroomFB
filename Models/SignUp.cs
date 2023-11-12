using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PikAroomFB.Models
{
    public class SignUp
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Full name(s) and Surname")]
        public string Name { get; set; }
        [Required, Display(Name ="Email Adress")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        
        [Required, Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password",ErrorMessage ="Please Confirm Password!!!")]
        [DataType(DataType.Password)]
        [Required, Display(Name = "Cornfirm Password")]
        public string ConfirmPassword { get; set; }
    }
}