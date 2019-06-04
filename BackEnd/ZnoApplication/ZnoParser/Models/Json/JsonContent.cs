using System;
using System.Collections.Generic;
using System.Text;
using Zno.Parser.Models.Enums;

namespace Zno.Parser.Models.Json
{
    public class JsonContent
    {
        public HtmlContentType ContentQuestion { get; set; }
        public string Mark { get; set; }
        public string Data { get; set; }
    }
}
