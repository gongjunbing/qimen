using System;
using System.Collections.Generic;

namespace QiMen.Common
{
    /// <summary>
    /// 奇门字典
    /// </summary>
    public class QiMenDictionary : Dictionary<string, string>
    {
        /// <summary>
        /// 
        /// </summary>
        public QiMenDictionary() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dictionary"></param>
        public QiMenDictionary(IDictionary<string, string> dictionary)
            : base(dictionary)
        { }

        /// <summary>
        /// 添加一个新的键值对。空键或者空值的键值对将会被忽略。
        /// </summary>
        /// <param name="key">键名称</param>
        /// <param name="value">键对应的值，目前支持：string, int, long, double, bool, DateTime类型</param>
        public void Add(string key, object value)
        {
            string strValue;

            if (value == null)
            {
                strValue = null;
            }
            else if (value is string)
            {
                strValue = (string)value;
            }
            else if (value is Nullable<DateTime>)
            {
                Nullable<DateTime> dateTime = value as Nullable<DateTime>;
                strValue = dateTime.Value.ToString(Constants.DATE_TIME_FORMAT);
            }
            else if (value is Nullable<int>)
            {
                strValue = (value as Nullable<int>).Value.ToString();
            }
            else if (value is Nullable<long>)
            {
                strValue = (value as Nullable<long>).Value.ToString();
            }
            else if (value is Nullable<double>)
            {
                strValue = (value as Nullable<double>).Value.ToString();
            }
            else if (value is Nullable<bool>)
            {
                strValue = (value as Nullable<bool>).Value.ToString().ToLower();
            }
            else
            {
                strValue = value.ToString();
            }

            if (!base.ContainsKey(key))
            {
                this.Add(key, strValue);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public new void Add(string key, string value)
        {
            if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value) && !base.ContainsKey(key))
            {
                base[key] = value;
            }
        }

        /// <summary>
        /// 添加全部
        /// </summary>
        /// <param name="dict"></param>
        public void AddAll(IDictionary<string, string> dict)
        {
            if (dict != null && dict.Count > 0)
            {
                IEnumerator<KeyValuePair<string, string>> kvps = dict.GetEnumerator();
                while (kvps.MoveNext())
                {
                    KeyValuePair<string, string> kvp = kvps.Current;
                    if (!base.ContainsKey(kvp.Key))
                    {
                        Add(kvp.Key, kvp.Value);
                    }
                }
            }
        }
    }
}
