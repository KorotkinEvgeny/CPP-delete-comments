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

        public List<string> GetAllLines(string path)
        {
            return File.ReadAllLines(path).ToList();
        }
    }
}
