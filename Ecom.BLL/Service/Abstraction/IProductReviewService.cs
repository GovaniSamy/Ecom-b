using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.BLL.Service.Abstraction
{
    public interface IProductReviewService
    {
        Task<ServiceResponse<IEnumerable<ProductReviewGetVM>>> GetAllAsync();

        Task<ServiceResponse<ProductReviewGetVM>> GetByIdAsync(int id);
        Task<ServiceResponse<IEnumerable<ProductReviewGetVM>>> GetByProductIdAsync(int productId);

        Task<ServiceResponse<IEnumerable<ProductReviewGetVM>>> GetByUserIdAsync(string userId);
        Task<ServiceResponse<bool>> CreateAsync(ProductReviewCreateVM vm);
        Task<ServiceResponse<bool>> UpdateAsync(ProductReviewUpdateVM vm);
        Task<ServiceResponse<bool>> ToggleDeleteAsync(int id, string userModified);
    }
}
