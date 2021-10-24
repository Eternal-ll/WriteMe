﻿using System;
using System.Collections.Generic;
using WriteMe.Database.DAL.Entities.Base;

namespace WriteMe.Database.DAL.Entities
{
    public class User : PersonEntity
    {
        public string MailAddress { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public bool IsNew { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime RegistrationDateTime { get; set; }
        public User()
        {
            ConnectionIdentifiers = new HashSet<UserConnection>();

        }
        public virtual ICollection<UserConnection> ConnectionIdentifiers { get; set; }
    }
}
