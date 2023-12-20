using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelistBlazor.Common.Interface
{
    public interface IDataComponent
    {
        Dictionary<string, string> GetComponentData();
    }
}
