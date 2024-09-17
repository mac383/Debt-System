using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.models.SaleUnit_models
{
    public class md_SaleUnit
    {
        public int SaleUnitID { get; set; }
        public string UnitName { get; set; }
        public int ByUser { get; set; }
        public int CompanyId { get; set; }

        public md_SaleUnit(int saleunitid, string unitname, int byuser, int companyid)
        {
            this.SaleUnitID = saleunitid;
            this.UnitName = unitname;
            this.ByUser = byuser;
            this.CompanyId = companyid;
        }
    }
}
