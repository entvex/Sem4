using System.Collections.Generic;
using SPDS.Models.DbModels;

namespace MSSQLModel
{
    public interface IDALRetrieving
    {
        ArticleReferences GetArticleReferenceByDOI(string DOI);
        Permission GetPermById(int id);
        List<Revision> GetAllRevisonBydataset(Dataset dataset);
        Dataset GetDatasetByRevision(int revId);
        Revision GetNewestRevisionByDataset(Dataset dataset);
        List<Dataset> GetAllDatasetsByFirstName(string firstName);
        List<Dataset> GetAllDatasetsByLastName(string lastName);
        List<Dataset> GetAllDatasetsByFirstLastName(string firstName, string lastName);
        List<Dataset> GetAllDatasetsByInstitute(string institute);
        List<Dataset> GetAllDatasets();
        List<Dataset> GetAllDatasetsByMethod(Method method);
        List<Dataset> GetAllDatasetsByTargetMaterial(TargetMaterial material);
        List<Dataset> GetAllDatasetsByProjectile(Projectile projectile);
        List<Dataset> GetAllDatasetsByArticleReference(ArticleReferences articleReference);
        List<Dataset> GetAllDatasetsBystateOfAggregation(StateOfAggregation stateOfAggregation);
        Projectile GetProjectileByName(string name);
        TargetMaterial GetTargetMaterialByName(string name);
        List<TargetMaterial> GetAllTargetMaterials();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataset"></param>
        /// <returns></returns>
        List<DataPoint> GetDataPointsByDataSet(Dataset dataset);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<Method> GetAllMethods();

        List<StateOfAggregation> GetAllStateOfAggregation();
        StateOfAggregation GetStateOfAggregationByForm(string form);
        Method GetMethodByName(string name);
        List<ArticleReferences> GetAllArticleReferenceses();
        List<Dataformat> GetAllDataFormats();
        List<Permission> GetAllPermissions();
        Dataformat GetDataformatByNotation(string notation);
        User GetUserByEmail(string email);
        List<Projectile> GetallProjectiles();
    }
}