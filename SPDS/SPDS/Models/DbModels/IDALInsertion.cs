using System.Collections.Generic;
using SPDS.Models.DbModels;

namespace MSSQLModel
{
    public interface IDALInsertion
    {
        void InsertRevision(Dataset dataset, Revision rev, User user, Revision prevRevision = null);

        void InsertDataset(List<DataPoint> dataPoints, TargetMaterial impactMaterial,
            Projectile projectile, Dataformat orginalDataformat, Dataformat converteDataformat, ArticleReferences AR = null, Method method = null,
            StateOfAggregation stateOfAggregation = null);

        void InsertTargetMaterial(TargetMaterial material);
        void InsertProjectile(Projectile projectile);
        void InsertDataFormat(Dataformat dataformat);
        void InsertStateOfAggregation(StateOfAggregation AG);
        void InsertMethod(Method method);
        void InsertArticleReference(ArticleReferences articleReference);
        void UpdateUserPermission(User user, Permission perm);
    }
}