using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RestSharp.Deserializers;

namespace FindAlbums.Data
{
    /// <summary>
    /// Результаты по информации альбомов
    /// </summary>
    public class AlbumsJson
    {
        [JsonProperty(PropertyName = "results")]
        public List<InfoDeserialize> Albums { get; set; }
    }


    public class InfoDeserialize
    {
        [DeserializeAs(Name = "artistId")]
        public double ArtistId { get; set; }

        [DeserializeAs(Name = "artistName")]
        public string ArtistName { get; set; }
        
        [DeserializeAs(Name = "collectionId")]
        public double CollectionId { get; set; }

        [DeserializeAs(Name = "collectionName")]
        public string CollectionName { get; set; }

        [DeserializeAs(Name = "releaseDate")]
        public DateTime ReleaseDate { get; set; }
    }
}