using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using MSSQLModel.Exceptions;
using SPDS.Models.DbModels;

namespace MSSQLModel
{


    public class MSSQLModelDAL : IDALUserManagement, IDALInsertion, IDALRetrieving
    {
        public MSSQLModelDAL()
        {
        }


        private void InsertDataPoint(List<DataPoint> dataPoints, Dataset dataset, Dataformat originalFormat,
            Dataformat convertedFormat)
        {
            using (var db = new TSPDSContext())
            {
                var query = from b in db.Dataformat
                    where b.Id == originalFormat.Id
                    where b.Id == convertedFormat.Id
                    select b;

                if (query.Any())
                {
                    foreach (var dataPoint in dataPoints)
                    {
                        dataPoint.DataformatForConverted_Id = convertedFormat.Id;
                        dataPoint.DataformatForOriginal_Id = originalFormat.Id;
                        dataPoint.ConvertedDataformat = convertedFormat;
                        dataPoint.OriginalDataformat = originalFormat;
                        dataPoint.Dataset = dataset;
                        dataPoint.DatasetDatasetId = dataset.Id;
                        db.Entry(dataPoint).State = EntityState.Modified;
                        db.DataPoint.Add(dataPoint);
                    }
                    db.SaveChanges();
                }
            }
        }

        public void InsertRevision(Dataset dataset, Revision rev, User user, Revision prevRevision = null)
        {
            using (var db = new TSPDSContext())
            {
                
                if (user.Id == 0)
                    throw new DALInfoNotSpecifiedException("User id was not specified");
                if (dataset.Id == 0)
                    throw new DALInfoNotSpecifiedException("Dataset id was not specified");
                if (rev.Id != 0)
                    rev.Id = 0;
                ValidateUser(user);
                if (prevRevision != null)
                    prevRevision.HeadRevision = rev;
                else
                {
                    var query = from b in db.Revision where b.Dataset.Id == dataset.Id select b;
                    if (query.Any())
                        throw new DALAlreadyExistsException(
                            "Dataset already exists, be sure to include a previous revision if you want to add a new revision to this dataset");
                }
                rev.User = user;
                rev.Dataset = dataset;
                rev.UserUserId = user.Id;
                rev.Dataset_Id = dataset.Id;
                
                if (prevRevision != null)
                {
                    rev.HeadRevision_Id = prevRevision.Id;
                }
                db.Entry(rev).State = EntityState.Modified;
                db.Revision.Add(rev);
                db.SaveChanges();
            }
        }

        public void InsertDataset(List<DataPoint> dataPoints, TargetMaterial impactMaterial,
            Projectile projectile, Dataformat orginalDataformat, Dataformat converteDataformat, ArticleReferences AR = null, Method method = null,
            StateOfAggregation stateOfAggregation = null)
        {
            if (impactMaterial.Id == 0)
                throw new DALInfoNotSpecifiedException("impactmaterial id was not specified");
            if (projectile.Id == 0)
                throw new DALInfoNotSpecifiedException("Projectile id was not specified");
            if (AR != null)
            {
                if (AR.Id == 0)
                    throw new DALInfoNotSpecifiedException("ArtivleReference id was not specified");
            }
            if (method != null)
            {
                if (method.Id == 0)
                    throw new DALInfoNotSpecifiedException("Method id was not specified");
            }
            if (stateOfAggregation != null)
            {
                if (stateOfAggregation.Id == 0)
                {
                    throw new DALInfoNotSpecifiedException("State Of Aggregation Id was not specified");
                }
            }
            foreach (var item in dataPoints)
            {
                if (item.ConvertetData == null)
                {
                    throw new DALInfoNotSpecifiedException(
                        "One or more DataPoints had unspecificed converted data");
                }
                if (item.EqEnergy == null)
                {
                    throw new DALInfoNotSpecifiedException("One or more DataPoints had unspecificed eqEnergy");
                }
                if (item.StoppingPower == null)
                {
                    throw new DALInfoNotSpecifiedException("One or more DataPoints had unspecificed StoppingPower");
                }
            }
            var tempCollection = new List<ArticleReferences>();
            tempCollection.Add(AR);
            var dataset = new Dataset()
            {
                Projectile_Id = projectile.Id,
                TargetMaterial_Id = impactMaterial.Id,
                ArticleReferences = AR,
                ArticleReferences_Id = AR.Id,
                Projectile = projectile,
                Method = method,
                Method_Id = method.Id,
                StateOfAggregation = stateOfAggregation,
                StateOfAggregation_Id = stateOfAggregation.Id,
                TargetMaterial = impactMaterial,
            };
            using (var db = new TSPDSContext())
            {
                db.Entry(dataset).State = EntityState.Modified;
                db.Dataset.Add(dataset);
                db.SaveChanges();
            }

            //using (var db = new TSPDSContext())
            //{
            //    AR.DatasetArticleReferences_ArticleReferences_Id = dataset.Id;
            //    //tempCollection.Add(AR);
            //    //dataset.ArticleReferences = tempCollection;
            //    //b.Entry(dataset).State = EntityState.Modified;
            //    db.Dataset.AddOrUpdate(dataset);
            //}

            InsertDataPoint(dataPoints, dataset, orginalDataformat, converteDataformat);
        }

