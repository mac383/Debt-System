using BusinessLayer.classes.keys;
using BusinessLayer.classes.validation;
using DataAccessLayer.models;
using DataAccessLayer.models.User_models;
using DataAccessLayer.repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.classes
{
    public class cls_Users
    {
        enum EN_Mode { AddNew = 1, UpdateGeneralUser = 2, UpdateCurrentUser = 3 };
        private EN_Mode _Mode;

        public enum EN_IsActive { Active = 1, InActive = 0 };
        public enum EN_IsDeleted { Deleted = 1, NotDeleted = 0 };

        public int UserId { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Phone1 { get; set; }
        public string? Phone2 { get; set; }
        public string? TelegramId { get; set; }
        public long Permissions { get; set; }
        public byte[]? Image { get; set; }
        public EN_IsActive IsActive { get; set; }
        public EN_IsDeleted IsDeleted { get; set; }
        public int? ByUser { get; set; }
        public int CompanyId { get; set; }

        // For new user.
        public cls_Users()
        {
            UserId = -1;
            FullName = string.Empty;
            UserName = string.Empty;
            Password = string.Empty;
            Phone1 = string.Empty;
            Phone2 = string.Empty;
            TelegramId = string.Empty;
            Permissions = 0;
            Image = null;
            IsActive = EN_IsActive.Active;
            IsDeleted = EN_IsDeleted.NotDeleted;
            ByUser = -1;
            CompanyId = -1;

            _Mode = EN_Mode.AddNew;
        }

        // For update general user.
        public cls_Users(int userId, string fullName, string phone1, string? phone2, string? telegramId,
            int companyId, int? byUser, string userName, long permissions, byte[]? image, bool isActive)
        {
            UserId = userId;
            FullName = fullName;
            UserName = userName;
            Password = string.Empty;
            Phone1 = phone1;
            Phone2 = phone2;
            TelegramId = telegramId;
            Permissions = permissions;
            Image = image;
            IsActive = isActive ? EN_IsActive.Active : EN_IsActive.InActive;
            IsDeleted = EN_IsDeleted.NotDeleted;
            ByUser = byUser;
            CompanyId = companyId;

            _Mode = EN_Mode.UpdateGeneralUser;
        }

        // For update current user
        public cls_Users(int userId, string fullName, string userName, string password, string phone1,
          string? phone2, string? telegramId, long permissions, byte[]? image, bool isActive,
          int? byUser, int companyId)
        {

            this.UserId = userId;
            this.FullName = fullName;
            this.UserName = userName;
            this.Password = password;
            this.Phone1 = phone1;
            this.Phone2 = phone2;
            this.TelegramId = telegramId;
            this.Permissions = permissions;
            this.Image = image;
            this.IsActive = isActive ? EN_IsActive.Active : EN_IsActive.InActive;
            this.IsDeleted = EN_IsDeleted.NotDeleted;
            this.ByUser = byUser;
            this.CompanyId = companyId;

            this._Mode = EN_Mode.UpdateCurrentUser;
        }

        // Completed Testing.
        public bool ValidateUserObject()
        {
            // تقليم النصوص وحفظها في متغيرات محلية
            string fullName = this.FullName.Trim();
            string userName = this.UserName.Trim();
            string password = this.Password.Trim();
            string phone1 = this.Phone1.Trim();
            string? phone2 = this.Phone2?.Trim();
            string? telegramId = this.TelegramId?.Trim();

            // التحقق من صحة اسم المستخدم
            if (!cls_validation.IsUsernameValid(userName))
                return false;

            // التحقق من صحة كلمة المرور فقط عندما يكون في وضع الاضافة و تحديث بيانات المستخدم الحالي
            if (_Mode == EN_Mode.AddNew || _Mode == EN_Mode.UpdateCurrentUser)
                // التحقق من صحة كلمة المرور
                if (!cls_validation.IsPasswordValid(password))
                    return false;

            // التحقق من صحة الاسم الكامل
            if (!cls_validation.IsText(fullName) || !cls_validation.CheckLength(1, 50, fullName))
                return false;

            // التحقق من صحة الهاتف 1
            if (!cls_validation.IsPhone1NumberValid(phone1))
                return false;

            // التحقق من صحة الهاتف 2 إذا لم يكن فارغًا
            if (!cls_validation.IsPhone2NumberValid(phone2))
                return false;

            // التحقق من صحة معرف التليجرام إذا لم يكن فارغًا
            if (!cls_validation.IsTelegramIdValid(telegramId))
                return false;

            // جميع التحقق مر بنجاح
            return true;
        }

        //Completed Testing
        public static async Task<List<md_Users>?> GetUsersAsync(int companyId)
        {
            return await cls_Users_D.GetUsersAsync(companyId);
        }

        //Completed Testing
        public static async Task<List<md_Users>?> GetActiveUsersAsync(int companyId)
        {
            return await cls_Users_D.GetActiveUsersAsync(companyId);
        }

        //Completed Testing
        public static async Task<List<md_Users>?> GetInActiveUsersAsync(int companyId)
        {
            return await cls_Users_D.GetInActiveUsersAsync(companyId);
        }

        //Completed Testing
        public static async Task<cls_Users?> GetUserByIdAsync(int userId, int companyId)
        {
            md_User? user = await cls_Users_D.GetUserByIdAsync(userId, companyId);

            if (user == null)
                return null;

            return new cls_Users
                (
                   user.UserId, user.FullName, user.Phone1, user.Phone2, user.TelegramId, user.CompanyId, user.ByUser,
                   user.UserName, user.Permissions, user.Image, user.IsActive
                );
        }

        //Completed Testing
        public static async Task<cls_Users?> GetUserByLogInInfoAsync(string userName, string password)
        {
            md_UserAuth? user = await cls_Users_D.GetUserByLogInInfoAsync(userName, password);

            if (user == null)
                return null;

            return new cls_Users
                (
                   user.UserId, user.FullName, user.UserName, user.Password, user.Phone1, user.Phone2, user.TelegramId,
                   user.Permissions, user.Image, user.IsActive, user.ByUser, user.CompanyId

                );
        }

        //Completed Testing
        public static async Task<int> GetUsersCountAsync(int companyId)
        {
            return await cls_Users_D.GetUsersCountAsync(companyId);
        }

        //Completed Testing
        public static async Task<bool> IsUserNameExistAsync(string userName, int companyId)
        {
            return await cls_Users_D.IsUserNameExistAsync(userName, companyId);
        }

        //Completed Testing
        public static async Task<bool> IsUserNameExistWithOutCurrentUserAsync(int userId, string userName, int companyId)
        {
            return await cls_Users_D.IsUserNameExistWithOutCurrentUserAsync(userId, userName, companyId);
        }

        //Completed Testing
        public static async Task<bool> SetUserAsActiveAsync(int userId,  int companyId)
        {
            return await cls_Users_D.SetUserAsActiveAsync(userId, companyId);
        }

        //Completed Testing
        public static async Task<bool> SetUserAsInActiveAsync(int userId, int companyId)
        {
            return await cls_Users_D.SetUserAsInActiveAsync(userId, companyId);
        }

        //Completed Testing
        private async Task<bool> _NewUserAsync()
        {
            // التحقق من صحة البيانات
            if (!ValidateUserObject())
                return false;

            // Keep user Data In User Model For Insert It In Database.
            md_NewUser user = new md_NewUser
            (
                    FullName, Phone1, Phone2, TelegramId, CompanyId, ByUser, UserName, Password, Permissions, Image
            );

            // Insert User In Database.
            int insertedId = await cls_Users_D.NewUserAsync(user);

            // Get Inserted Id If The User Is Inserted Successfully Or Get -1 If Not Inserted.
            UserId = insertedId > 0 ? insertedId : -1;

            // Return Is Company Inserted Successfully Or Not.
            return insertedId > 0;
        }

        //Completed Testing
        private async Task<bool> _UpdateUserAsync()
        {
            if (!ValidateUserObject())
                return false;

            md_UpdateUser user = new md_UpdateUser
                (
                    UserId, FullName, Phone1, Phone2, TelegramId, CompanyId, ByUser, UserName, Permissions, Image, Convert.ToBoolean(IsActive)
                );

            return await cls_Users_D.UpdateUserAsync(user);

        }

        //Completed Testing
        private async Task<bool> _UpdateCurrentUserAsync()
        {
            if (!ValidateUserObject())
                return false;

            md_UpdateCurrentUser user = new md_UpdateCurrentUser
                (
                    UserId, FullName, Phone1, Phone2, TelegramId, CompanyId, ByUser, UserName, Password, Image, Convert.ToBoolean(IsActive)
                );

            return await cls_Users_D.UpdateCurrentUserAsync(user);
        }

        //Completed Testing
        public async Task<bool> SaveAsync()
        {
            switch (_Mode)
            {
                case EN_Mode.AddNew:
                    if (await _NewUserAsync())
                    {
                        _Mode = EN_Mode.UpdateGeneralUser;
                        return true;
                    }
                    break;

                case EN_Mode.UpdateGeneralUser:
                    if (await _UpdateUserAsync())
                        return true;
                    break;

                case EN_Mode.UpdateCurrentUser:
                    if (await _UpdateCurrentUserAsync())
                        return true;
                    break;
            }

            return false;
        }

        //Completed Testing
        public static Task<bool> DeleteUser(int userId, int companyId, int byUser)
        {
            return cls_Users_D.DeleteUserAsync(userId, companyId, byUser);
        }
    }

}