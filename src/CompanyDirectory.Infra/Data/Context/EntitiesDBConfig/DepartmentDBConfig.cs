using CompanyDirectory.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyDirectory.Infra.Data.Context.EntitiesDBConfig;

internal sealed class DepartmentDBConfig : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ConfigureBaseEntity();

        builder
            .Property(department => department.Name)
            .HasMaxLength(150)
            .IsRequired();

        builder
            .Property(department => department.LocationId)
            .IsRequired();

        builder
            .HasMany<Personnel>()
            .WithOne(personnel => personnel.Department)
            .HasForeignKey(personnel => personnel.DepartmentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder 
            .Navigation(department => department.Location)
            .AutoInclude();
    }
}
