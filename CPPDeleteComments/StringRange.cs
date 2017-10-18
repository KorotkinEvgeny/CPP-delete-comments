using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPPDeleteComments
{
    class StringRange
    {
        public int StartStringRange { get;set; }
        public int EndStringRange { get; set; }
        public int IndexFinalElement { get; set; }
        public int IndexStartElement { get; set; }

        public StringRange(int startRange,int endRange,int indexEndElement)
        {
            StartStringRange = startRange;
            EndStringRange = endRange;
            IndexFinalElement = indexEndElement;
        }
        public StringRange(int startRange, int endRange,int indexStartElement, int indexEndElement)
        {
            StartStringRange = startRange;
            EndStringRange = endRange;
            IndexFinalElement = indexEndElement;
            IndexStartElement = indexStartElement;
        }

    }
}
