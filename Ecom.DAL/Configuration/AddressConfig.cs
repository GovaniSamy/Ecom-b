
namespace Ecom.DAL.Configuration
{
    public class AddressConfig : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasOne(a => a.AppUser)
                .WithMany(u => u.Addresses)
                .HasForeignKey(a => a.AppUserId);
        }
    }
}
