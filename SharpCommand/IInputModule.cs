using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpCommand
{
    public interface IInputModule
    {
        string[] GetPressedKeys();
    }
}
