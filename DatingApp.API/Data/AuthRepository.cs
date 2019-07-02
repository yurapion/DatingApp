using System;
using System.Threading.Tasks;
using DatingApp.API.Model;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;

        public AuthRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<User> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);

            if (user == null) return null;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt)) return null;

            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
               using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
           { 
             var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != passwordHash[i]) return false;
            }
               
           }
           return true;
        }

        public async Task<User> Register(User user, string password)
        {
            
           var HashSalt = CreatePasswordHash(password);
            user.PasswordHash = HashSalt.passwordHash;
            user.PasswordSalt = HashSalt.passwordSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private PasswordHashSaltValues CreatePasswordHash(string password)
        {
           // System.Security.Cryptography.HMACSHA512 hmac1;
           using (var hmac = new System.Security.Cryptography.HMACSHA512())
           { 
             //  hmac1 = hmac;
                return new PasswordHashSaltValues(hmac.Key,hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));
           }
        }

        public async Task<bool> UserExsist(string username)
        {
           if (await _context.Users.AnyAsync(u => u.UserName == username)) return true;

           return false;
        }
    }
}

    class PasswordHashSaltValues
    {
        public PasswordHashSaltValues(byte[] key, byte[] hash)
        {
            passwordSalt = key;
            passwordHash = hash;
        }
        public byte[] passwordHash { get; set; }
        public byte[] passwordSalt { get; set; }
    }