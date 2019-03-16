namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using Contracts;
    using PhotoShare.Client.Core.Dtos;
    using PhotoShare.Services.Contracts;

    public class ModifyUserCommand : ICommand
    {
        private readonly IUserService userService;
        private readonly ITownService townService;

        public ModifyUserCommand(IUserService userService, ITownService townService)
        {
            this.userService = userService;
            this.townService = townService;
        }

        // ModifyUser <username> <property> <new value>
        // For example:
        // ModifyUser <username> Password <NewPassword>
        // ModifyUser <username> BornTown <newBornTownName>
        // ModifyUser <username> CurrentTown <newCurrentTownName>
        // !!! Cannot change username
        public string Execute(string[] data)
        {
            string username = data[0];
            string propertyName = data[1];
            string newValue = data[2];

            var userExists = this.userService.Exists(username);

            if (!userExists)
            {
                throw new ArgumentException($"User {username} not found!");
            }

            int userId = this.userService.ByUsername<UserDto>(username).Id;
            
            switch (propertyName)
            {
                case "Password":
                    this.ChangePassword(userId, newValue);
                    break;
                case "BornTown":
                    this.ChangeBornTown(userId, newValue);
                    break;
                case "CurrentTown":
                    this.ChangeCurrentTown(userId, newValue);
                    break;
                default:
                    throw new ArgumentException($"Property {propertyName} not found!");
            }

            return $"User {username} {propertyName} is {newValue}.";
        }

        private void ChangePassword(int userId, string newPassword)
        {
            bool hasLowerChar = newPassword.Any(c => char.IsLower(c));
            bool hasDigits = newPassword.Any(c => char.IsDigit(c));
            if (!hasLowerChar || !hasDigits)
            {
                throw new ArgumentException($"Value [{newPassword}] not valid.{Environment.NewLine}[Invalid password!]");
            }

            this.userService.ChangePassword(userId, newPassword);
        }

        private void ChangeBornTown(int userId, string newBornTownName)
        {
            var townExists = this.townService.Exists(newBornTownName);

            if (!townExists)
            {
                throw new ArgumentException($"Value [{newBornTownName}] not valid.{Environment.NewLine}Town {newBornTownName} not found!");
            }

            int townId = this.townService.ByName<TownDto>(newBornTownName).Id;

            this.userService.SetBornTown(userId, townId);
        }

        private void ChangeCurrentTown(int userId, string newCurrentTownName)
        {
            var townExists = this.townService.Exists(newCurrentTownName);

            if (!townExists)
            {
                throw new ArgumentException($"Value [{newCurrentTownName}] not valid.{Environment.NewLine}Town {newCurrentTownName} not found!");
            }

            int townId = this.townService.ByName<TownDto>(newCurrentTownName).Id;

            this.userService.SetCurrentTown(userId, townId);
        }
    }
}
