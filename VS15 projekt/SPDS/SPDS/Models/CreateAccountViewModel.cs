using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SPDS.Models
{
    public class CreateAccountViewModel
    {
        [Required]
        [Display(Name ="Email")]
        public string _Email { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string _confirmEmail { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string _Pass { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string _confirmPass { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string _Institution { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string _FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string _LastName { get; set; }


        public void Create(string email, string confirmEmail,string Pass, string confirmPass,string institution, string fName, string lName)
        {
            var user = new User();
            user.Email = email;
            user.Password = Pass;
            user.Institute = institution;
            user.FirstName = fName;
            user.LastName = lName;
            var db = new TSPDSModelContainer();
            var query = db.PermissionSet.Find(1);
            db.UserSet.Add(user);
            db.SaveChanges();
        }
    }
}