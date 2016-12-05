using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace util
{
    public class serializer
    {
        public static string JsonSerialize(object value)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(value);
        }

        public T JsonDesiariliza<T>(string value)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(value);
        }


    }
}
