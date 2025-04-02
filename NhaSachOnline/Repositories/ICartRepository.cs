﻿using NhaSachOnline.Models;
using NhaSachOnline.Models.DTOs;

namespace NhaSachOnline.Repositories
{
    public interface ICartRepository
    {
        Task<int> AddItem(int bookId, int soLuong);
        Task<int> RemoveItem(int bookId);
        Task<ShoppingCart> GetUserCart(int id);
        Task<ShoppingCart> GetCart(string userId);
        Task<ShoppingCart> GetCartItemCount(string userId);
        Task<bool> DoCheckout(CheckoutModel checkoutModel);
    }
}
