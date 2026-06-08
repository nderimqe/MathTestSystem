using MathTestSystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTestSystem.Application.Services
{
    public class MathProcessor : IMathProcessor
    {
        public double Evaluate(string expression)
        {
            try
            {
                var table = new DataTable();
                var result = table.Compute(expression, null);

                return Convert.ToDouble(result);
            }
            catch
            {
                return double.NaN;
            }
        }
    }
}
