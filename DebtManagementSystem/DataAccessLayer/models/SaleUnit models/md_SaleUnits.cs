using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.models.SaleUnit_models
{
    public class md_SaleUnits
    {
        public int SaleUnitID { get; set; }
        public string UnitName { get; set; }
        public string ByUser { get; set; }

        public md_SaleUnits(int saleunitid, string unitname, string byuser)
        {
            this.SaleUnitID = saleunitid;
            this.UnitName = unitname;
            this.ByUser = byuser;  
        }
    }
}
