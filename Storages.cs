using System.Collections;
using System;

namespace Storages
{
    public class Storage
    {
        public static Hashtable Extensions = new Hashtable();
        public static int i = 0;
        public void Store(string NewExt)
        {
            if (i < 100)
            {
                Extensions.Add(i, NewExt);
                i++;
            }
            else
            {
                i = 0;
            }
        }
        public void Check(string NewExt)
        {
            int Count = 0;
            foreach (DictionaryEntry Entry in Extensions)
            {
                if (Convert.ToString(Entry.Value) == NewExt)
                {
                    Count++;
                }
            }
            Console.WriteLine("This Extension has been seen {0} times." + Environment.NewLine, Count);
        }
    }
}