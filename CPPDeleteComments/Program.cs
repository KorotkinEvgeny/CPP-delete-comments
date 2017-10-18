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

        public static string path = "С++ConsoleTestApp.cpp";
        //public static bool isMultilineComment = false;
        //public static int firstStringOfMultilineComment;
        //public static string path = Directory.GetCurrentDirectory();

        static void Main(string[] args)
        {
            FinderCPPFiles finderCPP = new FinderCPPFiles();
            foreach (string filePath in finderCPP.GetFilePathsList())
            {
                WriterCPPFile writerCPP = new WriterCPPFile();
                writerCPP.ScanFile(new ReaderCPPFile().GetAllLines(filePath),filePath);

            }


            //TODO at first check all strings with "" and ''

            //string[] str = File.ReadAllLines(path);

            //List<string> listRows = new List<string>(File.ReadAllLines(path));
            //using(StreamWriter sw = new StreamWriter(path))
            //{
            //    foreach (string item in listRows)
            //    {
            //        if (isMultilineComment)
            //        {
            //            //TODO нельзщя менять коллекцию перед окончанием обхода
            //            //TODO создать класс для хранения объекта с началбной и конечной позицией (структуру)
            //            if (item.Contains("*/"))
            //            {
            //                listRows.RemoveRange(firstStringOfMultilineComment + 1, listRows.IndexOf(item) - firstStringOfMultilineComment - 1);
            //                isMultilineComment = false;
            //            }
            //        }
            //        else
            //        {
            //            if (item.Contains("/*"))
            //            {
            //                isMultilineComment = true;
            //                firstStringOfMultilineComment = listRows.IndexOf(item);
            //                int index = item.IndexOf("/*");
            //                string editedString = item.Remove(index);
            //                sw.WriteLine(editedString);
            //            }
            //            else if (item.Contains("//"))
            //            {
            //                int index = item.IndexOf("//");
            //                string editedString = item.Remove(index);
            //                sw.WriteLine(editedString);
            //            }
            //            else
            //            {
            //                sw.WriteLine(item);
            //            }
            //        }

            //        Console.WriteLine(item);
            //    }
            //    sw.Close();
            Console.ReadLine();
        //  }
            
        }
    }
}
