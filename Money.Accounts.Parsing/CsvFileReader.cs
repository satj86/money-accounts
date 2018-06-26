using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Money.Accounts.Parsing
{
    public class CsvFileReader : ICsvFileReader
    {
        public IEnumerable<T> MapRows<T>(string path, Action<T, string[]> mapper) where T : new()
        {
            return MapRows<T>(path, string.Empty, mapper);
        }

        public IEnumerable<T> MapRows<T>(string path, string headerRow, Action<T, string[]> mapper) where T : new()
        {
            return MapRows<T>(path, headerRow, string.Empty, mapper);
        }

        public IEnumerable<T> MapRows<T>(string path, string headerRow, string treatAsColumnPattern, Action<T, string[]> mapper) where T : new()
        {
            using (var reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line.Equals(string.Empty) || line.Trim().Equals(headerRow.Trim(), StringComparison.InvariantCultureIgnoreCase))
                    {
                        continue;
                    }

                    string[] values;

                    if (!string.IsNullOrEmpty(treatAsColumnPattern))
                    {
                        var columnMatcher = new Regex(treatAsColumnPattern, RegexOptions.IgnoreCase);
                        var matchedColumns = columnMatcher.Matches(line);

                        var matchIndex = -1;
                        var lineWithSubstitutedColumns = line;

                        var newColumnValues = new List<string>();

                        foreach (Match matchedColumn in matchedColumns)
                        {
                            lineWithSubstitutedColumns = lineWithSubstitutedColumns.Replace(matchedColumn.Value, $"!#{ ++matchIndex }#!");
                            newColumnValues.Add(matchedColumn.Value);
                        }

                        values = lineWithSubstitutedColumns.Split(",");

                        for (int i = 0; i < values.Length; i++)
                        {
                            if (values[i].StartsWith("!#") && values[i].EndsWith("#!"))
                            {
                                var targetMatchIndex = Convert.ToInt32(values[i].Replace("!#", "").Replace("#!", ""));
                                values[i] = newColumnValues[targetMatchIndex];
                            }
                        }
                    }
                    else
                        values = line.Split(',');

                    var item = new T();
                    mapper(item, values);

                    yield return item;
                }
            }
        }
    }
}
