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
                form.Add(new StringContent(propertyValue.ToString()), propertyName);
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
    }
}
