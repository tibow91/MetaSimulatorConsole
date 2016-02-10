using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MetaSimulatorConsole
{
    abstract class SerializerTemplate // PATTERN TEMPLATE
    {
        protected bool EvaluateFilename(string filename)
        {
            if (String.IsNullOrEmpty(filename))
            {
                Console.WriteLine("Serializer: filename to deserialize is missing !");
                return false;
            }
            return true;
        }
        protected abstract XmlSerializer GetXMLSerializer();

        protected object ReadFrom(string filename,XmlSerializer deserializer)
        {
            if (deserializer == null)
            {
                Console.WriteLine("Serialize-Read: Cannot execute deserialization because of null deserializer");
                return null;
            }

            string path = @"C:\XML\" + filename + ".xml";
            if (!File.Exists(path))
            {
                Console.WriteLine("Serialize-Read: Impossible de déserializer '" + filename + ".xml' : Fichier non existant");
                return null;
            }
            TextReader reader = new StreamReader(path);
            object obj = deserializer.Deserialize(reader);
            reader.Close();
            return obj;
        }

        protected virtual void serialize(TextWriter writer, XmlSerializer serializer, object obj)
        {
            serializer.Serialize(writer, obj);
        }


        protected void Write(object obj,string filename,XmlSerializer serializer)
        {
            if (obj == null)
            {
                Console.WriteLine("Serialize-Write: Cannot serialize null objects !");
                return;
            }
            if (serializer == null)
            {
                Console.WriteLine("Serialize-Write: Cannot execute serialization because of null serializer");
                return;
            }
            Directory.CreateDirectory(@"C:\XML\");

            using (TextWriter writer = new StreamWriter(@"C:\XML\" + filename + ".xml"))
            {
                //serializer.Serialize(writer, obj);
                serialize(writer,serializer,obj);
            }
        }

        public object Deserialize(string filename)
        { //template method

            if (!EvaluateFilename(filename)) return null;
            return ReadFrom(filename, GetXMLSerializer());
        }

        public void Serialize(object obj,string filename)
        {
            if (!EvaluateFilename(filename)) return;
            Write(obj,filename,GetXMLSerializer());
        }
  
    }

    class CoordonneesSerializer : SerializerTemplate
    {
        protected override XmlSerializer GetXMLSerializer()
        {
            return new XmlSerializer(typeof(Coordonnees));
        }

        protected override void serialize(TextWriter writer,XmlSerializer serializer, object obj)
        {
            serializer.Serialize(writer,(Coordonnees) obj);
        }
    }

    class ZoneSerializer : SerializerTemplate
    {

        protected override XmlSerializer GetXMLSerializer()
        {
            return new XmlSerializer(typeof (ZoneAbstraite));
        }

    }

    class ZoneGeneraleAOKSerializer : SerializerTemplate
    {

        protected override XmlSerializer GetXMLSerializer()
        {
            return new XmlSerializer(typeof(ZoneGeneraleAOK));
        }
        protected override void serialize(TextWriter writer, XmlSerializer serializer, object obj)
        {
            serializer.Serialize(writer, (ZoneGeneraleAOK)obj);
        }
    }

}
