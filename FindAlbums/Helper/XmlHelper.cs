using System;
using System.IO;
using System.Xml.Serialization;
using FindAlbums.Data;

namespace FindAlbums.Helper
{
    public class XmlUse
    {
        private const string PathToXml = @"..\..\Albums.xml";

        /// <summary>
        /// Чтение данных с xml-файла
        /// </summary>
        public static AllData DeserializeXml()
        {
            try
            {
                using (FileStream fs = new FileStream(PathToXml, FileMode.Open))
                {
                    var xmlSerializer = new XmlSerializer(typeof(AllData));
                    return (AllData)xmlSerializer.Deserialize(fs);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Запись в xml-файл
        /// </summary>
        /// <param name="dataData"></param>
        public static void SerializeXml(AllData dataData)
        {
            if (dataData == null)
                throw new ArgumentNullException(nameof(dataData));

            try
            {
                var xmlSerializer = new XmlSerializer(typeof(AllData));
                using (var fs = new FileStream(PathToXml, FileMode.Open))
                {
                    xmlSerializer.Serialize(fs, dataData);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Чтение данных с xml
        /// </summary>
        /// <returns>Список Id исполнителей</returns>
        public static AllData XmlRead() => DeserializeXml();
    }
}