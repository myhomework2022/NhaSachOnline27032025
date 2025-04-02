using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NhaSachOnline.Data;
using NhaSachOnline.Models;
using NhaSachOnline.Models.DTOs;

namespace NhaSachOnline.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        public CartRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<int> AddItem(int bookId, int soLuong)
        {
            throw new UnauthorizedAccessException("Người dùng chưa đăng nhập");
        }

        public async Task<bool> DoCheckout(CheckoutModel checkoutModel)
        {
            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                var userId = GetUserId();
                if (string.IsNullOrEmpty(userId))
                {
                    throw new UnauthorizedAccessException("Người dùng chưa đăng nhập");
                }
                var cart = await GetCart(userId);
                if (cart is null) 
                {
                    throw new UnauthorizedAccessException("Lỗi, gỏi hàng trống");
                }
                var chiTietGoiHang = _dbContext.CartDetails.Where(x => x.ShoppingCartId == cart.Id).ToList();
                if (chiTietGoiHang.Count == 0)
                {
                    throw new UnauthorizedAccessException("Gỏi hàng trống");
                }
                var trangThaiDonHang = _dbContext.OrderStatuses.FirstOrDefault(x => x.StatusName == "Pending");
                if (trangThaiDonHang is null)
                {
                    throw new UnauthorizedAccessException("Đơn hàng đang chờ xử lý");
                }
                var order = new Order
                {
                    UserId = userId,
                    CraeteDate = DateTime.Now,
                    Name = checkoutModel.Name,
                    MobileNumber = checkoutModel.MobilNumber,
                    Address = checkoutModel.Address,
                    PaymentMethod = checkoutModel.PaymentMethod,
                    IsPaid = false,
                    //OrderStatus = trangThaiDonHang,
                   // OrderDetails = chiTietGoiHang
                };
                _dbContext.Orders.Add(order);
                _dbContext.SaveChanges();

                foreach (var item in chiTietGoiHang)
                {
                    var chiTietDonHang = new OrderDetail
                    {
                        BookId = item.BookId,
                        OrderId = order.Id,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice,

                    };
                }
            }
            catch
            {

            }
            throw new NotImplementedException();
        }

        //public async Task AddItem(int bookId, int soLuong)
        //{
        //    _dbContext.Books.Add(book);
        //    await _dbContext.SaveChangesAsync();
        //}
        public async Task<ShoppingCart> GetCart(string userId)
        {
            var cart = _dbContext.ShoppingCarts.FirstOrDefaultAsync(x => x.UserId == userId);
            return cart;
        }

        public async Task<ShoppingCart> GetCartItemCount(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<ShoppingCart> GetUserCart(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> RemoveItem(int bookId)
        {
            throw new NotImplementedException();
        }
        private string GetUserId()
        {
            //Nhận diện người dùng
            //var nhanDienNguoiDung = _contextAccessor.HttpContext.User;
            //string UserId = _userManager.GetUserId(nhanDienNguoiDung);
            //return UserId;

            ///Nhận diện người dùng

            var httpContext = _contextAccessor.HttpContext;

            //kiểm tra
            if (httpContext?.User != null)
            {
                return _userManager.GetUserId(httpContext.User);
            }
            return null;
        }
    }
}
