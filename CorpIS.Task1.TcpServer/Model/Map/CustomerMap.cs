using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using CorpIS.Task1.Lib.Model;

namespace CorpIS.Task1.TcpServer.Model.Map
{
    public class CustomerMap : EntityTypeConfiguration<Customer>
    {
        public CustomerMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(100);
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Table & Column Mappings
            this.ToTable("CUSTOMER");
            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.Name).HasColumnName("NAME");
            this.Property(t => t.Balance).HasColumnName("BALANCE");
        }
    }
}
