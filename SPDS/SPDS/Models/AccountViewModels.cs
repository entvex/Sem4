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
        private string _passback;

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
                return _passback;
            }
            set
            {
                _passback = Encryption.EncryptPassword(value);
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
        public bool Create(string email, string confirmEmail, string pass, string confirmPass, string institution, string fName, string lName)
        {
            //validate user input
            if (email != confirmEmail)
            {
                //user email doesn't match
                return false;
            }

            if (pass != confirmPass)
            {
                //user pass doesn't match
                return false;
            }
            try
            {
                IDalUserManagement dalUserManage = new MSSQLModelDAL();
                User user = new User()
                {
                    Email = email,
                    FirstName = fName,
                    LastName = lName,
                    Institute = institution,
                    Password = pass
                };
                Permission perm = dalUserManage.GetPermByAccessLevel(AccessLevel.Submitter);
                dalUserManage.InsertUser(user, perm);
                return true;
            }
            catch (NullReferenceException e)
            {
                
                return false;
            }
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

            List<User> users = dalUserManage.GetUsers(new ParametersForUsers()
            {Email = _email});
            User user = users.Find(x => x.Email == _email);
            String passback = Encryption.EncryptPassword(_pass);
            String passback2 = Encryption.EncryptPassword(user.Password);

            if (passback == _pass)
            {
                return true;
            }
            else
            {
                return false;
            }
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