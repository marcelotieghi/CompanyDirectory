using CompanyDirectory.Core.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyDirectory.Infra.Data.Context.EntitiesDBConfig;

internal static class BaseEntityDBConfig
{
    public static void ConfigureBaseEntity<TEntity>(this EntityTypeBuilder<TEntity> builder)
        where TEntity : BaseEntity
    {
        builder.HasKey(entity => entity.Id);

        builder.HasQueryFilter(entity => !entity.IsDeleted);

        builder
            .HasIndex(entity => entity.IsDeleted)
            .HasFilter("IsDeleted = 0");

        //Audit
        builder
            .Property(audit => audit.CreatedBy)
            .IsRequired();
        builder
            .Property(audit => audit.CreatedOn)
            .IsRequired();
        builder
            .Property(audit => audit.UpdatedBy)
            .IsRequired();
        builder
            .Property(audit => audit.UpdatedOn)
            .IsRequired();
        builder
            .Property(audit => audit.IsDeleted)
            .IsRequired();
    }
}