        public void InsertTargetMaterial(TargetMaterial material)
        {
            if (material.Name == null)
            {
                throw new DALInfoNotSpecifiedException("Material name was not specified");
            }
            using (var db = new TSPDSContext())
            {
                var query = from m in db.TargetMaterial where m.Name.ToLower() == material.Name.ToLower() select m;
                if (query.Any())
                {
                    throw new DALAlreadyExistsException("Material already excist");
                }
                db.TargetMaterial.Add(material);
                db.SaveChanges();
            }
        }

        public void InsertProjectile(Projectile projectile)
        {
            if (projectile.Name == null)
            {
                throw new DALInfoNotSpecifiedException("projectile name was not specified");
            }
            using (var db = new TSPDSContext())
            {
                var query = from m in db.Projectile where m.Name.ToLower() == projectile.Name.ToLower() select m;
                if (query.Any())
                {
                    throw new DALAlreadyExistsException("projectile already excist");
                }
                db.Projectile.Add(projectile);
                db.SaveChanges();
            }
        }

        public void InsertDataFormat(Dataformat dataformat)
        {
            if (dataformat.Description == null)
                throw new DALInfoNotSpecifiedException("DataFormat description was not specified");
            if (dataformat.DataNotiation == null)
                throw new DALInfoNotSpecifiedException("DataFormat notation was not specified");
            using (var db = new TSPDSContext())
            {
                var query = from m in db.Dataformat
                    where m.DataNotiation.ToLower() == dataformat.DataNotiation.ToLower()
                    select m;
                if (query.Any())
                {
                    throw new DALAlreadyExistsException("Dataformat already excist");
                }
                db.Dataformat.Add(dataformat);
                db.SaveChanges();
            }
        }

        public void InsertStateOfAggregation(StateOfAggregation AG)
        {
            if (AG.Form == null)
            {
                throw new DALInfoNotSpecifiedException("state of aggregation Form was not specified");
            }
            using (var db = new TSPDSContext())
            {
                var query = from s in db.StateOfAggregation where s.Form.ToLower() == AG.Form.ToLower() select s;
                if (query.Any())
                    throw new DALAlreadyExistsException("state of aggregation already exists");
                db.StateOfAggregation.Add(AG);
                db.SaveChanges();
            }
        }

        public void InsertMethod(Method method)
        {
            if (method.Name == null)
                throw new DALInfoNotSpecifiedException("Method name was not specified");

            using (var db = new TSPDSContext())
            {
                var query = from s in db.Method where s.Name.ToLower() == method.Name.ToLower() select s;
                if (query.Any())
                    throw new DALAlreadyExistsException("Mehod already exists");
                db.Method.Add(method);
                db.SaveChanges();
            }
        }

        public void InsertUser(User user, Permission perm)
        {
            ValidateUser(user);
            ValidatePermission(perm);
            using (var db = new TSPDSContext())
            {
                var query = from s in db.User where s.Email.ToLower() == user.Email.ToLower() select s;
                if (query.Any())
                    throw new DALAlreadyExistsException("User already exists");
                user.Permission = perm;
                user.PermissionPermissionId = perm.Id;
                db.Entry(user).State = EntityState.Modified;
                db.User.Add(user);
                db.SaveChanges();
            }
        }

