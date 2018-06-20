using System;
using Microsoft.Extensions.Configuration;

namespace IRSACA
{
    internal class Generatepdf
    {
        Query q = new Query();
        internal void Generatebasepdf(IConfiguration configuration)
        {
            q.GetACARate();
            throw new NotImplementedException();
        }
    }
}