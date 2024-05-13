using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Dictionar.MyClasses
{
    [Serializable]
    public class Word
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlElement]
        public string Description { get; set; }

        [XmlAttribute]
        public string Category { get; set; }

        [XmlElement]
        public string Image {  get; set; }
    }
}
