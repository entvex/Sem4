using System.Collections.Generic;
using SPDS;

namespace MSSQLModel
{
    public interface IDalInsertData
    {

        void InsertRevision(Dataset dataset,Revision rev,User user,Revision prevRevision = null);
        void InsertDataPoint(DataPoint dataP, Dataformat originalFormat, Dataformat convertedFormat);
        void InsertDataset(ICollection<DataPoint> dataPoints,TargetMaterial impactMaterial, Projectile projectile ,ArticleReferences AR = null,Method method = null);
        void InsertTargetMaterial(TargetMaterial material);
        void InsertProjectile(Projectile projectile);
        void InsertDataFormat(Dataformat dataformat);
        void InsertStateOfAggregation(StateOfAggregation AG);
        void InsertMethod(Method method);
        void InsertUser(User user,Permission perm);
    }
}