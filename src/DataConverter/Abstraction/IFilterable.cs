using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataConverter.Models;

namespace DataConverter.Abstraction
{
    public interface IFilterable
    {
        IFilterable Filter(Func<CsvAddressInfo, bool> filter);
    }
}
