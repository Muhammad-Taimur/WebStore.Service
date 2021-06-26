using EStore.Service.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EStore.Service.Configuration
{
    public class EncryptionConfiguration : IEntityTypeConfiguration<EncryptionTest>
    {

        public void Configure(EntityTypeBuilder<EncryptionTest> builder)
        {
            builder.HasKey(e => e.EncID);
            builder.Property(e => e.Name).HasMaxLength(4000);
        }
    }
}
