using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling
{
    public interface IPositiveIntegerPrinter : IPrinter<PositiveInteger>
    {
        void PrintValue(int value);
    }
}
