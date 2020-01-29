using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;

using Banking.Models;
using Banking.Data;


namespace Banking.Managers
{
    public interface ILoginManager 
    {
        public Task<Login> GetLoginAsync(string userId);
        public Task UpdateAsync(Login login, string userId);
    }

    public class LoginManager : ILoginManager
    {
        private BankingContext _context;
        private DbSet<Login> _set;
        
        public LoginManager(BankingContext context) 
        {
            _context = context;
            _set = _context.Login;
        }

        public async Task<Login> GetLoginAsync(string userId) 
        {
            return await _set.FindAsync(userId);
        }
        public async Task UpdateAsync(Login login, string userId) 
        {
            login.UserID = userId;
            _context.Update(login);
            await _context.SaveChangesAsync();
        }

    }
}
