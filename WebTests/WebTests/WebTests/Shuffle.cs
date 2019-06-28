using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebTests
{
    public static class Operation
    {
        public static List<int> GetRandomValues(List<int> list, int count)
        {
            //15 случайных вопросов из 126
            var new_list = new List<int>();
            var j = 0;
            if (list != null)
            {
                var random = new Random();
                for (int i = 0; i <= list.Count; i++)
                {
                    int randomIndex = random.Next(1, list.Count);
                    var flag = new_list.Contains(list[randomIndex]);
                    if (!flag)
                    {
                        new_list.Add(list[randomIndex]);
                        j++;
                    }
                    if (j == count) break;
                }
                return new_list;
            }
            return null;
        }
        public static List<string> PermOtveti(List<string> list)
        {
            List<string> perlist = list;// new List<string>();
            string tmp;
            Random rand = new Random();

            for (int i = 0; i < list.Count; i++)
            {
                tmp = list[i];
                list.RemoveAt(i);
                perlist.Insert(rand.Next(0, list.Count), tmp);
            }
            return perlist;
        }
    }
}