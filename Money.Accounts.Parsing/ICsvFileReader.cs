using System;
using System.Collections.Generic;

namespace Money.Accounts.Parsing
{
    public interface ICsvFileReader
    {
        IEnumerable<T> MapRows<T>(string path, Action<T, string[]> mapper) where T : new();
        IEnumerable<T> MapRows<T>(string path, string headerRow, Action<T, string[]> mapper) where T : new();
        IEnumerable<T> MapRows<T>(string path, string headerRow, string treatAsColumnPattern, Action<T, string[]> mapper) where T : new();
    }
}
