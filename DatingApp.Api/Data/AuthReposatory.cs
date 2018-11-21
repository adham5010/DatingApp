using System;
using System.Threading.Tasks;
using DatingApp.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Api.Data {
    public class AuthReposatory : IAuthReposatory {
        private DataContext _db;
        public AuthReposatory (DataContext db) {
            this._db = db;
        }
        public async Task<bool> IsUserExists (string UserName) {
            return (await _db.Users.SingleOrDefaultAsync (a => a.UserName == UserName) != null);
        }

        public async Task<User> Login (string UserName, string Password) {
            var User = await _db.Users.SingleOrDefaultAsync (a => a.UserName == UserName);
            if (User == null || !VerifyPasswordHash (Password, User.PasswordHash, User.PasswordSalt)) return null;
            return User;
        }

        public async Task<User> Register (User User, string Password) {
            byte[] PasswordHash, PasswordSalt;
            CreatePasswordHash (Password, out PasswordHash, out PasswordSalt);
            User.PasswordSalt = PasswordSalt;
            User.PasswordHash = PasswordHash;
            await _db.Users.AddAsync (User);
            await _db.SaveChangesAsync ();
            return User;
        }

        private void CreatePasswordHash (string password, out byte[] passwordHash, out byte[] passwordSalt) {
            using (var Hcal = new System.Security.Cryptography.HMACSHA512 ()) {
                passwordSalt = Hcal.Key;
                passwordHash = Hcal.ComputeHash (System.Text.Encoding.UTF8.GetBytes (password));
            }
        }

        private bool VerifyPasswordHash (string password, byte[] passwordHash, byte[] passwordSalt) {
            using (var Hcal = new System.Security.Cryptography.HMACSHA512 (passwordSalt)) {
                var computedHash = Hcal.ComputeHash (System.Text.Encoding.UTF8.GetBytes (password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if(computedHash[i]!=passwordHash[i]){
                        return false;
                    }
                }
            }
            return true;
        
        }
    }
}