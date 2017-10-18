using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPPDeleteComments
{
    class ReaderCPPFile
    {
        private List<string> fileRowsList;

        public ReaderCPPFile()
        {
            this.fileRowsList = new List<string>();
        }

        public List<string> GetAllLines(string path)
        {
            fileRowsList.AddRange(File.ReadAllLines(path));
            return fileRowsList;
        }
    }
}
