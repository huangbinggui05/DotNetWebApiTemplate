using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace MyHelper
{
    public static class ExtendString
    {
        /// <summary>
        /// 扩展string方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonArrayStr"></param>
        /// <returns></returns>
        public static List<T> JsonToList<T>(this string jsonArrayStr)
        {
            JavaScriptSerializer Serializer = new JavaScriptSerializer();
            List<T> objs = new List<T>();
            try
            {
                objs = Serializer.Deserialize<List<T>>(jsonArrayStr);
            }
            catch (Exception ex)
            {
                LogHelper.GetInstance().WriteLog(string.Format("Json字符串序列化对象异常(JsonToList)：{0}", ex.Message), LogType.错误);
            }

            return objs;
        }
        /// <summary>
        /// 扩展string方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        public static T JsonToObj<T>(this string jsonStr)
        {
            JavaScriptSerializer Serializer = new JavaScriptSerializer();
            T obj = default (T);
            try
            {
                obj = Serializer.Deserialize<T>(jsonStr);
            }
            catch (Exception ex)
            {
                LogHelper.GetInstance().WriteLog(string.Format("Json字符串序列化对象异常(JsonToObj)：{0}", ex.Message), LogType.错误);
            }

            return obj;
        }
    }
    
}
