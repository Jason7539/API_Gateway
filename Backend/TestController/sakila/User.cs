using System;
using System.Collections.Generic;

namespace TestController.sakila
{
    public partial class User
    {
        public User()
        {
            SaveList = new HashSet<SaveList>();
            Store = new HashSet<Store>();
            Upload = new HashSet<Upload>();
        }

        public string Username { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public bool Disabled { get; set; }
        public string UserType { get; set; }
        public string Salt { get; set; }
        public long TempTimestamp { get; set; }
        public string EmailCode { get; set; }
        public long EmailCodeTimestamp { get; set; }
        public int LoginFailures { get; set; }
        public long LastLoginFailTimestamp { get; set; }
        public int EmailCodeFailures { get; set; }
        public int PhoneCodeFailures { get; set; }

        public virtual ICollection<SaveList> SaveList { get; set; }
        public virtual ICollection<Store> Store { get; set; }
        public virtual ICollection<Upload> Upload { get; set; }
    }
}
