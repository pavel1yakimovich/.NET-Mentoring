using System;
using System.Collections.Generic;
using System.Text;

namespace Task1AdvancedCSharp
{
    public interface IDirectoryAdapter
    {
        string[] GetDirectories(string path);
        string[] GetFiles(string path);
    }
}
