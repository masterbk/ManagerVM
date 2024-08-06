using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Services.Helper
{
    public static class ObjectUtil
    {
        public static MultipartFormDataContent ToFormData(this object obj, Dictionary<string,string> mores = null)
        {
            MultipartFormDataContent form = new MultipartFormDataContent();
            Type type = obj.GetType();

            // Duyệt qua các thuộc tính của đối tượng
            foreach (PropertyInfo property in type.GetProperties())
            {
                // Lấy tên thuộc tính
                string propertyName = property.Name;

                // Lấy giá trị thuộc tính
                object propertyValue = property.GetValue(obj);
                if (propertyValue != null)
                {
                    form.Add(new StringContent(propertyValue.ToString()), propertyName);
                }
            }

            if (mores != null)
            {
                foreach (var item in mores)
                {
                    form.Add(new StringContent(item.Value), item.Key);
                }
            }

            return form;
        }

        public static string ToFloatString(this string str)
        {
            if(str?.EndsWith(".0") == true || str?.EndsWith("0") == false)
            {
                return str;
            }

            if(str?.EndsWith("0") == true)
            {
                str = str.Substring(0, str.Length - 2);
            }

            return ToFloatString(str);
        }
    }
}
