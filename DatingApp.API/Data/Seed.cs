using System.Collections.Generic;
using DatingApp.API.Model;

namespace DatingApp.API.Data
{
    public class Seed
    {
        private readonly DataContext _context;
        public Seed(DataContext context)
        {
            _context = context;

        }

        public void SeedUsers()
        {
            var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");
            var users = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(userData);
            foreach (var user in users)
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash("password", out  passwordSalt, out  passwordHash);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.UserName = user.UserName.ToLower();

                _context.Users.Add(user);
            }
            _context.SaveChanges();

        }

         private void CreatePasswordHash(string password, out byte[] passwordSalt, out byte[] passwordHash)
        {
           // System.Security.Cryptography.HMACSHA512 hmac1;
           using (var hmac = new System.Security.Cryptography.HMACSHA512())
           { 
             passwordSalt = hmac.Key;
             passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
           }
        }

         
    }
}