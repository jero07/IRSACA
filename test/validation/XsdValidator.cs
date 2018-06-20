using System;
using System.IO; 
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Schema;

namespace IRSACA.test
{
    public class XsdValidator
    {
         public List<XmlSchema> Schemas { get; set; }
        public List<String> Errors { get; set; }
        public List<String> Warnings { get; set; }

        //exception
        //public  ACAException Exception { get; set; }
        public List<ACAException> Exceptionls { get; set; }

        public XsdValidator()
        {
            Schemas = new List<XmlSchema>();
            Exceptionls = new List<ACAException>();
        }

        /// <summary>
        /// Add a schema to be used during the validation of the XML document
        /// </summary>
        /// <param name="schemaFileLocation">The file path for the XSD schema file to be added for validation</param>
        /// <returns>True if the schema file was successfully loaded, else false (if false, view Errors/Warnings for reason why)</returns>
        public bool AddSchema(string schemaFileLocation)
        {
            if (String.IsNullOrEmpty(schemaFileLocation)) return false;
            if (!File.Exists(schemaFileLocation)) return false;

            // Reset the Error/Warning collections
            Errors = new List<string>();
            Warnings = new List<string>();

            XmlSchema schema;

            using (var fs = File.OpenRead(schemaFileLocation))
            {
                schema = XmlSchema.Read(fs, ValidationEventHandler);
            }

            var isValid = !Errors.Any() && !Warnings.Any();

            if (isValid)
            {
                Schemas.Add(schema);
            }

            return isValid;
        }

        /// <summary>
        /// Perform the XSD validation against the specified XML document
        /// </summary>
        /// <param name="xmlLocation">The full file path of the file to be validated</param>
        /// <returns>True if the XML file conforms to the schemas, else false</returns>
        public bool IsValid(string xmlLocation)
        {
            if (!File.Exists(xmlLocation))
            {
                throw new FileNotFoundException("The specified XML file does not exist", xmlLocation);
            }

            using (var xmlStream = File.OpenRead(xmlLocation))
            {
                return IsValid(xmlStream);
            }
        }

        /// <summary>
        /// Perform the XSD validation against the supplied XML stream
        /// </summary>
        /// <param name="xmlStream">The XML stream to be validated</param>
        /// <returns>True is the XML stream conforms to the schemas, else false</returns>
        private bool IsValid(Stream xmlStream)
        {
            // Reset the Error/Warning collections
            Errors = new List<string>();
            Warnings = new List<string>();

            var settings = new XmlReaderSettings
            {
                ValidationType = ValidationType.Schema
            };
            settings.ValidationEventHandler += ValidationEventHandler;

            foreach (var xmlSchema in Schemas)
            {
                settings.Schemas.Add(xmlSchema);
            }

            var xmlFile = XmlReader.Create(xmlStream, settings);

            try
            {
                //var xmlFile = XmlReader.Create(xmlStream, settings);
                while (xmlFile.Read()) { }
            }
            catch (XmlException xex)
            {
                Errors.Add(xex.Message);
            }
            expcsv(Exceptionls);
            return !Errors.Any() && !Warnings.Any();
        }

        //private void ValidationEventHandler(object sender, ValidationEventArgs e)
        //{
        //    switch (e.Severity)
        //    {
        //        case XmlSeverityType.Error:
        //            Errors.Add(e.Message);
        //            break;
        //        case XmlSeverityType.Warning:
        //            Warnings.Add(e.Message);
        //            break;
        //    }
        //}

        private void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            XmlReader r = (XmlReader)sender;
            ACAException exp = new ACAException();
            exp.LineNumber = (e.Exception).LineNumber.ToString();
            exp.LinePosition = (e.Exception).LinePosition.ToString();
            exp.Message = (e.Exception).Message.ToString();
            exp.LocalName = r.LocalName;
            exp.Name = r.Name;
            exp.NamespaceURI = r.NamespaceURI;
            Exceptionls.Add(exp);
            switch (e.Severity)
            {
                case XmlSeverityType.Error:
                    Errors.Add(e.Message);
                    break;

                case XmlSeverityType.Warning:
                    Warnings.Add(e.Message);
                    break;
            }
        }

        public static void expcsv(List<ACAException> ls)
        {
            string pathDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = pathDesktop + "\\ACAException.csv"; //HOURLY SALARY

            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }

            using (System.IO.TextWriter writer = File.CreateText(filePath))
            {
                writeheader(filePath, writer);
                writedetail(filePath, writer, ls);
            }
        }

        private static void writeheader(string filePath, TextWriter writer)
        {
            writer.Write("Line");
            writer.Write(',');
            writer.Write("Position");
            writer.Write(',');
            //writer.Write("Local Name");
            //writer.Write(',');
            writer.Write("Name");
            writer.Write(',');
            writer.Write("Message");
            writer.Write(',');
            writer.Write("NamespaceURI");
            writer.WriteLine("");
        }

        private static void writedetail(string filePath, TextWriter writer, List<ACAException> ls)
        {
            foreach (var exp in ls)
            {
                writer.Write(exp.LineNumber);
                writer.Write(',');
                writer.Write(exp.LinePosition);
                writer.Write(',');
                //writer.Write(exp.LocalName);
                //writer.Write(',');
                writer.Write(exp.Name);
                writer.Write(',');
                writer.Write(exp.Message);
                writer.Write(',');
                writer.Write(exp.NamespaceURI);
                writer.WriteLine("");
            }
        }
        
    }
}