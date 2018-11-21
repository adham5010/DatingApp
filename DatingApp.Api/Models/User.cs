using System;
using System.ComponentModel.DataAnnotations;

namespace DatingApp.Api.Models {
    public class User {
        public User()
        {
            this.UserId = new Guid();
        }
        [Key]
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}