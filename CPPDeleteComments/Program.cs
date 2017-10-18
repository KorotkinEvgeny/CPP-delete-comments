using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CPPDeleteComments
{
    class Program
    {
        static void Main(string[] args)
        {
            FinderCPPFiles finderCPP = new FinderCPPFiles();
            foreach (string filePath in finderCPP.GetFilePathsList())
            {
                WriterCPPFile writerCPP = new WriterCPPFile();
                writerCPP.ScanFile(new ReaderCPPFile().GetAllLines(filePath),filePath);

            }
            Console.WriteLine("End...");   
            Console.ReadLine();
        }
    }
}