        public void InsertArticleReference(ArticleReferences articleReference)
        {
            using (var db = new TSPDSContext())
            {
                var query = from b in db.ArticleReferences where b.DOINumber == articleReference.DOINumber select b;
                if (query.Any())
                {
                    throw new DALAlreadyExistsException("The DOI nummber already exists");
                }
                db.ArticleReferences.Add(articleReference);
                db.SaveChanges();
            }
        }


        public void UpdateUserPermission(User user, Permission perm)
        {
            ValidateUser(user);
            ValidatePermission(perm);
            if (perm == null)
                throw new DALInfoNotSpecifiedException("User permission was not specified");
            if (perm.Id == 0)
                throw new DALInfoNotSpecifiedException("Permission id was not speciified");
            if (user.Id == 0)
                throw new DALInfoNotSpecifiedException("User id was not speciified");

            var temp = GetUserByEmail(user.Email);

            if (temp.Email != user.Email)
            {
                throw new DALInfoNotSpecifiedException("The user was not found in the databse");
            }

            using (var db = new TSPDSContext())
            {
                user.Permission = perm;
                user.PermissionPermissionId = perm.Id;
                db.Entry(user).State = EntityState.Modified;
                db.User.AddOrUpdate(user);
                db.SaveChanges();
            }
        }

        public ArticleReferences GetArticleReferenceByDOI(string DOI)
        {
            using (var db = new TSPDSContext())
            {
                var query = from b in db.ArticleReferences where b.DOINumber == DOI select b;

                if (query.Any())
                {
                    return query.Single();
                }
                return new ArticleReferences();
            }
        }

        public Permission GetPermById(int id)
        {
            using (var db = new TSPDSContext())
            {
                var query = from b in db.Permission where b.Id == id select b;
                if (query.Any())
                {
                    Permission perm = query.ToList()[0];

                    return perm;
                }
                throw new DALOutOfRangeException("Perm id was out of range");
            }
        }

        public List<Revision> GetAllRevisonBydataset(Dataset dataset)
        {
            if (dataset.Id == 0)
                throw new DALInfoNotSpecifiedException("The dataset id was not specified");

            using (var db = new TSPDSContext())
            {
                var query = from b in db.Revision where b.Dataset.Id == dataset.Id select b;

                if (query.Any())
                {
                    return query.ToList();
                }
                return new List<Revision>();
            }
        }

        public Dataset GetDatasetByRevision(int revId)
        {
            using (var db = new TSPDSContext())
            {
                var dataset = from b in db.Revision where b.Id == revId select b.Dataset;
                if (dataset.Any())
                {
                    Dataset returnedDataset = dataset.ToList()[0];
                    return returnedDataset;
                }
                return new Dataset();
            }
        }

        public Revision GetNewestRevisionByDataset(Dataset dataset)
        {
            using (var db = new TSPDSContext())
            {
                var query = from b in db.Revision where b.Dataset_Id == dataset.Id select b;
                if (query.Any())
                    return query.ToList().Last();
                return new Revision();
            }
        }


        public List<Dataset> GetAllDatasetsByFirstName(string firstName)
        {
            if (firstName == null)
                throw new DALInfoNotSpecifiedException("First name was not specified when getting d");
            var datasetList = new List<Dataset>();
            using (var db = new TSPDSContext())
            {
                var revisions = from b in db.User where b.FirstName.ToLower() == firstName.ToLower() select b.Revision;
                foreach (var revision in revisions.ToList())
                {
                    foreach (var dataset in revision)
                    {
                        datasetList.Add(dataset.Dataset);
                    }
                }

                return datasetList;
            }
        }

        public List<Dataset> GetAllDatasetsByLastName(string lastName)
        {
            if (lastName == null)
                throw new DALInfoNotSpecifiedException("First name was not specified when getting d");
            var datasetList = new List<Dataset>();
            using (var db = new TSPDSContext())
            {
                var revisions = from b in db.User where b.LastName.ToLower() == lastName.ToLower() select b.Revision;

                foreach (var revision in revisions.ToList())
                {
                    foreach (var dataset in revision)
                    {
                        datasetList.Add(dataset.Dataset);
                    }
                }
            }
            return datasetList;
        }

