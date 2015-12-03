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
    public class MSSQLModelDAL : IDalInsert, IDalRetrieve, IDalUserManagement
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
        private void InsertRevision(Dataset dataset, Revision rev, User user, Revision prevRevision = null)
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
                var query = from b in db.Revision where b.Dataset.Id == dataset.Id select b;
                if (query.Any())
                    throw new DALAlreadyExistsException("Dataset already exists");
                rev.User = user;
                rev.Dataset = dataset;
                rev.UserUserId = user.Id;
                rev.Dataset_Id = dataset.Id;
                

                if (prevRevision != null)
                {
                    if (prevRevision.Id == 0)
                        throw new DALInfoNotSpecifiedException("Previous revision was not null, but the id was not specified");
                    prevRevision.HeadRevision = rev;
                    prevRevision.HeadRevision_Id = rev.Id;
                    rev.HeadRevision_Id = prevRevision.Id;
                    db.Entry(prevRevision).State = EntityState.Modified;
                }
                db.Entry(dataset).State = EntityState.Modified;            
                db.Entry(user).State = EntityState.Modified;;
                db.Revision.Add(rev);

                db.SaveChanges();
            }
        }

        public void InsertDataset(List<DataPoint> dataPoints, TargetMaterial impactMaterial,
            Projectile projectile, Dataformat orginalDataformat, Dataformat converteDataformat, Revision rev, User user, Revision prevRevision = null,
            ArticleReferences AR = null, Method method = null,
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
                Projectile = projectile,
                Method = method,
                StateOfAggregation = stateOfAggregation,
                TargetMaterial = impactMaterial,
            };
            if (AR != null) dataset.ArticleReferences_Id = AR.Id;
            if (method != null) dataset.Method_Id = method.Id;
            if (stateOfAggregation != null) dataset.StateOfAggregation_Id = stateOfAggregation.Id;
            using (var db = new TSPDSContext())
            {
                db.Entry(dataset).State = EntityState.Modified;
                db.Dataset.Add(dataset);
                db.SaveChanges();
            }
            InsertDataPoint(dataPoints, dataset, orginalDataformat, converteDataformat);
            InsertRevision(dataset, rev, user, prevRevision);
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
                    throw new DALAlreadyExistsException("The DOI number already exists");
                }
                db.ArticleReferences.Add(articleReference);
                db.SaveChanges();
            }
        }

        public void DeleteUser(User user)
        {
            if (user.Id == 0)
                return;
            user.FirstName = "DeletedUser" + user.Id.ToString();
            user.LastName = null;
            user.Institute = null;
            user.Password = Encryption.EncryptPassword(DateTime.Now.ToString());
            var perm = new Permission {Id = 1};
            user.Permission = perm;
            user.PermissionPermissionId = perm.Id;
            user.Picture = null;
            user.PhoneNumber = null;
            user.Email = null;
            using (var db = new TSPDSContext())
            {
                db.Entry(user).State = EntityState.Modified;
                db.User.AddOrUpdate(user);
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
            var param = new ParametersForUsers();
            param.Email = user.Email;
            var temp = GetUsers(param)[0];

            if (temp.Email != user.Email)
            {
                throw new DALInfoNotSpecifiedException("The user was not found in the databse");
            }

            using (var db = new TSPDSContext())
            {
                user.Permission = perm;
                user.PermissionPermissionId = perm.Id;
                db.User.AddOrUpdate(user);
                db.SaveChanges();
            }
        }

        public List<Revision> GetAllRevisonBydataset(Dataset dataset)
        {

            using (var db = new TSPDSContext())
            {
                var query = from b in db.Revision where b.Dataset.Id == dataset.Id select b;

                if (query.Any())
                {
                    query = query.Include(m => m.Dataset.Projectile);
                    query = query.Include(m => m.Dataset.Method);
                    query = query.Include(m => m.Dataset.TargetMaterial);
                    query = query.Include(m => m.Dataset.StateOfAggregation);
                    query = query.Include(m => m.Dataset.ArticleReferences);
                    query = query.Include(m => m.Dataset.DataPoint);
                    query = query.Include(m => m.Dataset.Revision);
                    return query.ToList();
                }
                return new List<Revision>();
            }
        }


        public Revision GetNewestRevisionByDataset(Dataset dataset)
        {
            using (var db = new TSPDSContext())
            {
                var query = from b in db.Revision where b.Dataset_Id == dataset.Id select b;
                if (query.Any())
                {
                    query = query.Include(m => m.Dataset.Projectile);
                    query = query.Include(m => m.Dataset.Method);
                    query = query.Include(m => m.Dataset.TargetMaterial);
                    query = query.Include(m => m.Dataset.StateOfAggregation);
                    query = query.Include(m => m.Dataset.ArticleReferences);
                    query = query.Include(m => m.Dataset.DataPoint);
                    query = query.Include(m => m.Dataset.Revision);

                    return query.ToList().Last();
                }
                    
                return new Revision();
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
        public Method GetMethodByName(string name)
        {
            using (var db = new TSPDSContext())
            {
                var query = from b in db.Method where b.Name == name select b;
                if (query.Any())
                {
                    return query.Single();
                }
                return new Method();
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

        private ArticleReferences GetArticleReferenceByDOI(string DOI)
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

        public Permission GetPermByAccessLevel(AccessLevel accessLevel)
        {
            using (var db = new TSPDSContext())
            {
                var query = from b in db.Permission where b.Id == (int) accessLevel select b;
                if (query.Any())
                {
                    Permission perm = query.ToList()[0];
                    return perm;
                }
                throw new DALOutOfRangeException("Perm id was out of range");
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

        public List<Dataset> GetDatasets(ParametersForDataset parameters)
        {
            using (var db = new TSPDSContext())
            {
                var query = db.Dataset.AsQueryable();
                if (!string.IsNullOrWhiteSpace(parameters.ProjectileName))
                    query = query.Where(x => x.Projectile.Name == parameters.ProjectileName);

                if (parameters.MethodId.HasValue)
                    query = query.Where(x => x.Method_Id == parameters.MethodId);

                if (parameters.ArticleReferencesId.HasValue)
                    query = query.Where(x => x.ArticleReferences_Id == parameters.ArticleReferencesId);

                if (parameters.StateOfAggregationId.HasValue)
                    query = query.Where(x => x.StateOfAggregation_Id == parameters.StateOfAggregationId);

                if (!string.IsNullOrWhiteSpace(parameters.FirstName))
                    query = query.Where(x => x.Revision.Any(s => s.User.FirstName == parameters.FirstName));

                if (!string.IsNullOrWhiteSpace(parameters.LastName))
                    query = query.Where(x => x.Revision.Any(s => s.User.LastName == parameters.LastName));

                if (!string.IsNullOrWhiteSpace(parameters.Institute))
                    query = query.Where(x => x.Revision.Any(s => s.User.Institute == parameters.Institute));

                if (parameters.RevId.HasValue)
                    query = query.Where(x => x.Revision.Any(s => s.Id == parameters.RevId));

                if (parameters.Approved.HasValue)
                    query = query.Where(x => x.Revision.Any(s => s.Approved == parameters.Approved));


                //TargetMaterial search options
                if (!string.IsNullOrWhiteSpace(parameters.TargetMaterialName))
                    query = query.Where(x => x.TargetMaterial.Name == parameters.TargetMaterialName);

                if (!string.IsNullOrWhiteSpace(parameters.TargetMaterialChemicalFormula))
                    query =
                        query.Where(x => x.TargetMaterial.ChemicalFormula == parameters.TargetMaterialChemicalFormula);

                if (!string.IsNullOrWhiteSpace(parameters.TargetMaterialICRUId))
                    query = query.Where(x => x.TargetMaterial.ICRUId == parameters.TargetMaterialICRUId);

                if (!string.IsNullOrWhiteSpace(parameters.TargetMaterialZCharge))
                    query = query.Where(x => x.TargetMaterial.ZCharge == parameters.TargetMaterialZCharge);

                if (parameters.TargetMaterialMass.HasValue)
                    query = query.Where(x => x.TargetMaterial.Mass == parameters.TargetMaterialMass);

                if (parameters.TargetMaterialMolarMass.HasValue)
                    query = query.Where(x => x.TargetMaterial.MolarMass == parameters.TargetMaterialMolarMass);

                if (parameters.ProjectileMass.HasValue)
                    query = query.Where(x => x.Projectile.Mass == parameters.ProjectileMass);

                if (!string.IsNullOrEmpty(parameters.ProjectilePDGNumber))
                    query = query.Where(x => x.Projectile.PDGNumber == parameters.ProjectilePDGNumber);

                if (parameters.ProjectilezCharge.HasValue)
                    query = query.Where(x => x.Projectile.zCharge == parameters.ProjectilezCharge);

                query = query.Include(m => m.Projectile);
                query = query.Include(m => m.Method);
                query = query.Include(m => m.TargetMaterial);
                query = query.Include(m => m.StateOfAggregation);
                query = query.Include(m => m.ArticleReferences);
                query = query.Include(m => m.DataPoint);
                query = query.Include(m => m.Revision);

                return query.ToList();
            }
        }

        public List<ArticleReferences> GetArticleReferences(ParametersForArticelreferences parameters)
        {
            using (var db = new TSPDSContext())
            {
                var query = db.ArticleReferences.AsQueryable();
                if (!string.IsNullOrWhiteSpace(parameters.FirstName))
                    query = query.Where(x => x.Firstname == parameters.FirstName);
                if (!string.IsNullOrWhiteSpace(parameters.LastName))
                    query = query.Where(x => x.Lastname == parameters.LastName);
                if (parameters.Year.HasValue)
                    query = query.Where(x => x.Year == parameters.Year);
                if (!string.IsNullOrWhiteSpace(parameters.DOINumber))
                    query = query.Where(x => x.DOINumber == parameters.DOINumber);

                return query.ToList();
            }
        }

        public List<User> GetUsers(ParametersForUsers parameters)
        {
            using (var db = new TSPDSContext())
            {
                var query = db.User.AsQueryable();
                if (!string.IsNullOrWhiteSpace(parameters.FirstName))
                    query = query.Where(x => x.FirstName.ToLower() == parameters.FirstName.ToLower());
                if (!string.IsNullOrWhiteSpace(parameters.LastName))
                    query = query.Where(x => x.LastName.ToLower() == parameters.LastName.ToLower());
                if (!string.IsNullOrWhiteSpace(parameters.Institute))
                    query = query.Where(x => x.Institute.ToLower() == parameters.Institute.ToLower());
                if (!string.IsNullOrWhiteSpace(parameters.Email))
                    query = query.Where(x => x.Email.ToLower() == parameters.Email.ToLower());
                if (!string.IsNullOrWhiteSpace(parameters.PhoneNumber))
                    query = query.Where(x => x.PhoneNumber == parameters.PhoneNumber);
                if (parameters.WaitingOnPromotion == true)
                    query = query.Where(x => x.Permission.Id == 2);
                query = query.Include(x => x.Permission);
                return query.ToList();
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