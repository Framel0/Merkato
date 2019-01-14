using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Merkato.Lib.Models
{
    public partial class Parameters
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public static string GetValue(string name, MerkatoDbContext ctx)
        {
            var param = ctx.Parameters.SingleOrDefault(c => c.Name == name);
            if (param != null)
            {
                return param.Value;
            }

            return null;
        }

        public static T Get<T>(string name, MerkatoDbContext ctx) where T : class
        {
            var param = ctx.Parameters.SingleOrDefault(c => c.Name == name);
            if (param != null)
            {
                return JsonConvert.DeserializeObject<T>(param.Value);
            }

            return null;
        }

        public static void SaveValue(string name, string dataValue, MerkatoDbContext ctx)
        {

            var param = ctx.Parameters.SingleOrDefault(c => c.Name == name);
            if (param == null)
            {
                param = new Parameters()
                {
                    Name = name,
                    Value = dataValue,
                };
                ctx.Parameters.Add(param);
            }
            else
            {
                param.Value = dataValue;
            }
            ctx.SaveChanges();

        }


        public static void Save<T>(T obj, string name, MerkatoDbContext ctx) where T : class
        {
            var param = ctx.Parameters.SingleOrDefault(c => c.Name == name);
            if (param == null)
            {
                param = new Parameters()
                {
                    Name = name,
                    Value = JsonConvert.SerializeObject(obj)
                };
                ctx.Parameters.Add(param);
            }
            else
            {
                param.Value = JsonConvert.SerializeObject(obj);
            }
            ctx.SaveChanges();
        }

    }
}
