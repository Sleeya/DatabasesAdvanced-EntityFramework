namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using Contracts;
    using PhotoShare.Client.Core.Dtos;
    using PhotoShare.Services.Contracts;

    public class AddFriendCommand : ICommand
    {
        private readonly IUserService userService;

        public AddFriendCommand(IUserService userService)
        {
            this.userService = userService;
        }

        // AddFriend <username1> <username2>
        public string Execute(string[] data)
        {
            string username = data[0];
            string friendName = data[1];

            var userExists = this.userService.Exists(username);
            var friendExists = this.userService.Exists(friendName);

            if (!userExists)
            {
                throw new ArgumentException($"User {username} not found!");
            }

            if (!friendExists)
            {
                throw new ArgumentException($"User {friendName} not found!");
            }

            var user = this.userService.ByUsername<UserFriendsDto>(username);
            var friend = this.userService.ByUsername<UserFriendsDto>(friendName);

            bool isSendRequestFromUser = user.Friends.Any(x => x.Username == friend.Username);
            bool isSendRequestFromFriend = friend.Friends.Any(x => x.Username == user.Username);

            if (isSendRequestFromUser && isSendRequestFromFriend)
            {
                throw new InvalidOperationException($"{friend.Username} is already friend to {user.Username}!");
            }
            else if(isSendRequestFromUser && !isSendRequestFromFriend)
            {
                throw new InvalidOperationException($"Request is alraedy send!");
            }
            else if (isSendRequestFromFriend && !isSendRequestFromUser)
            {
                throw new InvalidOperationException($"Request is alraedy send!");
            }

            this.userService.AddFriend(user.Id, friend.Id);

            return $"Friend {friend.Username} added to {user.Username}";
        }
    }
}
