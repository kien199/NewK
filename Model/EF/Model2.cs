namespace Model.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model2 : DbContext
    {
        public Model2()
            : base("data source=DESKTOP-7REEAC9\\SQLEXPRESS;initial catalog=NewK;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework")
        {
        }

        public virtual DbSet<baiviet> baiviets { get; set; }
        public virtual DbSet<binhluan> binhluans { get; set; }
        public virtual DbSet<nguoidung> nguoidungs { get; set; }
        public virtual DbSet<theloaitin> theloaitins { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<baiviet>()
                .Property(e => e.slug)
                .IsUnicode(false);

            modelBuilder.Entity<baiviet>()
                .Property(e => e.thumbnail)
                .IsUnicode(false);

            modelBuilder.Entity<baiviet>()
                .HasMany(e => e.binhluans)
                .WithOptional(e => e.baiviet)
                .HasForeignKey(e => e.baiviet_id);

            modelBuilder.Entity<binhluan>()
                .Property(e => e.tennguoidang)
                .IsUnicode(false);

            modelBuilder.Entity<theloaitin>()
                .Property(e => e.slug)
                .IsUnicode(false);

            modelBuilder.Entity<theloaitin>()
                .HasMany(e => e.baiviets)
                .WithOptional(e => e.theloaitin)
                .HasForeignKey(e => e.theloai_id);
        }
        private void FixEfProviderServicesProblem()
        {
            // The Entity Framework provider type 'System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer'
            // for the 'System.Data.SqlClient' ADO.NET provider could not be loaded. 
            // Make sure the provider assembly is available to the running application. 
            // See http://go.microsoft.com/fwlink/?LinkId=260882 for more information.
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
    }
}
