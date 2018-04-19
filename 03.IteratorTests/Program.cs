using System;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        string[] input = Console.ReadLine().Split();

        ListIterator collectons = new ListIterator(input.Skip(1));
        string commands;
        while ((commands = Console .ReadLine()) != "END")
        {
            try
            {
                switch (commands)
                {
                    case "Move":
                        Console.WriteLine(collectons.Move());
                        break;
                    case "HasNext":
                        Console.WriteLine(collectons.HasNext());
                        break;
                    case "Print":
                        Console.WriteLine(collectons.Print());
                        break;
                }
            }
            catch (ArgumentNullException e)
            {
            }
        }
    }
}