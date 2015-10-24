using System.Collections.Generic;
using SPDS;

namespace MSSQLModel
{
    public interface IDalGetData
    {
        Permission GetPermById(int id);
        Dataset GetDatasetByRevision(int revId);
        List<Dataset> GetAllDatasetsByFirstName(string firstName);
        List<Dataset> GetAllDatasetsByLastName(string lastName);
        List<Dataset> GetAllDatasetsByFirstLastName(string firstName, string lastName);
        List<Dataset> GetAllDatasetsByInstitute(string institute);
        List<Dataset> GetAllDatasets();
        List<Dataset> GetAllDatasetsByMethod(Method method);
        List<Dataset> GetAllDatasetsByTargetMaterial(TargetMaterial material);
        List<Dataset> GetAllDatasetsByProjectile(Projectile projectile);
        List<Revision> GetAllRevisionSets(Dataset dataset);
        TargetMaterial GetTargetMaterialByName(string name);
        List<TargetMaterial> GetAllTargetMaterialSets();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataset"></param>
        /// <returns></returns>
        List<DataPoint> GetDataPointsByDataSet(Dataset dataset);

        /// <summary>
        /// getting all methods
        /// </summary>
        /// <returns></returns>
        List<Method> GetAllMethods();

        List<Dataformat> GetAllDataFormats();
        List<Permission> GetAllPermissionSets();
        User GetUserByEmail(string email);
    }
}