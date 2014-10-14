using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyCMS.Data
{
    public interface IDbDriver
    {
        string GetCriteria(CriteriaType type);

        string FormatField(string filed, Adorns adorns);

        string FormatTable(string table);

        string FormatSQL();
    }
}
