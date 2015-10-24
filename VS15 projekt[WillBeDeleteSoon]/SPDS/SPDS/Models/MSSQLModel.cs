using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MSSQLModel.Exceptions;
using SPDS;

namespace MSSQLModel
{
    public class MSSQLModelDAL : IDalUserManagement, IDalGetData, IDalInsertData
    {
        public MSSQLModelDAL()
        {}


        public void InsertDataPoint(DataPoint dataP, Dataformat originalFormat,Dataformat convertedFormat)
        {
            if (dataP.EqEnergy == 0)
                throw new DALInfoNotSpecifiedException("Eq energy was not specified");
            if (dataP.StoppingPower == 0)
                throw new DALInfoNotSpecifiedException("Stopping power was not specified");

            using (var db = new TSPDSContext())
            {
                var query = from b in db.Dataformat where b == originalFormat select b;
                if (query.Any())
                {
                    query = from b in db.Dataformat where b == convertedFormat select b;
                    if (query.Any())
                    {
                        dataP.ConvertedDataformat = convertedFormat;
                        dataP.OriginalDataformat = originalFormat;
                        db.Entry(dataP).State = EntityState.Modified;
                        db.DataPoint.Add(dataP);
                        db.SaveChanges();
                    }
                }

            }
        }
        public void InsertRevision(Dataset dataset,Revision rev,User user,Revision prevRevision = null)
        {
            using (var db = new TSPDSContext())
            {
                if (user.UserId == 0)
                    throw new DALInfoNotSpecifiedException("User id was not specified");
                if (dataset.DatasetId == 0)
                    throw new DALInfoNotSpecifiedException("Dataset id was not specified");
                if (rev.RevId != 0)
                    throw new DALInfoNotSpecifiedException("Revision id was specified when it shouldn't");
                ValidateUser(user);
                if (prevRevision != null)
                    prevRevision.HeadRevision.Add(rev);
                rev.User = user;
                rev.Dataset = dataset;
                rev.UserUserId = user.UserId;
                rev.Dataset_DatasetId = dataset.DatasetId;
                db.Entry(rev).State = EntityState.Modified;
;               db.Revision.Add(rev);
                db.SaveChanges();
            }
            
        }
        public void InsertDataset(ICollection<DataPoint> dataPoints,TargetMaterial impactMaterial, Projectile projectile ,ArticleReferences AR = null,Method method = null)
        {
            if (impactMaterial.Id == 0)
                throw new DALInfoNotSpecifiedException("impactmaterial id was not specified");
            if (projectile.Id == 0)
                throw new DALInfoNotSpecifiedException("Projectile id was not specified");
            if (AR != null)
            {
                if (AR.ArticleReferencesId == 0)
                    throw new DALInfoNotSpecifiedException("ArtivleReference id was not specified");
            }
            if (method != null)
            {
                if (method.MethodId == 0)
                    throw new DALInfoNotSpecifiedException("Method id was not specified");
            }
            foreach(var item in dataPoints)
            {
                if (item.DatapointId == 0)
                    throw new DALInfoNotSpecifiedException("Datapoint id was not specified");
                if(item.OriginalDataformat == null)
                {
                    throw new DALInfoNotSpecifiedException(
                        "One or more DataPoints had unspecificed original dataformat");
                }
                if(item.ConvertetData == null)
                {
                    throw new DALInfoNotSpecifiedException(
                        "One or more DataPoints had unspecificed converted data");
                }
                if(item.ConvertedDataformat == null)
                {
                    throw new DALInfoNotSpecifiedException(
                        "One or more DataPoints had unspecificed converted dataformat");
                }
                if(item.EqEnergy == null)
                {
                    throw new DALInfoNotSpecifiedException("One or more DataPoints had unspecificed eqEnergy");
                }
            }
            var tempCollection = new List<ArticleReferences>();
            tempCollection.Add(AR);
            var dataset = new Dataset()
            {
                Projectile_Id = projectile.Id,
                TargetMaterial_Id = impactMaterial.Id,
                ArticleReferences  = tempCollection,
                Projectile = projectile,
                TargetMaterial = impactMaterial,
                DataPoint = dataPoints,
            };
            using (var db = new TSPDSContext())
            {
                db.Entry(dataset).State = EntityState.Modified;
                db.Dataset.Add(dataset);
                db.SaveChanges();
            }

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
                var query = from m in db.Dataformat where m.DataNotiation.ToLower() == dataformat.DataNotiation.ToLower() select m;
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
            if (method.Description == null)
                throw new DALInfoNotSpecifiedException("Method Description was not specified");
            using (var db = new TSPDSContext())
            {
                var query = from s in db.Method where s.Description.ToLower() == method.Description.ToLower() select s;
                if (query.Any())
                    throw new DALAlreadyExistsException("Mehod already exists");
                db.Method.Add(method);
                db.SaveChanges();
            }
        }
        public void InsertUser(User user,Permission perm)
        {
            ValidateUser(user);
            if (perm == null)
                throw new DALInfoNotSpecifiedException("User permission was not specified");
            if (perm.PermissionId == 0)
                throw new DALInfoNotSpecifiedException("Permission id was not speciified");
            using (var db = new TSPDSContext())
            {
                var query = from s in db.User where s.Email.ToLower() == user.Email.ToLower() select s;
                if (query.Any())
                    throw new DALAlreadyExistsException("User already exists");
                user.Permission = perm;
                user.PermissionPermissionId = perm.PermissionId;
                db.Entry(user).State = EntityState.Modified;
                db.User.Add(user);
                db.SaveChanges();
            }
        }

