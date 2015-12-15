using System.Collections.Generic;
using MSSQLModel.Exceptions;
using SPDS.Models.DbModels;

namespace MSSQLModel
{
    /// <summary>
    /// An interface to retrieve data from the database. 
    /// </summary>
    /// <remarks>As default this interface uses lazy loading. If the database doesn't contain any of the search parameters, in the get methods, an empty object or list will be returned</remarks></remarks>
    public interface IDalRetrieve
    {
        /// <summary>
        /// Get all the Revisons that belong to a specific Dataset. 
        /// </summary>
        /// 
        /// <param name="dataset">The specific dataset</param>
        /// <remarks>This method uses eager loading</remarks>
        /// <returns>A list of Datasets </returns>
        List<Revision> GetAllRevisonBydataset(Dataset dataset);

        /// <summary>
        /// Get the newest Revision by a specific Dataset.
        /// </summary>
        /// <remarks> This method uses eager loading</remarks>
        /// <param name="dataset">The specific Dataset</param>
        /// 
        /// <returns>The newest revision of the specific dataset</returns>
        Revision GetNewestRevisionByDataset(Dataset dataset);

        /// <summary>
        /// Get all Projectile currently in the database. 
        /// </summary>
        /// <returns>A list of Projectiles</returns>
        List<Projectile> GetallProjectiles();

        /// <summary>
        /// Get Projectile by by a specific name. 
        /// </summary>
        /// <param name="name">Name of the Projectile </param>
        /// <returns>A Projectile</returns>
        Projectile GetProjectileByName(string name);

        /// <summary>
        /// Get a specific StateOfAggregation by a specific name. 
        /// </summary>
        /// <param name="name">Name of the TargetMaterial</param>
        /// <returns>A TargetMaterial</returns>
        TargetMaterial GetTargetMaterialByName(string name);

        /// <summary>
        /// Get all TargetMaterials currently in the database.
        /// </summary>
        /// <returns>A list of TargetMaterials</returns>
        List<TargetMaterial> GetAllTargetMaterials();

        /// <summary>
        /// Get all DataPoints that belong to a specific Dataset.
        /// </summary>
        /// <remarks></remarks>
        /// <param name="dataset">The specific Dataset</param>
        /// <returns>A list of DataPoint</returns>
        List<DataPoint> GetDataPointsByDataSet(Dataset dataset);

        /// <summary>
        /// Get all Methods currently in the database.
        /// </summary>
        /// <remarks></remarks>
        /// <returns>A list of Methods</returns>
        List<Method> GetAllMethods();

        /// <summary>
        /// Get a specific StateOfAggregation by a specific name.
        /// </summary>
        /// <remarks></remarks>
        /// <param name="name"></param>
        /// <returns>A Method </returns>
        Method GetMethodByName(string name);

        /// <summary>
        /// Get all StateOfAggregations currently in the database.
        /// </summary>
        /// <remarks></remarks>
        /// <returns>A list of StateOfAggregations</returns>
        List<StateOfAggregation> GetAllStateOfAggregation();

        /// <summary>
        /// Get a specific StateOfAggregation.
        /// </summary>
        /// <remarks></remarks>
        /// <param name="form"></param>
        /// <returns>A StateOfAggregation</returns>
        StateOfAggregation GetStateOfAggregationByForm(string form);

        /// <summary>
        /// Get all ArticleReferences currently in the database.
        /// </summary>
        /// <remarks></remarks>
        /// <returns>A list with ArticleReferences</returns>
        List<ArticleReferences> GetAllArticleReferenceses();

        /// <summary>
        /// Get all the DataFormats currently in the database.
        /// </summary>
        /// <remarks></remarks>
        /// <returns>A list with DataFormats</returns>
        List<Dataformat> GetAllDataFormats();

        /// <summary>
        /// Get the Dataformat by a specific notation.
        /// </summary>
        /// <remarks></remarks>
        /// <param name="notation">The notation to retrieve</param>
        /// <returns>A Dataformat object</returns>
        Dataformat GetDataformatByNotation(string notation);

        /// <summary>
        /// Used to search for datasets 
        /// </summary>
        /// <remarks>This method uses eager loading</remarks>
        /// <param name="parameters">Search parameters</param>
        /// <returns>A dataset that matched</returns>
        List<Dataset> GetDatasets(ParametersForDataset parameters);

        /// <summary>
        /// Used to get ArticleReferences
        /// </summary>
        /// <remarks></remarks>
        /// <param name="parameters">A set of parameters for the search</param>
        /// <example>
        /// This sample shows how to get ArticleReferences that match a given DOI
        /// <code>
        /// public void GetAspecificDOI()
        /// {
        ///    IDalRetrieve recv = new MSSQLModelDAL(); 
        ///    var result = recv.GetArticleReferences(new ParametersForArticelreferences() { DOINumber = "1337" });
        /// if (result.Any())
        ///    {
        ///        //use list of Article references here
        ///    }
        /// }
        /// }
        /// </code>
        /// </example>
        /// <returns>A list of ArticleReferences</returns>
        List<ArticleReferences> GetArticleReferences(ParametersForArticelreferences parameters);

        /// <summary>
        /// Used to search for users 
        /// </summary>
        /// <remarks></remarks>
        /// <param name="parameters">Search parameters is passed to the user</param>
        /// <returns>A list of Users that matched the search query</returns>
        /// <example>
        /// This sample shows how to get a all Users.        
        /// <code>
        /// public void GetAllUsers()
        /// {
        /// IDalRetrieve dal = new MSSQLModelDAL();
        /// var AllUsers = new ParametersForUsers();
        /// var result = dal.GetUsers(AllUsers);
        /// if (result.Any())
        ///    {
        ///        //use list of users here
        ///    }
        /// }
        /// </code>
        /// <example>
        /// This sample shows how to get a user with a specific email.
        /// </example>
        /// <code>
        /// public void GetUserWithSpecificMail()
        /// {
        ///    IDalRetrieve dal = new MSSQLModelDAL();
        ///    var result = dal.GetUsers(new ParametersForUsers() { Email = "bassler@phys.au.dk" });
        ///    if (result.Any())
        ///    {
        ///        //use list of users here
        ///    }
        /// }
        /// </code>
        /// </example>
        List<User> GetUsers(ParametersForUsers parameters);
    }
}