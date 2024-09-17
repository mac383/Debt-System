using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.models
{
    public class md_Category
    {

        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public byte[]? Image { get; set; }
        public int ByUser { get; set; }
        public int CompanyId { get; set; }

        public md_Category(int categoryid, string categoryname, byte[]? image, int byuser, int Companyid)
        {
            this.CategoryID = categoryid;
            this.CategoryName = categoryname;
            this.Image = image;
            this.ByUser = byuser;
            this.CompanyId = Companyid;
        }

    }
}
