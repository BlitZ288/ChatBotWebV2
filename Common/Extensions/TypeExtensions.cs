using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coman.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsInterfaceImplemented(this Type type, string interfaceName)
        {
            return type.GetInterface(interfaceName) != null;
        }
    }
}
