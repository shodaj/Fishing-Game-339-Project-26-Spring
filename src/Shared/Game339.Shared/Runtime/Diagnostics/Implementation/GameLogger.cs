using System;

namespace Game339.Shared.Diagnostics.Implementation
{
    public class GameLogger : IGameLog
    {
        public void Info(string message)
        {
            Console.WriteLine(message);
        }

        public void Warn(string message)
        {
            Console.WriteLine(message);
        }

        public void Error(string message)
        {
            Console.WriteLine(message);
        }
    }
}