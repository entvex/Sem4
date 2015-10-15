using SPDS.Models.DbModels;

namespace MSSQLModel
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
            modelBuilder.Entity<ArticleReferences>()
                .HasMany(e => e.Dataset)
                .WithOptional(e => e.ArticleReferences)
                .HasForeignKey(e => e.ArticleReferences_Id);

            modelBuilder.Entity<Dataformat>()
                .HasMany(e => e.DataPoint)
                .WithRequired(e => e.ConvertedDataformat)
                .HasForeignKey(e => e.DataformatForConverted_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Dataformat>()
                .HasMany(e => e.DataPoint1)
                .WithRequired(e => e.OriginalDataformat)
                .HasForeignKey(e => e.DataformatForOriginal_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Dataset>()
                .HasMany(e => e.DataPoint)
                .WithRequired(e => e.Dataset)
                .HasForeignKey(e => e.DatasetDatasetId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Dataset>()
                .HasMany(e => e.Revision)
                .WithRequired(e => e.Dataset)
                .HasForeignKey(e => e.Dataset_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Method>()
                .HasMany(e => e.Dataset)
                .WithOptional(e => e.Method)
                .HasForeignKey(e => e.Method_Id);

            modelBuilder.Entity<Permission>()
                .HasMany(e => e.User)
                .WithRequired(e => e.Permission)
                .HasForeignKey(e => e.PermissionPermissionId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Projectile>()
                .HasMany(e => e.Dataset)
                .WithRequired(e => e.Projectile)
                .HasForeignKey(e => e.Projectile_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Revision>()
                .HasMany(e => e.PrevRevision)
                .WithOptional(e => e.HeadRevision)
                .HasForeignKey(e => e.HeadRevision_Id);

            modelBuilder.Entity<StateOfAggregation>()
                .HasMany(e => e.Dataset)
                .WithOptional(e => e.StateOfAggregation)
                .HasForeignKey(e => e.StateOfAggregation_Id);

            modelBuilder.Entity<TargetMaterial>()
                .HasMany(e => e.Dataset)
                .WithRequired(e => e.TargetMaterial)
                .HasForeignKey(e => e.TargetMaterial_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Revision)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.UserUserId);
        }
    }
}
