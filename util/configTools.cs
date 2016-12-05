using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace util
{
    public class configTools
    {

        public static string getConfig(string key)
        {
            string sufix = ".DEV";
#if !DEBUG
            sufix = ".PRD";
#endif
            return System.Configuration.ConfigurationManager.AppSettings[new StringBuilder(key).Append(sufix).ToString()].ToString();
        }

    }
}
