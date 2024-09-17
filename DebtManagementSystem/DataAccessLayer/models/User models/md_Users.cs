using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.models.User_models
{
    public class md_Users
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Phone1 { get; set; }
        public string? Phone2 { get; set; }
        public string? TelegramId { get; set; }
        public long Permissions { get; set; }
        public byte[]? Image { get; set; }
        public string IsActive { get; set; }
        public string? ByUser { get; set; }
       
        public md_Users(int userId, string fullName, string userName,  string phone1,
            string? phone2, string? telegramId, long permissions, byte[]? image, string isActive,
            string? byUser)
        {
            this.UserId = userId;
            this.FullName = fullName;
            this.UserName = userName;
            this.Phone1 = phone1;
            this.Phone2 = phone2;
            this.TelegramId = telegramId;
            this.Permissions = permissions;
            this.Image = image;
            this.IsActive = isActive;
            this.ByUser = byUser;
        }
    }
}
