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
        [Display(Name = "Email")]
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


        public void Create(string email, string confirmEmail, string Pass, string confirmPass, string institution, string fName, string lName)
        {
            /*var user = new UserSet();
            user.Email = email;
            user.Password = Pass;
            user.Institute = institution;
            user.FirstName = fName;
            user.LastName = lName;
            var db = new TSPDSEntities();
            var query = db.PermissionSet.Find(1);
            user.PermissionSet = query;
            db.UserSet.Add(user);
            db.SaveChanges();
             * */
        }
    }

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
          /*  try
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
            }*/

            return false;
        }
    }
}