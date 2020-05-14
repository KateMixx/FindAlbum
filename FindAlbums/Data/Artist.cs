using System;
using System.Collections.Generic;

namespace FindAlbums.Data
{
    /// <summary>
    /// Данные по исполнителю
    /// </summary>
    [Serializable]
    public class Artist
    {
        public double ArtistId { get; set; }
       
        public string ArtistName { get; set; }

        public List<AlbumInfo> Albums { get; set; }
    }

    [Serializable]
    public class AllData
    {
        public List<Artist> Artists;
    }
}
