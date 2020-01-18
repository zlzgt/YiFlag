using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace YiFlag.Tools
{
    public class ModelHelper
    {
        /// <summary>
        /// 将模型modelB的值赋值给modelA
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="modelA"></param>
        /// <param name="modelB"></param>
        public static void CopyModel<T1, T2>(T1 modelA, T2 modelB)
        {
            PropertyInfo[] cfgItemProperties = modelB.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

            foreach (PropertyInfo item in cfgItemProperties)
            {
                string name = item.Name;
                object value = item.GetValue(modelB, null);
                //string 或 值属性，且value 不为 null
                if ((item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String")) && value != null && !string.IsNullOrEmpty(value.ToString()))
                {
                    #region 在MODEL2中查找是否有此参数名，有则赋值
                    PropertyInfo[] list2 = modelA.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
                    foreach (PropertyInfo i2 in list2)
                    {
                        //两者 PropertyType.Name  要相等
                        if (item.Name == i2.Name && item.PropertyType.Name == i2.PropertyType.Name)
                        {
                            i2.SetValue(modelA, value, null);
                        }
                    }
                    #endregion

                }
            }
        }
    }
}
