using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.EntityClient;
using System.Linq;
using System.Text;
using CorpIS.Task1.Lib.Model;
using CorpIS.Task1.TcpServer.Model.Map;

namespace CorpIS.Task1.TcpServer.Model
{
    public class EntityContext : DbContext
    {
        public EntityContext()
            : base("CorpISDatabase")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new CustomerMap());
        }

        public DbSet<Customer> Customers { get; set; }
    }
}
