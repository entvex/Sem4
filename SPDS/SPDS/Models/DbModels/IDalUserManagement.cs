using System;
using System.Collections.Generic;
using MSSQLModel.Exceptions;
using SPDS.Models.DbModels;

namespace MSSQLModel
{
    public enum AccessLevel { Submitter = 1,Reviewer = 3};
    /// <summary>
    /// An interface to retrieve,insert and deleting users from the database.
    /// </summary>
    /// <remarks>As default this interface uses lazy loading. If the database doesn't contain any of the search parameters, in the get methods, an empty object or list will be returned</remarks>
    public interface IDalUserManagement
    {
 
        /// <summary>
        /// Deletes a specific user permanently! 
        /// </summary>
        /// <param name="user">The user to delete</param>
        void DeleteUser(User user);


        /// <summary>
        /// Get all the DataFormats currently in the database.
        /// </summary>
        /// <returns>A list with all permissions</returns>
        List<Permission> GetAllPermissions();


        /// <summary>
        /// Get a specific permission.
        /// </summary>
        /// <param name="accessLevel">The accessLevel of the permission</param>
        /// <returns>A Permission</returns>
        /// <exception cref="DALOutOfRangeException">Is thrown if the specified accessLevel is not found in the database</exception>
        Permission GetPermByAccessLevel(AccessLevel accessLevel);



        /// <summary>
        /// Used to search for users 
        /// </summary>
        /// <param name="parameters">User parameters to search after</param>
        /// <returns>A user that matched the search query</returns>
        /// <example>
        /// This sample shows how to get a all Users.
        /// </example>
        /// <code>
        /// public void GetAllUsers()
        /// {
        /// IDalRetrieve dal = new MSSQLModelDAL();
        /// var AllUsers = new ParametersForUsers();
        /// var result = dal.GetUsers(AllUsers);
        /// }
        /// </code>
        /// <example>
        /// This sample shows how to get a user with a specific email.
        /// </example>
        /// <code>
        /// public void GetUserWithSpecificMail()
        /// {
        ///     IDalRetrieve dal = new MSSQLModelDAL();
        ///     var result = dal.GetUsers(new ParametersForUsers() { Email = "bassler@phys.au.dk" });
        ///     if (result.Any())
        ///     {
        ///        //use list of users here
        ///    }
        /// }
        /// }
        /// </code>
        List<User> GetUsers(ParametersForUsers parameters);



        /// <summary>
        /// Insertion of a new user into the database.
        /// </summary>
        /// <param name="user">The user to insert into the database. Institute is optional</param>
        /// <param name="perm">The user permission. Permission Id is required</param>
        /// <exception cref="DALInfoNotSpecifiedException">Is thrown if some of the user information was not specified or the permission id was not specified.</exception>
        /// <exception cref="DALAlreadyExistsException">Is thrown if the user already exists in the database.<see cref="UpdateUserPermission"/></exception>
        /// <example>
        /// <code>
        /// IDalUserManagement dal = new MSSQLModelDAL();
        /// var myUser = new User() { Email = "Email@science.com", Password = 123, Institute = "ASE", FirstName = "Rasmus", LastName = "Bach" };
        /// var perm = dal.GetPermByAccessLevel(Reviewer);
        /// if (perm.Id != 1)
        ///   return;
        /// try{
        ///   insert.InsertUser(myUser,perm)
        /// }
        /// catch(DALInfoNotSpecifiedException e)
        /// {
        ///    //error handling
        /// }
        /// catch(DALAlreadyExistsException e)
        /// {
        ///   // error handling
        /// }
        /// </code>
        /// </example>
        void InsertUser(User user, Permission perm);


        /// <summary>
        /// Updates a existing User with a new Permission
        /// </summary>
        /// <param name="user">The existing User</param>
        /// <param name="perm">The new Permission</param>
        /// <exception cref="DALInfoNotSpecifiedException">Is thrown if some of the user information was not specified or the permission id was not specified or the user don't exists.</exception>
        /// <seealso cref="IDalRetrieve.GetUsers(ParametersForUsers)"/>
        void UpdateUserPermission(User user, Permission perm);
    }
}