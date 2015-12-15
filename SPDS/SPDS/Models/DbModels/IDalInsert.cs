using System.Collections.Generic;
using MSSQLModel.Exceptions;
namespace MSSQLModel
{
    /// <summary>
    /// An interface to insert data into the database.
    /// </summary>
    public interface IDalInsert
    {
        /// <summary>
        /// Insertion of a dataset
        /// </summary>
        /// <param name="dataPoints">The datapoints to connect with the dataset.</param>
        /// <param name="impactMaterial">The targetmaterial to connect with the dataset.</param>
        /// <param name="projectile">The projectilel to connect with the dataset.</param>
        /// <param name="orginalDataformat">The original dataformat to connect with the dataset.</param>
        /// <param name="converteDataformat">The converted dataformat to connect with the dataset, this can be the same as the original dataformat.</param>
        /// <param name="rev">The revision to connect with this dataset.</param>
        /// <param name="user">The user to connect with the revision.</param>
        /// <param name="prevRevision">A previous revision to connect with the new revision.</param>
        /// <param name="AR">The Article reference to connect with the dataset.</param>
        /// <param name="method">The method to connect with the dataset.</param>
        /// <param name="stateOfAggregation">The state of aggregation to connect with the dataset.</param>
        /// <example>
        ///     This example shows you how to insert a dataset
        ///     <code>
        ///     IDalInsert insert = new MSSQLModelDAL();
        ///     IDalRetrieve recv = new MSSQLModelDAL();
        ///     var dataPointList = new List[DataPoint]();
        ///     for (int i = 10; i >= 0; i--)
        ///     {
        ///         var datapoint = new DataPoint();
        ///         datapoint.EqEnergy = i * 50;
        ///         datapoint.StoppingPower = i * 20;
        ///         datapoint.ConvertetData = i * 50;
        ///         dataPointList.Add(datapoint);
        ///     }
        ///     var targetmat = recv.GetTargetMaterialByName("AG");
        ///     if (targetmat.Id == 0)
        ///         return;
        ///     
        ///     var projectile = recv.GetProjectileByName("H");
        ///     if (projectile.Id == 0)
        ///         return;
        ///     var format = recv.GetDataformatByNotation("myNotation");
        ///     if (format.Id == 0)
        ///         return;
        ///     var SOA = recv.GetStateOfAggregationByForm("S");
        ///     if (SOA.Id == 0)
        ///         return;
        ///     var article = recv.GetArticleReferences(new ParametersForArticelreferences() { DOINumber = "666" });
        ///     if (!article.Any())
        ///         return;
        ///     var method = recv.GetMethodByName("MyMethodName");
        ///     if (method.Id == 0)
        ///         return;
        ///     Revision revision = new Revision();
        ///     revision.Date = DateTime.Now;
        ///     revision.Comment = "There could be an error in ...";
        ///     var users = recv.GetUsers(new ParametersForUsers() { Email = "sumsar1812_1337@hotmail.com" });
        ///     if (!users.Any())
        ///         return;
        ///     try
        ///     {
        ///         insert.InsertDataset(dataPointList, targetmat, projectile, format, format, revision, users[0], null, article[0], method, SOA);;
        ///     }
        ///     catch (DALInfoNotSpecifiedException e)
        ///     {
        ///         // error handling
        ///     }
        ///     catch (DALAlreadyExistsException e)
        ///     {
        ///         // error handling
        ///     }                                                                                                                      
        /// 
        /// </code>
        /// </example>
        /// <exception cref="DALInfoNotSpecifiedException" >This exception is thrown if there wasn't specified enough information in some of the parameters</exception>
        /// <exception cref="DALAlreadyExistsException">This is thrown if the dataset Id already exists in the database</exception>
        /// <seealso cref="IDalRetrieve.GetUsers(ParametersForUsers)"/>
        /// <seealso cref="IDalRetrieve.GetArticleReferences(ParametersForArticelreferences)"/>
        /// <seealso cref="IDalRetrieve.GetDataformatByNotation(string)"/>
        /// <seealso cref="IDalRetrieve.GetProjectileByName(string )"/>
        /// <seealso cref="IDalRetrieve.GetTargetMaterialByName(string )"/>
        /// <seealso cref="IDalRetrieve.GetMethodByName(string )"/>
        /// <seealso cref="IDalRetrieve.GetStateOfAggregationByForm(string )"/>

