using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTestSystem.Application.Interfaces
{
    public interface IMathProcessor
    {
        public double Evaluate(string expression);
    }
}
