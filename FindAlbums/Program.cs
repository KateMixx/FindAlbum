using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using FindAlbums.Data;
using FindAlbums.Helper;
using Newtonsoft.Json;
using static FindAlbums.Helper.XmlUse;

namespace FindAlbums
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataBase = XmlRead();

            Console.WriteLine("Введите исполнителя:");
            string name = Console.ReadLine();

            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Не введен исполнитель");
                Main(null);
            }

            var artistsId = RestHelper.GetAtristsId(name);

            if (artistsId == null) 
            {
                Console.WriteLine("Ошибка соединения, смотрим в базе ...");
                if (dataBase?.Artists != null && (dataBase != null && dataBase.Artists.Any(i => i.ArtistName.ToLower().Contains(name.ToLower()))))
                {
                    InfoToConsole(dataBase.Artists.Where(i => i.ArtistName.ToLower().Contains(name.ToLower())).ToList());
                }
                else
                {
                    Console.WriteLine("Данных по исполнителю не найдено");
                }
            }

            else if (artistsId.Count == 0)
            {
                Console.WriteLine("Данных по исполнителю не найдено, смотрим в базе ...");
                if (dataBase?.Artists != null && (dataBase != null && dataBase.Artists.Any(i => i.ArtistName.ToLower().Contains(name.ToLower()))))
                {
                    InfoToConsole(dataBase.Artists.Where(i => i.ArtistName.ToLower().Contains(name.ToLower())).ToList());
                }
                else
                {
                    Console.WriteLine("Данных по исполнителю не найдено");
                }
            }

            else
            {
                // поиск альбомов по Id исполнителя
                foreach (var artistId in artistsId)
                {
                    var artistList = RestHelper.GetAlbumsInfo(artistId.ArtistId);
                    InfoToConsole(artistList, dataBase);
                }
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Вывод информации на консоль
        /// </summary>
        /// <param name="artistsId">Список информации на консоль</param>
        /// <param name="dataBase">БД (если данные не из БД)</param>
        public static void InfoToConsole(List<Artist> artistList, AllData dataBase = null)
        {
            artistList?.ForEach(item =>
            {
                Console.WriteLine($"Результаты поиска : {item.ArtistName} (Id = {item.ArtistId})");
                item.Albums?.ForEach(al =>
                {
                    Console.WriteLine($"{al.ReleaseDate.Date} --- {al.CollectionId} --- {al.CollectionName}");
                });

                if(dataBase?.Artists!= null && dataBase.Artists.All(i=> i.ArtistId != item.ArtistId))
                    dataBase.Artists.Add(item);
            });

            if(dataBase != null)
                SerializeXml(dataBase);
        }
    }
}
