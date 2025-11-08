
namespace Ecom.DAL.Entity
{
    public class AppUser : IdentityUser
    {

        public string? Username { get; private set; }
        public string? Email { get; private set; }
        public string? PasswordHash { get; private set; }
        public string? Role { get; private set; } // e.g., "Admin", "Customer"

        public bool IsActive { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public DateTime? UpdatedOn { get; private set; }
        public string? UpdatedBy { get; private set; }

        // Navigation Properties
        public virtual ICollection<Address>? Addresses { get; private set; }
        public virtual ICollection<ProductReview>? ProductReviews { get; private set; }
        public virtual ICollection<WishlistItem>? WishlistItems { get; private set; }

        // Logic
        public AppUser() { }

        public AppUser(string username, string email, string passwordHash, string role)
        {
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
            Role = role;
            IsActive = true;
            CreatedOn = DateTime.UtcNow;
        }

        public bool Update(string username, string email, string role, string userModified)
        {
            if (!string.IsNullOrEmpty(userModified))
            {
                Username = username;
                Email = email;
                Role = role;
                UpdatedOn = DateTime.UtcNow;
                UpdatedBy = userModified;
                return true;
            }
            return false;
        }

    }
}
