using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace PikAroomFB.Models
{
    public class Apply
    {
        [Key]

        public string ApplyId { get; set; }

        [Display(Name = "First Name(s)")]
        [Required(ErrorMessage = "First name(s) required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Your full name may not be longer than 2 characters")]
        public string FullName { get; set; }

        [Display(Name = "Surname")]
        [Required(ErrorMessage = "Surname required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Your surname may not be longer than 2 characters")]
        public string Surname { get; set; }

        [Display(Name = "Gender")]
        [Required(ErrorMessage ="Gender required")]
        public string Gender { get; set; }

        [Display(Name = "Date Of Birth")]

        [StringLength(200, MinimumLength = 2)]
        public string DateOfBirth { get; set; }

        [Display(Name = "Nationality")]
        [Required(ErrorMessage = "Nationality required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Your Nationality may not be longer than 2 characters")]
        public string Nationality { get; set; }

        [Display(Name = "ID number")]
        [Required(ErrorMessage = "ID Number required")]
        [StringLength(13, ErrorMessage = "Your ID number should be 13 digits")]
        public string IdNumber { get; set; }

        [Display(Name = "Race")]
        [Required(ErrorMessage = "Race required, choose your race")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Your Race may not be longer than 2 characters")]
        public string Race { get; set; }

        [Display(Name = "Your Email")]
        [Required(ErrorMessage = "Email is required")]
        [StringLength(100, MinimumLength = 2)]
        public string Email { get; set; }

        [Display(Name = "Room Type")]
        [Required(ErrorMessage = "Room type is required")]
        [StringLength(200, MinimumLength = 2)]
        public string RoomType { get; set; }

        [Display(Name = "Accomodation name")]
        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string AccomodationName { get; set; }

        [Display(Name = "Prefered Moving Date")]

        [StringLength(200, MinimumLength = 2)]
        public string MovingDate { get; set; }
    }
}