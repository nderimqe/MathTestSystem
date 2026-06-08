using MathTestSystem.Application.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTestSystem.Application.Interfaces
{
    public interface IXMLService
    {
        public List<TaskResultDto> Process(Stream xmlStream);
    }
}