        public Permission GetPermById(int id)
        {
            using (var db = new TSPDSContext())
            {
                var query = from b in db.Permission where b.PermissionId == id select b;
                if (query.Any())
                {
                    Permission perm = query.ToList()[0];

                    return perm;
                }
                throw new DALOutOfRangeException("Perm id was out of range");
            }
        }

        public Dataset GetDatasetByRevision(int revId)
        {
            using (var db = new TSPDSContext())
            {
                var dataset = from b in db.Revision where b.RevId == revId select b.Dataset;
                if (dataset.Any())
                {
                    Dataset returnedDataset = dataset.ToList()[0];
                    return returnedDataset;
                }
                return new Dataset();
            }
        }
        public List<Dataset> GetAllDatasetsByFirstName(string firstName)
        {
            if (firstName == null)
                throw new DALInfoNotSpecifiedException("First name was not specified when getting d");
            var datasetList = new List<Dataset>();
            using (var db = new TSPDSContext())
            {
                var users = from b in db.User where b.FirstName.ToLower() == firstName.ToLower() select b;
                foreach (var user in users)
                {
                    var revisions = from b in db.Revision where b.User == user select b.HeadRevision;
                    foreach (var revision in revisions)
                    {
                        datasetList.Add(revision.ToList()[0].Dataset);
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
                var users = from b in db.User where b.LastName.ToLower() == lastName.ToLower() select b;
                foreach (var user in users)
                {
                    var revisions = from b in db.Revision where b.User == user select b.HeadRevision;
                    foreach (var revision in revisions)
                    {
                        datasetList.Add(revision.ToList()[0].Dataset);
                    }
                }
                return datasetList;
            }
        }
        public List<Dataset> GetAllDatasetsByFirstLastName(string firstName, string lastName)
        {
            if (firstName == null)
                throw new DALInfoNotSpecifiedException("First name was not specified when getting d");
            var datasetList = new List<Dataset>();
            using (var db = new TSPDSContext())
            {
                var users = from b in db.User where b.FirstName.ToLower() == firstName.ToLower()
                                && b.LastName.ToLower() == lastName.ToLower() select b;
                foreach (var user in users)
                {
                    var revisions = from b in db.Revision where b.User == user select b.HeadRevision;
                    foreach (var revision in revisions)
                    {
                        datasetList.Add(revision.ToList()[0].Dataset);
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
                var users = from b in db.User where b.Institute.ToLower() == institute.ToLower() select b;
                foreach (var user in users)
                {
                    var revisions = from b in db.Revision where b.User == user select b.HeadRevision;
                    foreach (var revision in revisions)
                    {
                        datasetList.Add(revision.ToList()[0].Dataset);
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
                var datasets = from b in db.Method where b.MethodId == method.MethodId select b.Dataset;
                if (datasets.Any())
                {
                    return datasets.ToList();
                }
                return new List<Dataset>();
            }
        }

        public List<Dataset> GetAllDatasetsByTargetMaterial(TargetMaterial material)
        {
            var datasetList = new List<Dataset>();
            using (var db = new TSPDSContext())
            {
                var datasets = from b in db.TargetMaterial where b.Name.ToLower() == material.Name.ToLower() select b;
                if (datasets.Any())
                {
                    foreach (var dataset in datasets)
                    {
                       datasetList.Add(dataset.Dataset.ToList()[0]);
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
                var datasets = from b in db.Projectile where b.Name.ToLower() == projectile.Name.ToLower() select b;
                if (datasets.Any())
                {
                    foreach (var dataset in datasets)
                    {
                        datasetList.Add(dataset.Dataset.ToList()[0]);
                    }
                    return datasetList;
                }
                return new List<Dataset>();
            }
        }
        public List<Revision> GetAllRevisionSets(Dataset dataset)
        {
            using (var db = new TSPDSContext())
            {
                var revs = from b in db.Revision select b;
                if (revs.Any())
                {
                    return revs.ToList();
                }
            }
            return new List<Revision>();
        }

        public TargetMaterial GetTargetMaterialByName(string name)
        {
            using (var db = new TSPDSContext())
            {
                var query = from b in db.TargetMaterial where b.Name.ToLower() == name.ToLower() select b;
                if (query.Any())
                {
                    return query.ToList()[0];
                }
                return new TargetMaterial();
            }
        }

        public List<TargetMaterial> GetAllTargetMaterialSets()
        {
            using (var db = new TSPDSContext())
            {
                var targetMaterials = from b in db.TargetMaterial select b;
                if (targetMaterials.Any())
                {
                    return targetMaterials.ToList();
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
            using (var db = new TSPDSContext())
            {
                var dataPoints = from b in db.DataPoint where b.Dataset == dataset select b;
                if (dataPoints.Any())
                {
                    return dataPoints.ToList();
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

        public List<Permission> GetAllPermissionSets()
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
