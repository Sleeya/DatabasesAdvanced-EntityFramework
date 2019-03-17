using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Employees.App
{
    class Engine
    {
        private IServiceProvider serviceProvider;
        bool isRunning;

        public Engine(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.isRunning = true;
        }

        internal void Run()
        {
            while (isRunning)
            {
                string input = Console.ReadLine();

                string[] commandTokens = input.Split().ToArray();

                string commandName = commandTokens[0];

                string[] commandArgs = commandTokens.Skip(1).ToArray();

                var command = CommandParser.ParseCommand(this.serviceProvider, commandName);

                var result = command.Execute(commandArgs);

                Console.WriteLine(result);
            }
        }
    }
}
