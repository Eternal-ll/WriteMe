﻿using System.Collections.Generic;
using WriteMe.Database.DAL.Entities;

namespace Website.ViewModels
{
    public class ProfileViewModel
    {
        public string FullName => (User.Name + " " + User.Surname + " " + User.Patronymic).Replace("  ", " ");
        public User User { get; set; }
        public bool IsOwner { get; set; }
        public bool IsMod { get; set; }
        public bool IsFriend { get; set; }
        public IEnumerable<Post> UserPosts { get; set; }
    }
}
