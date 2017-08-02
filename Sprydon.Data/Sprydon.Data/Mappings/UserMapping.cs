using Sprydon.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprydon.Data.Mappings
{
    public class UserMapping: EntityTypeConfiguration<User>
    {
        public UserMapping()
        {
            ToTable("ApplicationUser");
            HasKey(t => t.Id);
            Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.RowVersion).HasColumnName("TimeStamp").IsRowVersion().IsConcurrencyToken(true);
            Property(t => t.Forename).HasColumnName("Forename");
            Property(t => t.Surname).HasColumnName("Surname");
            Property(t => t.CreatedBy).HasColumnName("CreatedBy").IsRequired();
            Property(t => t.EditedBy).HasColumnName("EditedBy").IsRequired();
            Property(t => t.Created).HasColumnName("Created").IsRequired();
            Property(t => t.LastEdited).HasColumnName("LastEdited").IsRequired();
        }
    }
}