        void InsertDataset(List<DataPoint> dataPoints, TargetMaterial impactMaterial,
            Projectile projectile, Dataformat orginalDataformat, Dataformat converteDataformat, Revision rev, User user, Revision prevRevision = null,
            ArticleReferences AR = null, Method method = null,
            StateOfAggregation stateOfAggregation = null);
        /// <summary>
        /// Insertion of a target material into the database
        /// </summary>
        /// <param name="material">The material to insert. The name of the material is required</param>
        /// <example>
        /// This example shows you how to insert a new target material into the database.
        /// <code>
        /// IDalInsert insert = new MSSQLModelDAL();
        /// var targetMaterial = new TargetMaterial();
        /// targetMaterial.Name = "AG";
        /// try
        /// {
        ///   insert.InsertTargetMaterial(targetMaterial);
        /// }
        /// catch(DALInfoNotSpecifiedException e)
        /// {
        ///   // error handling if needed
        /// }
        /// catch(DALAlreadyExistsException e)
        /// {
        ///   // error handling if needed
        /// }
        /// 
        /// </code>
        /// </example>
        /// <exception cref="DALAlreadyExistsException">Is thrown if the target material already exists in the database</exception>
        /// <exception cref="DALInfoNotSpecifiedException">Is thrown if the target material name was not specified</exception>

        void InsertTargetMaterial(TargetMaterial material);


        /// <summary>
        /// Insertion of a Projectile into the database.
        /// </summary>
        /// <param name="projectile">The projectile to insert into the database. Projectile name is required</param>
        /// <exception cref="DALInfoNotSpecifiedException">Is thrown if the projectile name was not specified</exception>
        /// <exception cref="DALAlreadyExistsException">Is thrown if the projectile already exists in the database</exception>
        /// <example>
        /// </example>
        void InsertProjectile(Projectile projectile);


        /// <summary>
        /// Insertion of a dataformat into the database.
        /// </summary>
        /// <param name="dataformat">The data format to insert into the database. Data notation is required.</param>
        /// <exception cref="DALInfoNotSpecifiedException">Is thrown if the data notation was not specified.</exception>
        /// <exception cref="DALAlreadyExistsException">Is thrown if the data format already exists in the database.</exception>
        /// <example>
        /// </example>
        void InsertDataFormat(Dataformat dataformat);


        /// <summary>
        /// Insertion of state of aggregation into the database.
        /// </summary>
        /// <param name="AG">The state of aggregation to insert into the database. Form is required.</param>
        /// <exception cref="DALInfoNotSpecifiedException">Is thrown if the form was not specified.</exception>
        /// <exception cref="DALAlreadyExistsException">Is thrown if the state of aggregation already exists in the database.</exception>
        /// <example>
        /// </example>
        void InsertStateOfAggregation(StateOfAggregation AG);

        /// <summary>
        /// Insertion of a method into the database.
        /// </summary>
        /// <param name="method">The method to insert into the database. Method name is required</param>
        /// <exception cref="DALInfoNotSpecifiedException">Is thrown if the method name was not specified.</exception>
        /// <exception cref="DALAlreadyExistsException">Is thrown if the method already exists in the database.</exception>
        /// <example>
        /// </example>
        void InsertMethod(Method method);

        /// <summary>
        /// Insertion of a new article reference
        /// </summary>
        /// <param name="articleReference">The article reference to insert into the database. </param>
        /// <exception cref="DALAlreadyExistsException">Is thrown if the DOI number specified already exists in the database.</exception>
        /// <example>
        /// </example>
        void InsertArticleReference(ArticleReferences articleReference);
    }
}

