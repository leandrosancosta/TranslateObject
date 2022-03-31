using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslateObject.Core.Object
{
    public class ObjectReceived
    {
        public string? Source { get; set; }
        public string? Destination { get; set; }
        public bool HasInheritance { get; set; }
        public string? Content { get; set; } 

    }
}
