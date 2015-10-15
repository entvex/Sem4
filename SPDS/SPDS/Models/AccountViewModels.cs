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

        /// <summary>
        /// Method creates user and inserts it into database
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="confirmEmail">user email confirmed</param>
        /// <param name="Pass">user password</param>
        /// <param name="confirmPass">user password confirmed</param>
        /// <param name="institution">user institute</param>
        /// <param name="fName">user first name</param>
        /// <param name="lName">user last name</param>
        public void Create(string email, string confirmEmail, string Pass, string confirmPass, string institution, string fName, string lName)
        {

            //USE IDALUSERMANAGEMENT TO CREATE USER - ASK RASMUS / DAVID

           /* User user = new User();
            user.Email = email;
            user.Password = Pass;
            user.Institute = institution;
            user.FirstName = fName;
            user.LastName = lName;
            var db = new TSPDSEntities();
            var query = db.PermissionSet.Find(1);
            user.PermissionSet = query;
            db.UserSet.Add(user);
            db.SaveChanges();*/



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

        /// <summary>
        /// Method Logs user into application if validated succesfully
        /// </summary>
        /// <param name="_email">User submitted email</param>
        /// <param name="_pass">User submitted password</param>
        /// <returns></returns>
        public bool Login(string _email, string _pass)
        {
          /* USE IDALUSERMANAGEMENT TO LOG USER IN - ASK RASMUS / DAVID  
          
            
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
            }*/

            return true;
        }
    }
}