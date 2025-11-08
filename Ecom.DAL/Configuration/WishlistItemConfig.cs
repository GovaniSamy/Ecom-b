
namespace Ecom.DAL.Configuration
{
    public class WishlistItemConfig : IEntityTypeConfiguration<WishlistItem>
    {
        public void Configure(EntityTypeBuilder<WishlistItem> builder)
        {
            builder.HasOne(w => w.AppUser)
                .WithMany(u => u.WishlistItems)
                .HasForeignKey(w => w.AppUserId);

            builder.HasOne(w => w.Product)
                .WithMany(p => p.WishlistItems)
                .HasForeignKey(w => w.ProductId);
        }

    }
}
