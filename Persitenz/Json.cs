using Mining_Tool_3.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Mining_Tool_3.Persitenz
{
    public class Json
    {

        public void writeJson(List<Reminder> reminder)
        {
        string json = JsonSerializer.Serialize(reminder);
        File.WriteAllText(@".\save.json", json);           
        }

        public List<Reminder> readJson()
        {
            string curFile = @".\save.json";
            if (File.Exists(curFile))
            {
                var json = File.ReadAllText(curFile);
                return JsonSerializer.Deserialize<List<Reminder>>(json);
            }
            return new List<Reminder>();
        }
    }
}
