
namespace Ecom.DAL.Configuration
{
    public class ProductImageUrlConfig : IEntityTypeConfiguration<ProductImageUrl>
    {
        public void Configure(EntityTypeBuilder<ProductImageUrl> builder)
        {
            builder.HasOne(p => p.Product)
                .WithMany(p => p.ProductImageUrls)
                .HasForeignKey(p => p.ProductId);
        }
    }
}
