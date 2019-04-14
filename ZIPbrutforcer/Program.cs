using Ionic.Zip;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ZIPbrutforcer
{
    class Program
    {
        static void Main()
        {
            string zipFileName = "1.zip";
            string BaseDirectory = "D:\\Temp";

            var allLinesPassword = File.ReadAllLines("D:\\Temp\\10m.txt");
            var totalCount = allLinesPassword.LongLength;

            using (ZipFile zip = ZipFile.Read(Path.Combine(BaseDirectory, zipFileName)))
            {
                Parallel.For(0, allLinesPassword.Length, i =>
                {
                    try
                    {
                        zip.Password = allLinesPassword[i];
                        zip.ExtractAll(BaseDirectory, ExtractExistingFileAction.DoNotOverwrite);
                    }
                    catch (BadPasswordException)
                    {
                    }
                });
            }
        }
    }
}


