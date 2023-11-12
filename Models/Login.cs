using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace PikAroomFB.Models
{
    public class Login
    {
        [Key]
        public int Id { get; set; }
        
        [Required, Display(Name = "Email Adress")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Required, Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }
    }
}