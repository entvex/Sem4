using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using MSSQLModel;
using SPDS.Models.DbModels;

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
            //IDALUserManagement dalUserManage = new MSSQLModelDAL();


            User user = new User();
            user.Email = email;
            user.Password = Pass;
            user.Institute = institution;
            user.FirstName = fName;
            user.LastName = lName;
            //Permission perm = dalUserManage.GetPermById(1);
            //dalUserManage.InsertUser(user,perm);







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
            //IDALUserManagement dalUserManage = new MSSQLModelDAL();

            //User user = dalUserManage.GetUserByEmail(_email);

            //if (user.Password == _pass)
            //{
                return true;
            //}

            //else
            //{
            //    return false;
            //}
        }
    }
}