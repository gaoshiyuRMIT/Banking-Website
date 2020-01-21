using System;
using Microsoft.EntityFrameworkCore;

using Banking.Models;

namespace Banking.Data
{
    public class BankingContext : DbContext
    {
        public BankingContext(DbContextOptions<BankingContext> options)
            : base(options)
        {
        }

        public DbSet<Login> Logins { get; set; }
    }
}
