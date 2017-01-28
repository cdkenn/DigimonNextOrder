using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using NextOrderGuide.Classes;

namespace NextOrderGuide.DigiAppIO
{
    class DigiAppReadWrite
    {
        public static void writeXML(DigiApp da)
        {
            XmlSerializer writer = new XmlSerializer(typeof(DigiApp));
            var path = "digiapp.xml";
            System.IO.FileStream file = System.IO.File.Create(path);
            writer.Serialize(file, da);
            file.Close();
        }

        public static DigiApp readXML()
        {
            XmlSerializer reader = new XmlSerializer(typeof(DigiApp));
            var path = @"digiapp.xml";
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            DigiApp da = (DigiApp)reader.Deserialize(file);
            file.Close();
            return da;
        }
    }
}
