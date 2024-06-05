//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FindMyBook.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class table_publisher
    {
        [Display(Name = "Publisher ID")]
        public int publisher_id { get; set; }

        [Required(ErrorMessage = "Publisher name field cannot be empty.")]
        [Display(Name = "Publisher name")]
        public string publisher_name { get; set; }

        [Required(ErrorMessage = "Publisher address field cannot be empty.")]
        [Display(Name = "Address")]
        public string address { get; set; }

        [Required(ErrorMessage = "Publisher country field cannot be empty.")]
        [Display(Name = "Country")]
        public string country { get; set; }

        [Required(ErrorMessage = "Publisher phone number field cannot be empty.")]
        [Display(Name = "Phone number")]
        public string publisher_phone_number { get; set; }

        [Required(ErrorMessage = "Publisher email address field cannot be empty.")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string publisher_email { get; set; }
    }
}