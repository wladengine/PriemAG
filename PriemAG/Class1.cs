using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Priem
{
    class Class1
    {
        void foo()
        {
            List<KeyValuePair<string, string>> lstPairs = new List<KeyValuePair<string, string>>();
            lstPairs.Add(new KeyValuePair<string, string>("Id", "1"));
            lstPairs.Add(new KeyValuePair<string, string>("Id", "2"));
            lstPairs.Add(new KeyValuePair<string, string>("Id", "3"));

            //преобразовать в строку вида @Id=1, @Id=2, @Id=3

            String s = from KeyValuePair<string, string> map in lstPairs
                       select "@" + map.Key + "=" + map.Value;
        }


    }
}
