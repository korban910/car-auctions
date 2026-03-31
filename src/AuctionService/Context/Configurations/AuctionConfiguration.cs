using AuctionService.Common.Enums;
using AuctionService.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuctionService.Context.Configurations;

public class AuctionConfiguration : IEntityTypeConfiguration<Auction>
{
    public void Configure(EntityTypeBuilder<Auction> builder)
    {
        CreateAuctionsTable(builder);
    }

    private static void CreateAuctionsTable(EntityTypeBuilder<Auction> builder)
    {
        builder.ToTable("Auctions");
        
        builder.HasKey(a => a.Id);

        builder.Property(a => a.ReservePrice);
        
        builder.Property(a => a.Seller);
        
        builder.Property(a => a.Winner);
        
        builder.Property(a => a.SoldAmount);
        
        builder.Property(a => a.CurrentHighBid);
        
        builder.Property(a => a.CreatedAt);
        
        builder.Property(a => a.UpdatedAt);
        
        builder.Property(a => a.AuctionEndedAt);
        
        builder.Property(a => a.Status)
            .HasConversion(
                type => type.Value,
                value => Status.FromValue(value));
        
        builder.HasOne(a => a.Item)
            .WithOne(a => a.Auction)
            .HasForeignKey<Item>(a => a.AuctionId);
    }
}