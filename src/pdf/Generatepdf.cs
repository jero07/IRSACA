using System;
using Microsoft.Extensions.Configuration;

namespace IRSACA
{
    internal class Generatepdf
    {
        Query q = new Query();
        internal void Generatebasepdf(IConfiguration configuration)
        {
            var year = configuration["Project:YEAR"];
            var rate = q.GetACARate();
            var code = q.GetACACode(year); 
            var company = q.GetCompanyDetail(year);
            var empldetail = q.GetPayrollEmp();
            throw new NotImplementedException();
        }
    }
}