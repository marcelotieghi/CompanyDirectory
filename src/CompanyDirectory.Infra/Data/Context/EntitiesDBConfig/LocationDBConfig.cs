using CompanyDirectory.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyDirectory.Infra.Data.Context.EntitiesDBConfig;

internal sealed class LocationDBConfig : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.ConfigureBaseEntity();

        builder
            .Property(location => location.Name)
            .HasMaxLength(150)
            .IsRequired();

        builder
            .HasMany(location => location.DepartmentList)
            .WithOne(department => department.Location)
            .HasForeignKey(department => department.LocationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}