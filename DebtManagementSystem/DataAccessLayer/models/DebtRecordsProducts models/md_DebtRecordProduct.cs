using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.models.DebtRecordsProducts_models
{
    public class md_DebtRecordsProducts
    {
        public int DebtProductId { get; set; }
        public string FullName { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public string UnitName { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
        public string Currency { get; set; }
        public string ByUser { get; set; }
        public string IsPaid { get; set; }
        public DateTime RegistrationDate { get; set; }

        public md_DebtRecordsProducts(int debtProductId, string fullName, string productName, double productPrice, string unitName,
            int quantity, double totalPrice, string currency, string byUser, string isPaid, DateTime registrationDate)
        {
            DebtProductId = debtProductId;
            FullName = fullName;
            ProductName = productName;
            ProductPrice = productPrice;
            UnitName = unitName;
            Quantity = quantity;
            TotalPrice = totalPrice;
            Currency = currency;
            ByUser = byUser;
            IsPaid = isPaid;
            RegistrationDate = registrationDate;
        }

    }
}
