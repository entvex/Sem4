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
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Display(Name = "Email")]
        public string _Email { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Compare("_Email",ErrorMessage = "Email does not match")]
        [Display(Name = "Email")]
        public string _confirmEmail { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [MinLength(8,ErrorMessage = "Password must be at least 8 characters long")]
        [MaxLength(19, ErrorMessage = "Password must be under characters long")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$", ErrorMessage ="Password must contains at least: one upper case letter, one lower case letter and one digit")]
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
        [Compare("_Pass",ErrorMessage ="Passwords does not match")]
        [DataType(DataType.Password)]
        [MinLength(8)]
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
        private string _passback;

        [Required]
        [Display(Name = "Email")]
        public string _Email { get; set; }

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

        /// <summary>
        /// Method Logs user into application if validated succesfully
        /// </summary>
        /// <param name="_email">User submitted email</param>
        /// <param name="_pass">User submitted password</param>
        /// <returns></returns>
        public LoginReturn Login(string _email, string _pass)
        {
            LoginReturn result = new LoginReturn();

            try
            {

                IDalUserManagement dalUserManage = new MSSQLModelDAL();

                List<User> users = dalUserManage.GetUsers(new ParametersForUsers()
                { Email = _email });
                User user = users.Find(x => x.Email == _email);


                if (_pass == user.Password)
                {
                    result.status = true;
                    result.message = "";
                    return result;
                }
                else
                {
                    result.status = false;
                    result.message = "Password is incorrect!";
                    return result;
                }
            }
            catch(NullReferenceException e)
            {
                result.status = false;
                result.message = String.Format("No user with email: {0} found",_email);
                return result;
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
    public class LoginReturn
    {
        public bool status
        {
            get; set;
        }
        public string message {get; set;}
    }
}