using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Project
{
    public static class JsonReader
    {
        public static void CreateJson(PlayerData data,string jsonName)
        {
            var json = JsonConvert.SerializeObject(data);
            File.WriteAllText($"{jsonName}.json", json);
        }
        public static PlayerData ReadJson(string jsonName)
        {
            return JsonConvert.DeserializeObject<PlayerData>(File.ReadAllText($"{jsonName}.json")); ;
        }
    }
}
