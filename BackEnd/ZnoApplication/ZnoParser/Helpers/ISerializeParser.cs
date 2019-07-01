using System;
using System.Collections.Generic;
using System.Text;
using Zno.Parser.Models;

namespace Zno.Parser.Helpers
{
    public interface ISerializeParser
    {
        HtmlSubject[] Deserialize();
        void Serialize(HtmlSubject[] lists);
    }
}
