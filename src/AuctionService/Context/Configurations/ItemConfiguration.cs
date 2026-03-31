using AuctionService.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuctionService.Context.Configurations;

public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        CreateItemsTable(builder);
    }

    private static void CreateItemsTable(EntityTypeBuilder<Item> builder)
    {
       builder.ToTable("Items");
       
       builder.HasKey(i => i.Id);

       builder.Property(i => i.Make);
       
       builder.Property(i => i.Model);
       
       builder.Property(i => i.Year);
       
       builder.Property(i => i.Color);
       
       builder.Property(i => i.Mileage);
       
       builder.Property(i => i.ImageUrl);
    }
}