namespace SPDS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Dataset")]
    public partial class Dataset
    {
        public Dataset()
        {
            ArticleReferences = new HashSet<ArticleReferences>();
            DataPoint = new HashSet<DataPoint>();
            Method = new HashSet<Method>();
            StateOfAggregation = new HashSet<StateOfAggregation>();
            Revision = new HashSet<Revision>();
        }

        public int DatasetId { get; set; }

        public int TargetMaterial_Id { get; set; }

        public int Projectile_Id { get; set; }

        public virtual ICollection<ArticleReferences> ArticleReferences { get; set; }

        public virtual ICollection<DataPoint> DataPoint { get; set; }

        public virtual ICollection<Method> Method { get; set; }

        public virtual Projectile Projectile { get; set; }

        public virtual ICollection<StateOfAggregation> StateOfAggregation { get; set; }

        public virtual ICollection<Revision> Revision { get; set; }

        public virtual TargetMaterial TargetMaterial { get; set; }
    }
}
