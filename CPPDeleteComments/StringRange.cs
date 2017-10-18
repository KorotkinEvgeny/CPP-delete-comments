using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPPDeleteComments
{
    class StringRange
    {
        public int StartStringRange { get;private set; }
        public int EndStringRange { get; private set; }
        public int IndexFinalElement { get; private set; }
        public int IndexStartElement { get; private set; }

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
