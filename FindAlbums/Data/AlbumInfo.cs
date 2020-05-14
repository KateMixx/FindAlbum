using System;
using System.Xml.Serialization;
using RestSharp.Deserializers;

namespace FindAlbums.Data
{
    /// <summary>
    /// Данные по альбому
    /// </summary>
    public class AlbumInfo
    {
        [XmlAttribute("CollectionId")]
        [DeserializeAs(Name = "collectionId")]
        public double CollectionId { get; set; }

        [XmlAttribute("CollectionName")]
        [DeserializeAs(Name = "collectionName")]
        public string CollectionName { get; set; }

        [XmlAttribute("ReleaseDate")]
        [DeserializeAs(Name = "releaseDate")]
        public DateTime ReleaseDate { get; set; }
    }
}