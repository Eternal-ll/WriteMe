﻿namespace Website.ViewModels.Friends
{
    public class FriendViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string PhotoPath { get; set; }
        public string FriendshipType { get; set; }
        public ApplicationState ApplicationState { get; set; }

        public bool IsFitsCondition(string filter) =>
            Name.Contains(filter) || Surname.Contains(filter) || Patronymic.Contains(filter);
    }

    public enum ApplicationState
    {
        incoming=1,
        outgoing=2,
        friend=3
    }
}
