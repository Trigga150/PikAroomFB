using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PikAroomFB.Models
{
    public class Feedback
    {
        [Key]

        public string FeedbackId { get; set; }

        [Display(Name = "Full name and surname")]
        [Required(ErrorMessage = "Fullname required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Your full name may not be longer than 2 characters")]
        public string FullName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required")]
        [StringLength(100, MinimumLength = 2)]
        public string Email { get; set; }

        [Display(Name = "Message")]
        [Required(ErrorMessage = "Message is required")]
        [StringLength(200, MinimumLength = 2)]
        public string Message { get; set; }

        [Display(Name = "Response Message")]
        
        [StringLength(200, MinimumLength = 2)]
        public string ResponseMessage { get; set; }



    }
}