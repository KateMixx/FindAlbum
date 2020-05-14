using System;
using System.Collections.Generic;
using System.Linq;
using FindAlbums.Data;
using Newtonsoft.Json;
using RestSharp;

namespace FindAlbums.Helper
{
    public class RestHelper
    {
        /// <summary>
        /// Получение данный id исполнителя по названию
        /// </summary>
        /// <param name="artstName">Название испольнителя</param>
        /// <returns>Список Id с упоминанием имени исполнителя</returns>
        public static List<ArtistsId> GetAtristsId(string artstName)
        {
            artstName = artstName.Replace(" ", "+");

            var response = ResponseExecute($"https://itunes.apple.com/search?term={artstName}");

            return response != null && response.IsSuccessful
                ? ((JsonConvert.DeserializeObject<ArtistsJson>(response.Content))?.ArtistIds ??
                   throw new InvalidOperationException())
                .GroupBy(ar => ar.ArtistId)
                .Select(g => g.First()).ToList()
                : null;
        }

        /// <summary>
        /// Получение списка альбомов по Id исполнителя
        /// </summary>
        /// <param name="artistId">Id исполнителя</param>
        /// <returns>Информания по альбомам</returns>
        public static List<Artist> GetAlbumsInfo(double artistId)
        {
            var response = ResponseExecute($"https://itunes.apple.com/lookup?id={artistId}&entity=album");
            return response != null && response.IsSuccessful
                ? ((JsonConvert.DeserializeObject<AlbumsJson>(response.Content))?.Albums 
                   ?? throw new InvalidOperationException())
                .Where(i => i.CollectionId != 0)
                .GroupBy(i => i.ArtistId)
                .Select(i => new Artist()
                {
                    ArtistId = artistId,
                    ArtistName = i.Select(a => a.ArtistName).FirstOrDefault(),
                    Albums = i.OrderBy(o => o.ReleaseDate).Select(a => new AlbumInfo()
                    {
                        CollectionId = a.CollectionId,
                        CollectionName = a.CollectionName,
                        ReleaseDate = a.ReleaseDate
                    }).ToList()
                })
                .ToList() : null;
        }

        private static IRestResponse ResponseExecute(string text)
        {
            var client = new RestClient(text);
            var request = new RestRequest(Method.POST);
            return client.Execute(request);
        }
    }
}