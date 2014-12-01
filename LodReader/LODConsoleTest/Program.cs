using Fantasista.Archive.LOD;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LODConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (FileStream file = File.Open(@"C:\GOG Games\Heroes of Might and Magic 3 Complete\Data\H3ab_bmp.lod",FileMode.Open))
            {
                var archive = new LODArchive(file);
                Console.WriteLine(archive);
                foreach (var entry in archive.Entries)
                    Console.WriteLine(entry);
            }
            Console.ReadLine();
        }
    }
}
