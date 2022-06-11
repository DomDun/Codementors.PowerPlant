using System;

namespace PowerPlantCzarnobyl.Domain
{
    public interface IConsoleManager
    {
        void Clear();
        ConsoleKeyInfo ReadKey();
        string ReadLine();
        void Write(string text);
        void WriteLine(string text);
    }

    public class ConsoleManager : IConsoleManager
    {
        public void Clear()
        {
            Console.Clear();
        }

        public ConsoleKeyInfo ReadKey()
        {
            return Console.ReadKey();
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void Write(string text)
        {
            Console.Write(text);
        }

        public void WriteLine(string text)
        {
            Console.WriteLine(text);
        }
    }
}
