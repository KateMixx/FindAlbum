using System.Collections.Generic;
using Newtonsoft.Json;
using RestSharp.Deserializers;

namespace FindAlbums.Data
{
    /// <summary>
    /// Результаты ArtistIds 
    /// </summary>
    public class ArtistsJson
    {
        [JsonProperty(PropertyName = "results")]
        public List<ArtistsId> ArtistIds { get; set; }
    }


    public class ArtistsId
    {
        [DeserializeAs(Name = "artistId")]
        public double ArtistId { get; set; }

    }
}