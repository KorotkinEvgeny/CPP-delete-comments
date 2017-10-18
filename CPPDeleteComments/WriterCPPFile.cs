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
        private List<string> allFileRows;

        public WriterCPPFile()
        {
            stringsRangeInFile = new List<StringRange>();
            stringsMultilineCommentsRange = new List<StringRange>();
        }

        public void ScanFile(List<string> fileRows, string path)
        {
            allFileRows = fileRows;
            SearchAllFileForStrings();
            ReWriteFile(path);
        }

        private void SearchAllFileForStrings()
        {
            int startFileStringIndex=0;
            int endFileStringIndex;
            int indexFinalElement;
            bool isMultilineString = false;

            foreach (string singleLine in allFileRows)
            {
                if (singleLine.Contains("\""))
                {
                    if (!isMultilineString)
                    {
                        startFileStringIndex = allFileRows.IndexOf(singleLine);
                        for (int i = singleLine.IndexOf("\"") + 1; i <= singleLine.Length; i++)//Maybe needed +1
                        {
                            if (singleLine[i] == '"')
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
                            if (singleLine[i] == '"')
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
            CheckMultilineComments();
            CheckEndDeleteBackslashComments(path);
            DeleteMultilineComments(path);

        }

        private void CheckMultilineComments()
        {
                int startFileStringWithCommentIndex = 0;
                int indexStartCommentElement = 0;

                int endFileStringWithCommentIndex;
                int indexFinalCommentElement;

                bool isMultilineComment = false;
                foreach (string singleString in allFileRows)
                {
                    if (!isMultilineComment)
                    {
                        if (singleString.Contains("/*"))
                        {
                            
                            startFileStringWithCommentIndex = allFileRows.IndexOf(singleString);
                            indexStartCommentElement = singleString.IndexOf("/*");
                            if (singleString.Contains(@"*\"))
                            {
                                endFileStringWithCommentIndex = allFileRows.IndexOf(singleString);
                                indexFinalCommentElement = singleString.IndexOf(@"*\");
                                stringsMultilineCommentsRange.Add(new StringRange(startFileStringWithCommentIndex, endFileStringWithCommentIndex, indexStartCommentElement, indexFinalCommentElement));
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
                        if (singleString.Contains(@"*\"))
                        {
                            endFileStringWithCommentIndex = allFileRows.IndexOf(singleString);
                            indexFinalCommentElement = singleString.IndexOf(@"*\");
                            stringsMultilineCommentsRange.Add(new StringRange(startFileStringWithCommentIndex, endFileStringWithCommentIndex, indexStartCommentElement, indexFinalCommentElement));
                            isMultilineComment = false;
                            break;
                        }


                    }
   
                }

        }
        private void CheckEndDeleteBackslashComments(string path)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                foreach (string singleString in allFileRows)
                {
                    if (singleString.Contains("//"))
                    {
                        Console.WriteLine("Строка найдена");
                        foreach (StringRange singleRange in stringsRangeInFile)
                        {
                            //TODO добавитть проверку на комментарий после строки
                            if (allFileRows.IndexOf(singleString) < singleRange.StartStringRange)
                            {
                                sw.WriteLine(singleString.Remove(singleString.IndexOf("//")));
                            }
                            else if (allFileRows.IndexOf(singleString) == singleRange.EndStringRange)
                            {
                                if (singleString.IndexOf("//") > singleRange.IndexFinalElement)
                                {
                                    sw.WriteLine(singleString.Remove(singleString.IndexOf("//")));
                                }
                            }
                            else
                            {
                                sw.WriteLine(singleString.Remove(singleString.IndexOf("//")));
                            }
                        }
                    }
                    else
                    {
                        sw.WriteLine(singleString);
                    }
                }
                sw.Close();
            }
        }
        private void DeleteMultilineComments(string path)
        {
            foreach (StringRange singleRange in stringsMultilineCommentsRange)
            {
                allFileRows.RemoveRange(singleRange.StartStringRange, singleRange.EndStringRange - singleRange.StartStringRange);
            }
            Console.WriteLine("Delete this rows");
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
