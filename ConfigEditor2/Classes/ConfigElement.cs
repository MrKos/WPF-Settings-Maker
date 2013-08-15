using System;
using System.Collections.Generic;
using System.Windows.Documents;
using System.Xml.Serialization;

namespace ConfigEditor2.Classes
{
    /// <summary>Описание строки конфигурации</summary>
    [Serializable]
    public class ConfigElement
    {

        [XmlAttribute(AttributeName = "key")]
        public string Key
        {
            get;
            set;
        }

        [XmlAttribute(AttributeName = "value")]
        public string Value
        {
            get;
            set;
        }
    //    [XmlAttribute(AttributeName = "departments")]
    //    public IEnumerable<Department> Departments { get; set; }
    //}

    //[Serializable]
    //public class Department
    //{
    //    [XmlAttribute(AttributeName = "key")]
    //    public string Key
    //    {
    //        get;
    //        set;
    //    }

    //    [XmlAttribute(AttributeName = "value")]
    //    public string Value
    //    {
    //        get;
    //        set;
    //    }
    }
}