        public List<Dataset> GetAllDatasetsByFirstLastName(string firstName, string lastName)
        {
            if (firstName == null)
                throw new DALInfoNotSpecifiedException("First name was not specified when getting d");
            var datasetList = new List<Dataset>();
            using (var db = new TSPDSContext())
            {
                var revisions = from b in db.User
                    where b.FirstName.ToLower() == firstName.ToLower()
                    where b.LastName.ToLower() == lastName.ToLower()
                    select b.Revision;

                foreach (var revision in revisions.ToList())
                {
                    foreach (var dataset in revision)
                    {
                        datasetList.Add(dataset.Dataset);
                    }
                }

                return datasetList;
            }
        }

        public List<Dataset> GetAllDatasetsByInstitute(string institute)
        {
            if (institute == null)
                throw new DALInfoNotSpecifiedException("First name was not specified when getting institute");
            var datasetList = new List<Dataset>();
            using (var db = new TSPDSContext())
            {
                var revisions = from b in db.User where b.Institute.ToLower() == institute.ToLower() select b.Revision;
                foreach (var revision in revisions.ToList())
                {
                    foreach (var dataset in revision)
                    {
                        datasetList.Add(dataset.Dataset);
                    }
                }
                return datasetList;
            }
        }

        public List<Dataset> GetAllDatasets()
        {
            using (var db = new TSPDSContext())
            {
                var datasets = from b in db.Dataset select b;
                if (datasets.Any())
                {
                    return datasets.ToList();
                }
                return new List<Dataset>();
            }
        }

        public List<Dataset> GetAllDatasetsByMethod(Method method)
        {
            using (var db = new TSPDSContext())
            {
                var datasets = from b in db.Method where b.Id == method.Id select b.Dataset;
                if (datasets.Any())
                {
                    List<Dataset> listOfDatasets = new List<Dataset>();
                    foreach (var dataset in datasets)
                    {
                        if (dataset.Any())
                        {
                            listOfDatasets.Add(dataset.ToList()[0]);
                        }
                    }
                    return listOfDatasets;
                }
                return new List<Dataset>();
            }
        }

        public List<Dataset> GetAllDatasetsByTargetMaterial(TargetMaterial material)
        {
            var datasetList = new List<Dataset>();
            using (var db = new TSPDSContext())
            {
                var datasets = from b in db.Dataset
                    where b.TargetMaterial.Name.ToLower() == material.Name.ToLower()
                    select b;
                if (datasets.Any())
                {
                    foreach (var dataset in datasets)
                    {
                        datasetList.Add(dataset);
                    }
                    return datasetList;
                }
                return new List<Dataset>();
            }
        }

        public List<Dataset> GetAllDatasetsByProjectile(Projectile projectile)
        {
            var datasetList = new List<Dataset>();
            using (var db = new TSPDSContext())
            {
                var datasets = from b in db.Dataset
                    where b.Projectile.Name.ToLower() == projectile.Name.ToLower()
                    select b;
                if (datasets.Any())
                {
                    foreach (var dataset in datasets)
                    {
                        datasetList.Add(dataset);
                    }
                    return datasetList;
                }
                return new List<Dataset>();
            }
        }

        public List<Dataset> GetAllDatasetsByArticleReference(ArticleReferences articleReference)
        {
            var datasetList = new List<Dataset>();
            using (var db = new TSPDSContext())
            {
                var datasets = from b in db.Dataset
                               where b.ArticleReferences.Id == articleReference.Id
                               select b;
                if (datasets.Any())
                {
                    return datasets.ToList();
                }
                return new List<Dataset>();
            }
        }
        public List<Dataset> GetAllDatasetsBystateOfAggregation(StateOfAggregation stateOfAggregation)
        {
            var datasetList = new List<Dataset>();
            using (var db = new TSPDSContext())
            {
                var datasets = from b in db.Dataset
                               where b.StateOfAggregation_Id == stateOfAggregation.Id
                               select b;
                if (datasets.Any())
                {
                    return datasets.ToList();
                }
                return new List<Dataset>();
            }
        }

        public Projectile GetProjectileByName(string name)
        {
            using (var db = new TSPDSContext())
            {
                var query = from b in db.Projectile where b.Name.ToLower() == name.ToLower() select b;
                if (query.Any())
                {
                    return query.Single();
                }
                return new Projectile();
            }
        }

        public TargetMaterial GetTargetMaterialByName(string name)
        {
            using (var db = new TSPDSContext())
            {
                var query = from b in db.TargetMaterial where b.Name.ToLower() == name.ToLower() select b;
                if (query.Any())
                {
                    return query.Single();
                }
                return new TargetMaterial();
            }
        }

