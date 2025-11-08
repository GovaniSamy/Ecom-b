
namespace Ecom.DAL.Configuration
{
    public class ProductReviewConfig : IEntityTypeConfiguration<ProductReview>
    {
        public void Configure(EntityTypeBuilder<ProductReview> builder)
        {
            builder.HasOne(r => r.Product)
                .WithMany(p => p.ProductReviews)
                .HasForeignKey(r => r.ProductId);

            builder.HasOne(r => r.AppUser)
                .WithMany(u => u.ProductReviews)
                .HasForeignKey(r => r.AppUserId);
        }

    }
}
