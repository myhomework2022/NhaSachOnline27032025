using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NhaSachOnline.Models
{
    [Table("Book")]
    public class Book
    {
        public int Id { get; set; }
        //Required là nghĩa trường dữ liệu ko để trống (trêm giao diện)
        // dấu ? nghĩa là cho phép null (trong database)
        [Required]
        [MaxLength(50)]
        public string? BookName {  get; set; }
        [Required]
        [MaxLength(50)]
        public string? AuthorName { get; set; }
        [Required]
        public double Price { get; set; }
        //nvarvachar(Max)
        public string? Image { get; set; }
        [Required]
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public List<CartDetail> CartDetails { get; set; }
        public Stock Stock { get; set; }

        //Không ánh xạ trường dữ liệu GenreName trong bảng Genre
        [NotMapped]
        public string GenreName { get; set; }

        //Không ánh xạ trường dữ liệu Quantity trong bảng Stock
        [NotMapped]
        public int Quantity { get; set; }
    }
}
