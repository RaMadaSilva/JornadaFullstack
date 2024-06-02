using Finances.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finances.Api.Data.Mappings;

public class TransactionMapping : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder
            .ToTable(nameof(Transaction));

        builder
            .HasKey(t => t.Id);

        builder
            .Property(x => x.Id)
            .IsRequired()
            .HasColumnName("Id")
            .HasColumnType("BIGINT");

        builder
            .Property(x=>x.Title)
            .IsRequired()
            .HasColumnName("Title")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80);

        builder
            .Property(x => x.CreateAt)
            .IsRequired()
            .HasColumnName("CreateAt")
            .HasColumnType("DATETIME2");

        builder
            .Property(x => x.PaidOrReceivedAt)
            .IsRequired(false)
            .HasColumnName("PaidOrReceivedAt")
            .HasColumnType("DATETIME2");

        builder
            .Property(x => x.Type)
            .IsRequired()
            .HasColumnName("Type")
            .HasColumnType("SMALLINT");

        builder
            .Property(x => x.Amount)
            .IsRequired()
            .HasColumnName("Amount")
            .HasColumnType("MONEY")
            .HasPrecision(10,2);

        builder.Property(x => x.UserId)
            .IsRequired()
            .HasColumnName("UserId")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(160);
    }
}
