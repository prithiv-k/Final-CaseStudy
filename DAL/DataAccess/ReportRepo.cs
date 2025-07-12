using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public class ReportRepo : IReportRepo
    {
        private readonly string _reportPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "reports", "compliance");

        public List<string> GetComplianceReports()
        {
            if (!Directory.Exists(_reportPath))
                return new List<string>();

            return Directory.GetFiles(_reportPath)
                            .Select(Path.GetFileName)
                            .ToList();
        }
    }
}