using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ionic.Zip;
using Ionic.Zlib;

namespace ZIPbrutforcer
{
    class Program
    {
        static void Main()
        {
            string ExistingZipFile = "1.zip";
            string BaseDirectory = "D:\\tmp";

            var allLines = File.ReadAllLines("1.txt");
            var totalCount = allLines.LongLength;
            var counter = 0L;

            Parallel.For(0, totalCount, i =>
            {
                Interlocked.Increment(ref counter);
                var password = allLines[i];
                try
                {
                    using (var stream = new FileStream(ExistingZipFile, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.SequentialScan))

                    using (ZipFile zip = ZipFile.Read(stream))
                    {
                        ZipEntry e = zip["DELtrial_14.exe"];
                        e.ExtractWithPassword(Path.Combine(BaseDirectory, Thread.CurrentThread.ManagedThreadId.ToString()), ExtractExistingFileAction.DoNotOverwrite, password);
                    }
                }
                catch (BadPasswordException)
                {
                    Console.WriteLine($"Pass # {counter} is not Ok!");
                }
                catch (ZlibException)
                {
                    Console.WriteLine("Bad Zlib happen");
                }
                catch (BadStateException)
                {
                    Console.WriteLine("Bad State happen");
                }
                catch (BadReadException)
                {
                    Console.WriteLine("Bad read happen");
                }
            });
        }
    }
}


