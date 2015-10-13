using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SPDS.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string _Email { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string _Pass { get; set; }

        public bool Login(string _email, string _pass)
        {
            try
            {
                using (var db = new TSPDSContext())
                {
                    var query = db.User.Where(u => u.Email == _email && u.Password == _pass);

                    var user = query.Single<User>();

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }



    }
}