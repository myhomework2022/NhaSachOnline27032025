using Microsoft.AspNetCore.Identity;
using NhaSachOnline.Const;

namespace NhaSachOnline.Data
{
    public class DataSeed
    {
        public static async Task KhoiTaoDuLieuMacDinh(IServiceProvider dichVu)
        {
            var quanLyNguoiDung = dichVu.GetService<UserManager<IdentityUser>>();
            var quanLyVaiTro = dichVu.GetService<RoleManager<IdentityRole>>();

            //Thêm nột vai trò thêm một Role vào cơ sở dữ liệu
            await quanLyVaiTro.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await quanLyVaiTro.CreateAsync(new IdentityRole(Roles.User.ToString()));

            var quanTri = new IdentityUser
            {
                UserName = "admin@test.xyz",
                Email = "admin@test.xyz",
                EmailConfirmed = true
            };
            
            var nguoiDungTrongCSDL = await quanLyNguoiDung.FindByEmailAsync(quanTri.Email);
            //nếu tài khoản Admin không tồn tại trong cơ sở dữ liệu
            //Hoặc có thể hiểu là chưa có trong csdl
            if (nguoiDungTrongCSDL is null)
            {
                //tạo tài khoản Admin với mật khẩu là Admin12345!@#$%
                var ketQua = await quanLyNguoiDung.CreateAsync(quanTri, "Admin12345!@#$%");
                if (ketQua.Succeeded)
                {
                    await quanLyNguoiDung.AddToRoleAsync(quanTri, Roles.Admin.ToString());
                }
                else
                {
                    //In ra lỗi
                    foreach (var error in ketQua.Errors)
                    {
                        Console.WriteLine(error.Description);
                    }
                }
            }
        }
    }
}
