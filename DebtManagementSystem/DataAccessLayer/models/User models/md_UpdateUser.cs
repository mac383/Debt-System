﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.models.User_models
{
    public class md_UpdateUser
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Phone1 { get; set; }
        public string? Phone2 { get; set; }
        public string? TelegramId { get; set; }
        public int CompanyId { get; set; }
        public int? ByUser { get; set; }
        public string UserName { get; set; }
        public long Permissions { get; set; }
        public byte[]? Image { get; set; }
        public bool IsActive { get; set; }

        public md_UpdateUser(int userId, string fullName, string phone1, string? phone2, string? telegramId,
            int companyId, int? byUser, string userName, long permissions, byte[]? image, bool isActive)
        {
            UserId = userId;
            FullName = fullName;
            Phone1 = phone1;
            Phone2 = phone2;
            TelegramId = telegramId;
            CompanyId = companyId;
            ByUser = byUser;
            UserName = userName;
            Permissions = permissions;
            Image = image;
            IsActive = isActive;
        }
    }
}