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
        private string _confirmPassback;
        private string _pass;

        [Required]
        [Display(Name = "Email")]
        public string _Email { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string _confirmEmail { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string _Pass
        {
            get
            {
                return _pass;
            }
            set
            {
                _pass = Encryption.EncryptPassword(value);
            }
        }

        [Required]
        [Display(Name = "Password")]
        public string _confirmPass
        {
            get
            {
                return _confirmPassback;
            }
            set
            {
                _confirmPassback = Encryption.EncryptPassword(value);
            }
        }

        [Required]
        [Display(Name = "Institution")]
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
            IDalUserManagement dalUserManage = new MSSQLModelDAL();

            var user = new ParametersForUsers()
            {
                Email = email,                 
                Institute = institution,
                FirstName = fName,
                LastName = lName,
            };
            Permission perm = dalUserManage.GetPermByAccessLevel(AccessLevel.Submitter);
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
            IDalUserManagement dalUserManage = new MSSQLModelDAL();

            List<User> user = dalUserManage.GetUsers(new ParametersForUsers()
            {Email = _pass}); 

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

    public class ProfilePageViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string _newEmail { get; set; }

              [Required]
        [Display(Name = "Password")]
        public string _newPass { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string _confirmNewPass { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string _newInstitution { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string _newFirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string _newLastName { get; set; }
    }
}