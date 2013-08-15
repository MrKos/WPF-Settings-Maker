using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ConfigEditor2.Classes
{
    /// <summary>Простатая читалка конфига, типа Ключ-Значение(Key, Value)</summary>
    public class SimpleConfig
    {
        /// <summary></summary>
        public List<ConfigElement> ConfigElements;


        /// <summary></summary>
        public SimpleConfig()
        {
            ConfigElements = new List<ConfigElement>();
        }


        /// <summary></summary>
        public ConfigElement this[string key]
        {
            get
            {
                return ConfigElements.Find(delegate(ConfigElement element) { return element.Key == key; });
            }
        }


        /// <summary></summary>
        public static void SaveConfig(SimpleConfig obj, string filePath)
        {
            XmlWriterSettings settings = new XmlWriterSettings(); // всего-навсего для читаемости xml

            settings.Indent = true;
            settings.IndentChars = "    ";
            settings.NewLineChars = "\n";
            settings.OmitXmlDeclaration = true;

            XmlTextWriter writer = new XmlTextWriter(filePath, System.Text.Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 4;

            XmlSerializer serializer = new XmlSerializer(typeof(SimpleConfig));
            serializer.Serialize(writer, obj);

            writer.Close();

        }


        /// <summary></summary>
        public static SimpleConfig LoadConfig(string filePath)
        {

            SimpleConfig res;

            XmlSerializer serializer = new XmlSerializer(typeof(SimpleConfig));

            StreamReader reader = new StreamReader(filePath);

            res = (SimpleConfig)serializer.Deserialize(reader);

            reader.Close();

            return res;
        }
        class VacationSpots : ObservableCollection<string>
        {
            public VacationSpots()
            {

                Add("Spain");
                Add("France");
                Add("Peru");
                Add("Mexico");
                Add("Italy");
            }
        }
    }
}