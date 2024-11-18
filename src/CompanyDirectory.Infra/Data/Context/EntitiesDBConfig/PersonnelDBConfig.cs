using CompanyDirectory.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyDirectory.Infra.Data.Context.EntitiesDBConfig;

internal sealed class PersonnelDBConfig : IEntityTypeConfiguration<Personnel>
{
    public void Configure(EntityTypeBuilder<Personnel> builder)
    {
        builder.ConfigureBaseEntity();

        builder
            .Property(personnel => personnel.FirstName)
            .HasMaxLength(150)
            .IsRequired();
        builder
            .Property(personnel => personnel.LastName)
            .HasMaxLength(150)
            .IsRequired();
        builder
            .Property(personnel => personnel.Email)
            .HasMaxLength(150)
            .IsRequired();
        builder
            .Property(personnel => personnel.JobTitle)
            .HasMaxLength(150)
            .IsRequired();
        builder
            .Property(personnel => personnel.DepartmentId)
            .IsRequired();

        builder
            .Navigation(personnel => personnel.Department)
            .AutoInclude();
    }
}