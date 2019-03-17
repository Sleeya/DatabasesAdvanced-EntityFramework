
using Employees.Services;
using System;

namespace Employees.App.Commands
{
    class ExitCommand : ICommand
    {

        public string Execute(params string[] args)
        {
            Environment.Exit(0);
            return String.Empty;
        }
    }
}
