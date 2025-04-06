using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace AssetBundleFramework
{
    /// <summary>
    /// 高度定制的每一个打包文件夹的具体信息,资源类型,资源路径等
    /// </summary>
    public class BuildItem
    {
        [DisplayName("资源路径")]
        [XmlAttribute("AssetPath")]
        public string assetPath { get; set; }

        [DisplayName("资源类型")]
        [XmlAttribute("ResourceType")]
        public EResourceType resourceType { get; set; } = EResourceType.Direct;

        [DisplayName("ab粒度类型")]
        [XmlAttribute("BundleType")]
        public EBundleType bundleType { get; set; } = EBundleType.File;

        [DisplayName("资源后缀")]
        [XmlAttribute("Suffix")]
        public string suffix { get; set; } = ".prefab"; //Suffix资源配置里面的字符串

        [XmlIgnore]
        public List<string> ignorePaths { get; set; } = new List<string>(); 

        [XmlIgnore]
        public List<string> suffixes { get; set; } = new List<string>(); //资源类型,主要是有些包体可以多种类型打在一起,例如Suffix=".png|.spriteatlas"

        //匹配该打包设置的个数
        [XmlIgnore]
        public int Count { get; set; }  //资源类型个数
    }
}
