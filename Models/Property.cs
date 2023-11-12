using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PikAroomFB.Models
{
    public class Property
    {

        /******************************Property Model***************************/
        //Declair the Property variables
        //[Display(Name = "") provides the variable name in the user inputs

        [Key]

        public string PropertyId { get; set; }
        [Required(ErrorMessage = "Property name must not be less than 2 characters")]
        [StringLength(100, MinimumLength = 2)]
        [Display(Name = "Property Name")]
        public string PropertyName { get; set; }
        [Display(Name = "ERF Number")]
        [Required(ErrorMessage = "ERF Number is required")]
        [StringLength(100, MinimumLength = 2)]
        public string ERFNumber { get;set; }

        [Display(Name = "Type of Accommodation")]
        [Required(ErrorMessage = "Type of accomodation is required")]
        [StringLength(100, MinimumLength = 2)]
        public string Type { get; set; }

        [Display(Name = "Monthly Price")]
        [Required(ErrorMessage = "Bathroom image is required")]
        public double Price { get; set; }

        [Display(Name = "Rating")]
        [Required(ErrorMessage = "Bathroom image is required")]
        public int Rating { get; set; }

        //[Display(Name = "Available space")]
        //[Required(ErrorMessage = "You have to specify the number of beds available")]
        //[StringLength(100, MinimumLength = 2)]
        //public string AvailableSpace { get; set; }
       
        [Display(Name = "Description")]
        [Required(ErrorMessage = "Accommodation description is required")]
        [StringLength(100, MinimumLength = 2)]
        public string Description { get; set; }

        [Display(Name = "Profile Image")]
        [Required(ErrorMessage = "Profile image is required")]
        public string ProfileImagePath { get; set; }

        public string ImageName { get; set; }

        //public Login UserLogin { get; set; }
    }
}