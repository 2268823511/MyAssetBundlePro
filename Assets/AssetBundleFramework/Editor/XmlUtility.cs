using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace AssetBundleFramework
{
    /// <summary>
    /// 序列化,反序列化XML,解析路径BuildSetting
    /// </summary> <summary>
    /// 
    /// </summary>
    public static class XmlUtility
    {
        public static UTF8Encoding UTF8 = new UTF8Encoding(false);

        /// <summary>
        /// 序列化XML配置文件路径
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName">配置文件--文件路径</param>
        /// <param name="data">应该是策划配置的配置文件的一个表?</param>
        /// <returns></returns> <summary>
        public static bool Save<T>(string fileName, T data)
        {
            if (File.Exists(fileName))
                File.Delete(fileName);

            FileStream stream = File.Open(fileName, FileMode.Create, FileAccess.Write);
            //去掉命名空间
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            XmlTextWriter writer = new XmlTextWriter(stream, UTF8);
            writer.Formatting = Formatting.Indented;
            serializer.Serialize(writer, data, ns);
            writer.Close();
            stream.Close();
            return true;
        }

        /// <summary>
        /// 反序列化XML配置文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static T Read<T>(string fileName) where T : class
        {
            FileStream stream = null;
            if (!File.Exists(fileName))
            {
                return default(T);
            }

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                stream = File.OpenRead(fileName);
                XmlReader reader = XmlReader.Create(stream);
                T instance = (T)serializer.Deserialize(reader);
                stream.Close();
                return instance;
            }
            catch
            {
                if (stream != null)
                    stream.Close();
                return default(T);
            }
        }

        public static string ObjectToString(Object obj)
        {
            MemoryStream stream = new MemoryStream();
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            XmlTextWriter writer = new XmlTextWriter(stream, UTF8);
            writer.Formatting = Formatting.Indented;

            serializer.Serialize(writer, obj);

            stream.Seek(0, SeekOrigin.Begin);
            StreamReader sr = new StreamReader(stream);
            string xmlString = sr.ReadToEnd();
            sr.Close();

            return xmlString;
        }

        public static T StringToObject<T>(string text) where T : class
        {
            byte[] byteArray = UTF8.GetBytes(text);
            MemoryStream stream = new MemoryStream(byteArray);

            XmlSerializer serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(stream);
        }
    }
}
