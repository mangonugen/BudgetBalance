using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionDataModels;

namespace TransactionRepository
{
    public class TransactionDatabase : DbContext
    {
        public TransactionDatabase() : base("name=TransactionBalance") { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("dbo.Users");            
        }
    }

    //public class TransactionInitializer : CreateDatabaseIfNotExists<TransactionDatabase>
    //{
    //    protected override void Seed(TransactionDatabase context)
    //    {
    //        base.Seed(context);
    //    }
    //}
}
