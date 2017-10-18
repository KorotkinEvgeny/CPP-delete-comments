using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPPDeleteComments
{
    class FinderCPPFiles
    {
        private static string path = Directory.GetCurrentDirectory();
        private List<string> filePaths;

        public FinderCPPFiles()
        {
            filePaths = Directory.EnumerateFiles(path, "*.cpp").ToList();
        }

        public List<string> GetFilePathsList()
        {
            return this.filePaths;
        }

    }
}
