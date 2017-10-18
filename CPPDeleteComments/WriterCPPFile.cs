using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPPDeleteComments
{
    class WriterCPPFile
    {
        private List<StringRange> stringsRangeInFile;
        private List<StringRange> stringsMultilineCommentsRange;
        private List<string> allFileRowsWithoutSingleLineComments;
        private List<string> allFileRows;

        public WriterCPPFile()
        {
            stringsRangeInFile = new List<StringRange>();
            stringsMultilineCommentsRange = new List<StringRange>();
            allFileRowsWithoutSingleLineComments = new List<string>();

        }

        public void ScanFile(List<string> fileRows, string path)
        {
            allFileRows = fileRows;
            SearchAllFileForStrings('\"');
            ReWriteFile(path);
        }

        /// <summary>
        /// Method for getting all strings variable location in file. 
        /// Check all rows of file to find string variables
        /// Create an object of StringRange class which contain number of row where we detected double quotes
        /// Also contain the numner of row where we detected second double quotes (it helps if we have multiline comment)
        /// Also we stored position of double qoutes in string, where it was founded (it helps in situations where comment located after code in one string)       
        /// We fill up stringsRangeInFile list
        /// </summary>

        private void SearchAllFileForStrings(char quotesPattern)
        {
            int startFileStringIndex = 0;
            int endFileStringIndex;
            int indexFinalElement;
            bool isMultilineString = false;

            foreach (string singleLine in allFileRows)
            {
                if (singleLine.Contains(quotesPattern))
                {
                    if (!isMultilineString)
                    {
                        startFileStringIndex = allFileRows.IndexOf(singleLine);
                        for (int i = singleLine.IndexOf(quotesPattern) + 1; i <= singleLine.Length; i++)
                        {
                            if (singleLine[i] == quotesPattern)
                            {
                                indexFinalElement = i;
                                endFileStringIndex = allFileRows.IndexOf(singleLine);
                                stringsRangeInFile.Add(new StringRange(startFileStringIndex, endFileStringIndex, indexFinalElement));
                                isMultilineString = false;
                                break;
                            }
                            else
                            {
                                isMultilineString = true;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < singleLine.Length; i++)
                        {
                            if (singleLine[i] == quotesPattern)
                            {
                                indexFinalElement = i;
                                endFileStringIndex = allFileRows.IndexOf(singleLine);
                                stringsRangeInFile.Add(new StringRange(startFileStringIndex, endFileStringIndex, indexFinalElement));
                                isMultilineString = false;
                                break;
                            }
                        }
                    }

                }
            }

        }

        private void ReWriteFile(string path)
        {
            CheckAndRemoveBackslashDoubleComments();
            CheckMultilineComments();
            DeleteComments();
            WriteInFile(path);

        }

        /// <summary>
        /// Method for getting multiline comments. 
        /// Store rows indexes where comment begin and end
        /// Store positions of comments in their rows
        /// We fill up stringsMultilineCommentsRange list
        /// </summary>

        private void CheckMultilineComments()
        {
            int startFileStringWithCommentIndex = 0;
            int indexStartCommentElement = 0;

            int endFileStringWithCommentIndex = 0;
            int indexFinalCommentElement = 0;

            bool isMultilineComment = false;

            int rangeObjectsIterator = 0;

            foreach (string singleString in allFileRows)
            {
                if (!isMultilineComment)
                {
                    if (singleString.Contains("/*"))
                    {
                        startFileStringWithCommentIndex = allFileRows.IndexOf(singleString, endFileStringWithCommentIndex);

                        indexStartCommentElement = singleString.IndexOf("/*");

                        stringsMultilineCommentsRange.Add(new StringRange(startFileStringWithCommentIndex, endFileStringWithCommentIndex, indexStartCommentElement, indexFinalCommentElement));

                        if (singleString.Contains("*/"))
                        {
                            endFileStringWithCommentIndex = allFileRows.IndexOf(singleString, startFileStringWithCommentIndex);
                            indexFinalCommentElement = singleString.IndexOf("*/");
                            stringsMultilineCommentsRange[rangeObjectsIterator].EndStringRange = endFileStringWithCommentIndex;
                            stringsMultilineCommentsRange[rangeObjectsIterator].IndexFinalElement = indexFinalCommentElement;
                            rangeObjectsIterator += 1;
                            isMultilineComment = false;
                        }
                        else
                        {
                            isMultilineComment = true;
                        }

                    }
                }
                else
                {
                    if (singleString.Contains("*/"))
                    {
                        endFileStringWithCommentIndex = allFileRows.IndexOf(singleString, startFileStringWithCommentIndex);
                        indexFinalCommentElement = singleString.IndexOf("*/");

                        stringsMultilineCommentsRange[rangeObjectsIterator].EndStringRange = endFileStringWithCommentIndex;
                        stringsMultilineCommentsRange[rangeObjectsIterator].IndexFinalElement = indexFinalCommentElement;
                        isMultilineComment = false;
                        rangeObjectsIterator += 1;
                    }


                }

            }

        }
        /// <summary>
        /// Find all double slash comments
        /// remove it
        /// Fill up allFileRowsWithoutSingleLineComments list
        /// </summary>
        private void CheckAndRemoveBackslashDoubleComments()
        {
            foreach (string singleString in allFileRows)
            {
                if (singleString.Contains("//"))
                {
                    if (CheckIfCharSymbol(singleString))
                    {
                        allFileRowsWithoutSingleLineComments.Add(singleString);
                        continue;
                    }

                    //Another way of detecting char symbol, but method helps to manipulate with single quotes range
                    //if (singleString.Contains("'//'"))
                    //{
                    //    allFileRowsWithoutSingleLineComments.Add(singleString);
                    //    continue;
                    //}
                    foreach (StringRange singleRange in stringsRangeInFile)
                    {
                        if (allFileRows.IndexOf(singleString) < singleRange.StartStringRange)
                        {
                            allFileRowsWithoutSingleLineComments.Add(singleString.Remove(singleString.IndexOf("//")));
                            break;
                        }

                        else if (allFileRows.IndexOf(singleString) == singleRange.EndStringRange)
                        {
                            if (singleString.IndexOf("//") > singleRange.IndexFinalElement)
                            {
                                allFileRowsWithoutSingleLineComments.Add(singleString.Remove(singleString.IndexOf("//")));
                                break;
                            }
                            else
                            {
                                allFileRowsWithoutSingleLineComments.Add(singleString);
                                break;
                            }
                        }
                    }
                }
                else
                {
                    allFileRowsWithoutSingleLineComments.Add(singleString);
                }
            }
        }


        private bool CheckIfCharSymbol(string stringWithBackslash)
        {
            if (stringWithBackslash[stringWithBackslash.IndexOf("//") - 1 < 0 ? 0 : stringWithBackslash.IndexOf("//") - 1] == '\'' 
                && 
                stringWithBackslash[stringWithBackslash.IndexOf("//") + 2] == '\'')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Fill up allFilesRows list with data from allFileRowsWithoutSingleLineComments list
        /// Take substrings without comment part
        /// delete ranges between multiline comments
        /// </summary>
        private void DeleteComments()
        {
            allFileRows.Clear();
            allFileRows.AddRange(allFileRowsWithoutSingleLineComments);

            foreach (StringRange singleRange in stringsMultilineCommentsRange)
            {

                allFileRows[singleRange.StartStringRange] = allFileRows[singleRange.StartStringRange].Substring(0, singleRange.IndexStartElement);
                allFileRows[singleRange.EndStringRange] = allFileRows[singleRange.EndStringRange].Substring(singleRange.IndexFinalElement - 2 <  0 ? 0 : singleRange.IndexFinalElement-2, allFileRows[singleRange.EndStringRange].Length-2);

                for (int i = singleRange.StartStringRange + 1; i < singleRange.EndStringRange; i++)
                {
                    allFileRows[i] = "";
                }
            }



        }
        private void WriteInFile(string path)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {

                foreach (string singleString in allFileRows)
                {
                    sw.WriteLine(singleString);
                }
                sw.Close();
            }
        }
    }
}