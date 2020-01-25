using System;
using Microsoft.AspNetCore.Mvc.Rendering;

using Banking.Models;

namespace Banking.Extensions
{
    public static class BillPayPeriodExtensions
    {
        public static string ToString(this BillPayPeriod bp)
        {
            if (bp == BillPayPeriod.OnceOff)
                return "Once Off";
            return bp.ToString();
        }
    }
}
