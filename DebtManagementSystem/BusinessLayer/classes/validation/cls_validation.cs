using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessLayer.classes.validation
{
    public class cls_validation
    {
        public static bool IsTextNullOrEmpty(string text)
        {
            return string.IsNullOrWhiteSpace(text);
        }

        public static bool IsText(string text)
        {
            // تحقق من أن النص ليس null
            if (string.IsNullOrEmpty(text))
                return false;

            // تعريف النمط للتحقق من أن النص يحتوي فقط على أحرف
            string pattern = @"^[^\d!@#$%^&*()_+{}|:;<>,.?~\-=[\]\\]*$";

            // التحقق من تطابق النمط مع النص
            return Regex.IsMatch(text, pattern);

            // تقبل فقط النص باي لغة.
        }

        public static bool IsInt(string text)
        {
            // تعريف النمط للتحقق من أن النص يحتوي على أرقام فقط
            string pattern = @"^\d+$";

            // التحقق من تطابق النمط مع النص
            return Regex.IsMatch(text, pattern);

            // تقبل فقط الارقام الصحيحة.
        }

        public static bool IsFloat(string text)
        {
            // تعريف النمط للتحقق من أن النص يحتوي على أرقام فقط (يسمح بالأرقام العشرية)
            string pattern = @"^\d+(\.\d+)?$";

            // التحقق من تطابق النمط مع النص
            return Regex.IsMatch(text, pattern);

            // تقبل الارقام الصحيحة والعشرية.
        }

        public static bool IsPhone1NumberValid(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                return false;

            return IsInt(phoneNumber) && CheckLength(8, 14, phoneNumber);
            // يقبل فقط الارقام وبطول 8 - 14 ولا يقبل القيمة الفارغة.
        }

        public static bool IsPhone2NumberValid(string phoneNumber)
        {
            return IsTextNullOrEmpty(phoneNumber) ? true : IsPhone1NumberValid(phoneNumber);
            // يقبل القيمة الفارغة والارقام بطول 8 - 14.
        }

        public static bool IsUsernameValid(string username)
        {
            // تحقق من أن اسم المستخدم ليس null
            if (string.IsNullOrEmpty(username))
                return false;

            // تعريف النمط للتحقق من أن اسم المستخدم يتطابق مع الشروط المطلوبة
            string pattern = @"^[a-zA-Z][a-zA-Z0-9_]{4,13}$";

            // التحقق من تطابق النمط مع اسم المستخدم
            return Regex.IsMatch(username, pattern);
            /*
             * يقبل الحروف الانكليزية والارقام والشارحة السفلية وبطول 5 - 14.
             * يجب ان تبدا النص بحرف
             */
        }

        public static bool IsTelegramIdValid(string telegramId)
        {
            if (telegramId == null)
                return true;

            // تعريف النمط للتحقق من أن معرف Telegram يتطابق مع الشروط المطلوبة
            string pattern = @"^[a-zA-Z][a-zA-Z0-9_]{4,24}$";

            // التحقق من تطابق النمط مع معرف Telegram
            return Regex.IsMatch(telegramId, pattern);
            /*
            * يقبل الحروف الانكليزية والارقام والشارحة السفلية وبطول 5 - 25.
            * يجب ان تبدا النص بحرف
            */
        }

        public static bool IsPasswordValid(string password)
        {
            if (string.IsNullOrEmpty(password))
                return false;

            return password.Length >= 8 && password.Length <= 14;
            // يقبل اي شي بطول 8 - 14.
        }

        public static bool CheckLength(byte minLength, byte maxLength, string? text)
        {
            if (string.IsNullOrEmpty(text))
                return false;

            return text.Length >= minLength && text.Length <= maxLength;
        }

    }
}
