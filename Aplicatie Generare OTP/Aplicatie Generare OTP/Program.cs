using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Insert your ID and the date (YYYY-MM-DD HH:mm:ss) to generate a password:");
        Console.Write("ID: ");

        int id = int.Parse(Console.ReadLine());
        Console.Write("Date: ");

        DateTime date = DateTime.Parse(Console.ReadLine());
        string password = GeneratingPassword();

        Console.WriteLine("Password: " + password);
        Console.WriteLine("Your password is valid for 15 seconds.");

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        DateTime startTime = DateTime.Now;
        DateTime targetTime = startTime.AddSeconds(15);

        string question = "Do you want to check if your password is still valid? (yes/no): ";
        Console.WriteLine(question);

        while (DateTime.Now <= targetTime)
        {
            if (Console.KeyAvailable)
            {
                    Console.WriteLine();
                    string response = Console.ReadLine();

                    if (response.ToLower() == "yes")
                    {
                        if (IsPasswordValid(startTime))
                        {
                            Console.WriteLine("Your password is still valid.");
                           
                            Thread.Sleep(1000);
                        }
                        else
                        {
                            Console.WriteLine("Your password has expired.");
                        
                            break;
                        }
                    }
                    else if (response.ToLower() == "no")
                    {
                        Console.WriteLine(question);
                    }
                
            }
      
        }

        Console.WriteLine("Your password has expired.");
        stopwatch.Stop();

        Console.WriteLine(stopwatch.Elapsed.TotalSeconds.ToString("0")+"sec");
        Console.ReadLine();
    }

    static string GeneratingPassword()
    {
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        Random random = new Random();
        string password = new string(Enumerable.Repeat(chars, 10).Select(s => s[random.Next(s.Length)]).ToArray());
        return password;
    }

    static bool IsPasswordValid(DateTime startTime)
    {
        DateTime currentTime = DateTime.Now;
        TimeSpan elapsedTime = currentTime - startTime;
        return elapsedTime.TotalSeconds <= 15;
    }
}
