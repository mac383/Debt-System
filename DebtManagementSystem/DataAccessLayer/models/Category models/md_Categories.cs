using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.models
{
    public class md_Categories
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public byte[]? CategoryImage { get; set; }
        public string ByUser { get; set; }
        public int Items { get; set; }

        public md_Categories(int categoryId, string categoryName, byte[]? categoryImage, string byUser, int items)
        {
            CategoryId = categoryId;
            CategoryName = categoryName;
            CategoryImage = categoryImage;
            ByUser = byUser;
            Items = items;
        }
    }
}
