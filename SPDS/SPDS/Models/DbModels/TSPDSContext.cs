namespace SPDS
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TSPDSContext : DbContext
    {
        public TSPDSContext()
            : base("name=TSPDSContext")
        {
        }

        public virtual DbSet<ArticleReferences> ArticleReferences { get; set; }
        public virtual DbSet<Dataformat> Dataformat { get; set; }
        public virtual DbSet<DataPoint> DataPoint { get; set; }
        public virtual DbSet<Dataset> Dataset { get; set; }
        public virtual DbSet<Method> Method { get; set; }
        public virtual DbSet<Permission> Permission { get; set; }
        public virtual DbSet<Projectile> Projectile { get; set; }
        public virtual DbSet<Revision> Revision { get; set; }
        public virtual DbSet<StateOfAggregation> StateOfAggregation { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<TargetMaterial> TargetMaterial { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dataformat>()
                .HasMany(e => e.DataPoint)
                .WithRequired(e => e.OriginalDataformat)
                .HasForeignKey(e => e.DataformatForConverted_FormatId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Dataformat>()
                .HasMany(e => e.DataPoint1)
                .WithRequired(e => e.ConvertedDataformat)
                .HasForeignKey(e => e.DataformatForOriginal_FormatId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Dataset>()
                .HasMany(e => e.ArticleReferences)
                .WithRequired(e => e.Dataset)
                .HasForeignKey(e => e.DatasetArticleReferences_ArticleReferences_DatasetId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Dataset>()
                .HasMany(e => e.DataPoint)
                .WithRequired(e => e.Dataset)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Dataset>()
                .HasMany(e => e.Method)
                .WithOptional(e => e.Dataset)
                .HasForeignKey(e => e.DatasetMethod_Method_DatasetId);

            modelBuilder.Entity<Dataset>()
                .HasMany(e => e.StateOfAggregation)
                .WithOptional(e => e.Dataset)
                .HasForeignKey(e => e.DatasetStateOfAggregation_StateOfAggregation_DatasetId);

            modelBuilder.Entity<Dataset>()
                .HasMany(e => e.Revision)
                .WithRequired(e => e.Dataset)
                .HasForeignKey(e => e.Dataset_DatasetId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Permission>()
                .HasMany(e => e.User)
                .WithRequired(e => e.Permission)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Projectile>()
                .HasMany(e => e.Dataset)
                .WithRequired(e => e.Projectile)
                .HasForeignKey(e => e.Projectile_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Revision>()
                .HasMany(e => e.HeadRevision)
                .WithRequired(e => e.PrevRevision)
                .HasForeignKey(e => e.PrevRevision_RevId);

            modelBuilder.Entity<TargetMaterial>()
                .HasMany(e => e.Dataset)
                .WithRequired(e => e.TargetMaterial)
                .HasForeignKey(e => e.TargetMaterial_Id)
                .WillCascadeOnDelete(false);
        }
    }
}
