using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.LinkLabel;

namespace ScreenShotTool
{
    internal static class RtfTools
    {
        public static string CreateRtfText(List<string> lines, int defaultPointSize = 10, string font = "fswiss Helvetica")
        {
            int[] textSizes = { defaultPointSize * 2, 56, 48, 40, 32, 26, 20, 20 };
            List<int> columnSizes = new List<int>();
            var text = new StringBuilder();

            text.AppendLine("{\\rtf1\\ansi\\deff0 {\\fonttbl\\f0\\" + font + ";}\\pard");

            //foreach (var originalLine in lines)
            for (int i = 0; i < lines.Count(); i++)
            {
                string line = lines[i];

                line = SetHeading(textSizes, line);

                line = SetStyle(line, "**", "b"); // bold
                line = SetStyle(line, "__", "b"); // bold
                line = SetStyle(line, "*", "i"); // italic
                line = SetStyle(line, "_", "i"); // italic

                line = SetImage(line);

                var newColumnSizes = SetColumnWidths(line);
                if (newColumnSizes.Count > 0)
                {
                    Debug.WriteLine("New Column sizes: " + newColumnSizes.Count);
                    columnSizes = newColumnSizes;
                    continue; // skip this line, it's a "<!--" comment for Column Widths
                }

                if (line.TrimStart().StartsWith('|'))
                {
                    (line, i) = CreateTable(i, lines, columnSizes);
                }

                text.AppendLine(line);
                text.AppendLine("\\par ");
            }

            text.AppendLine("}");
            return text.ToString();
        }

        private static List<int> SetColumnWidths(string line)
        {
            List<int> result = new();
            // line is e.g.: <!---CW:2000:4000:1000:-->
            if (line.Contains("<!---CW:"))
            {
                string[] columnWidths = line.Split(':');
                foreach (string cw in columnWidths)
                {
                    if (int.TryParse(cw, out int width))
                    {
                        result.Add(width);
                    }
                }
            }
            return result;
        }

        private static (string line, int i) CreateTable(int i, List<string> lines, List<int> columSizes)
        {
            StringBuilder result = new StringBuilder();
            int tableRows = 0;
            bool foundRow = true;
            int columns = lines[i].AllIndexesOf("|").Count() -1;
            int lastColumnWidth = 0;
            
            for (int j = i; foundRow && j < lines.Count(); j++)
            {
                if (lines[j].TrimStart().StartsWith('|'))
                {
                    tableRows++;
                    foundRow = true;
                    //Debug.WriteLine($"+++   Row: {lines[j]}");
                }
                else
                {
                    foundRow = false;
                    //Debug.WriteLine($"NOT a Row: {lines[j]}");
                }
            }
            Debug.WriteLine($"CreateTable found {tableRows} rows");

            if (tableRows > 2)
            {
                
                for (int r = i; r < i + tableRows; r++)// string line in lines)
                {
                    lastColumnWidth = 0;
                    if (r == i+1) continue; // skip row with dashes that separates headings from rows
                    result.AppendLine("\\trowd\\trgaph150");
                    for (int c = 0; c < columns; c++)
                    {
                        //cellx crashes the rtf load
                        //int newWidth = (c + 1) * 3000;
                        if (columSizes.Count >= columns)
                        {
                            int newWidth = lastColumnWidth + columSizes[c];
                            result.AppendLine($"\\cellx{newWidth}");
                            Debug.WriteLine("Using new column width " + newWidth);
                            lastColumnWidth = newWidth;
                        }
                        else
                        {
                            Debug.WriteLine("Using default column widths");
                            int newWidth = (c + 1) * 2000;
                            result.AppendLine($"\\cellx{newWidth}");
                        }
                    }

                    string[] split = lines[r].Trim().Split('|');
                    for (int c = 1; c < split.Length - 1; c++) // string column in split)
                    {
                        string colWord = split[c].Trim();
                        colWord = SetStyle(colWord, "**", "b"); // bold
                        colWord = SetStyle(colWord, "__", "b"); // bold
                        colWord = SetStyle(colWord, "*", "i"); // italic
                        colWord = SetStyle(colWord, "_", "i"); // italic
                        result.Append(colWord);
                        result.AppendLine("\\intbl\\cell");
                    }
                    result.AppendLine("\\row ");
                }
                result.AppendLine("\\pard");

                return (result.ToString(), i + tableRows);
            }
            else
            {
                return (lines[i], i);
            }
        }

        private static string SetImage(string line)
        {
            if (line.Contains("![image]", StringComparison.InvariantCultureIgnoreCase))
            {
                int startTagPos = line.IndexOf("![image]", StringComparison.InvariantCultureIgnoreCase);
                int endTagPos = line.IndexOf(')') + 1;
                if (startTagPos < 0 || endTagPos < 0) return line;
                if (startTagPos >= line.Length) return line;
                if (endTagPos >= line.Length) return line.Substring(0, startTagPos); // tag end is at the end of the line, skip the end string
                return line.Substring(0, startTagPos) + line.Substring(endTagPos);
            }
            return line;
        }

        private static string SetStyle(string line, string tag, string rtfTag)
        {
            if (line.Contains(tag))
            {
                StringBuilder sb = new StringBuilder();
                List<int> matches = line.AllIndexesOf(tag).ToList();
                if (matches.Count > 0)
                {
                    sb.Append(line.Substring(0, matches[0]));

                    for (int i = 0; i < matches.Count(); i++)
                    {
                        if (matches.Count() >= i)
                        {
                            sb.Append($"\\{rtfTag} ");
                            if (matches[i] < line.Length && matches[i+1] < line.Length)
                            {
                                try
                                {
                                    string words = line.Substring(matches[i], matches[i + 1] - matches[i]);
                                    sb.Append(words.Replace(tag, ""));
                                }
                                catch
                                {
                                    Debug.WriteLine($"SetStyle, match index {matches[i]} and {matches[i + 1]}, line length is {line.Length} from:\n{line}");   
                                }
                            }
                            
                            sb.Append($"\\{rtfTag}0 ");
                            i++;
                        }
                        else
                        {
                            sb.Append($"\\{rtfTag} ");
                            sb.Append(line.Substring(matches[i]));
                            sb.Append($"\\{rtfTag}0 ");
                        }
                    }
                    line = sb.ToString();
                }
            }
            return line;
        }

        private static string SetHeading(int[] textSizes, string line)
        {
            if (line.TrimStart().StartsWith("#") && line.Length > 6)
            {
                StringBuilder sb = new StringBuilder();
                string lineStart = line.Substring(0, 6);
                string lineEnd = line.Substring(6);
                int headingSize = lineStart.Split('#').Length; // smaller numbers are bigger text
                sb.Append($"\\fs{textSizes[headingSize]} ");
                sb.Append(lineStart.Replace("#", "").TrimStart());
                sb.Append(lineEnd);
                sb.Append($"\\fs{textSizes[0]}");
                line = sb.ToString();
            }
            return line;
        }

        public static IEnumerable<int> AllIndexesOf(this string str, string searchstring)
        {
            int minIndex = str.IndexOf(searchstring);
            while (minIndex != -1)
            {
                yield return minIndex;
                minIndex = str.IndexOf(searchstring, minIndex + searchstring.Length);
            }
        }
    }
}