        public List<TargetMaterial> GetAllTargetMaterials()
        {
            using (var db = new TSPDSContext())
            {
                var query = from b in db.TargetMaterial select b;
                if (query.Any())
                {
                    return query.ToList();
                }
                return new List<TargetMaterial>();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataset"></param>
        /// <returns></returns>
        public List<DataPoint> GetDataPointsByDataSet(Dataset dataset)
        {
            var datapointsList = new List<DataPoint>();
            using (var db = new TSPDSContext())
            {
                var dataPoints = from b in db.Dataset where b.Id == dataset.Id select b.DataPoint;
                if (dataPoints.Any())
                {
                    return dataPoints.ToList()[0].ToList();
                }
                return new List<DataPoint>();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Method> GetAllMethods()
        {
            using (var db = new TSPDSContext())
            {
                var methods = from b in db.Method select b;
                if (methods.Any())
                {
                    return methods.ToList();
                }
                return new List<Method>();
            }
        }

        public List<StateOfAggregation> GetAllStateOfAggregation()
        {
            using (var db = new TSPDSContext())
            {
                var query = from b in db.StateOfAggregation select b;

                if (query.Any())
                {
                    return query.ToList();
                }
                return new List<StateOfAggregation>();
            }
        }

        public StateOfAggregation GetStateOfAggregationByForm(string form)
        {
            using (var db = new TSPDSContext())
            {
                var query = from b in db.StateOfAggregation where b.Form.ToLower() == form.ToLower() select b;
                if (query.Any())
                {
                    return query.Single();
                }
                return new StateOfAggregation();
            }
        }

        public Method GetMethodByName(string name)
        {
            using (var db = new TSPDSContext())
            {
                var query = from b in db.Method select b;
                if (query.Any())
                {
                    return query.Single();
                }
                return new Method();
            }
        }

        public List<ArticleReferences> GetAllArticleReferenceses()
        {
            using (var db = new TSPDSContext())
            {
                var query = from b in db.ArticleReferences select b;

                if (query.Any())
                {
                    return query.ToList();
                }
                return new List<ArticleReferences>();
            }
        }


        public List<Dataformat> GetAllDataFormats()
        {
            using (var db = new TSPDSContext())
            {
                var dataformats = from b in db.Dataformat select b;
                if (dataformats.Any())
                {
                    return dataformats.ToList();
                }
                return new List<Dataformat>();
            }
        }

        public List<Permission> GetAllPermissions()
        {
            using (var db = new TSPDSContext())
            {
                var perms = from b in db.Permission select b;
                if (perms.Any())
                {
                    return perms.ToList();
                }
                return new List<Permission>();
            }
        }

        public Dataformat GetDataformatByNotation(string notation)
        {
            using (var db = new TSPDSContext())
            {
                var query = from b in db.Dataformat where b.DataNotiation.ToLower() == notation.ToLower() select b;
                if (query.Any())
                {
                    return query.Single();
                }
                return new Dataformat();
            }
        }

        public User GetUserByEmail(string email)
        {
            using (var db = new TSPDSContext())
            {
                var query = from b in db.User where b.Email.ToLower() == email.ToLower() select b;
                if (query.Any())
                    return query.ToList()[0];
                return new User();
            }
        }

        public List<Projectile> GetallProjectiles()
        {
            using (var db = new TSPDSContext())
            {
                var query = from b in db.Projectile select b;
                if (query.Any())
                {
                    return query.ToList();
                }
                return new List<Projectile>();
            }
        }

        private void ValidatePermission(Permission perm)
        {
            using (var db = new TSPDSContext())
            {
                var query = from b in db.Permission where b.Id == perm.Id select b;
                if (!query.Any())
                    throw new DALOutOfRangeException("Permission id is out of range");
            }
        }
        private void ValidateUser(User user)
        {
            if (user.Email == null)
                throw new DALInfoNotSpecifiedException("User email was not specified");
            if (user.Password == null)
                throw new DALInfoNotSpecifiedException("User password was not specified");
            if (user.FirstName == null)
                throw new DALInfoNotSpecifiedException("User Firstname was not specified");
            if (user.LastName == null)
                throw new DALInfoNotSpecifiedException("User Lastname was not specified");
        }
    }
}