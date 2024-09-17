using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.models.User_models
{
    public class md_User
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Phone1 { get; set; }
        public string? Phone2 { get; set; }
        public string? TelegramId { get; set; }
        public long Permissions { get; set; }
        public byte[]? Image { get; set; }
        public bool IsActive { get; set; }
        public int? ByUser { get; set; }
        public int CompanyId { get; set; }

        public md_User(int userid, string fullname, string userName, string phone1, string? phone2, string? telegramId,
          long permissions, byte[]? image, bool isactive,  int? byuser, int companyid)
        {
            this.UserId = userid;
            this.FullName = fullname;
            this.UserName = userName;
            this.Phone1 = phone1;
            this.Phone2 = phone2;
            this.TelegramId = telegramId;
            this.Permissions = permissions;
            this.Image = image;
            this.IsActive = isactive;
            this.ByUser = byuser;
            this.CompanyId = companyid;
        }
    }
}
