using System;
using System.Threading;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine($"entrée du main : {Thread.CurrentThread.ManagedThreadId}");
        var toto =  DoSomethingAsync();
        Console.WriteLine($"waiting end of dosomethingasync on thread : {Thread.CurrentThread.ManagedThreadId}");
        toto.Wait();
        Console.WriteLine($"sortie du main : {Thread.CurrentThread.ManagedThreadId}");
        Console.ReadLine();
    }

    public static async Task DoSomethingAsync()
    {
        Console.WriteLine($"entrée de DomesthingAsync : {Thread.CurrentThread.ManagedThreadId}");
        await Task.Delay(TimeSpan.FromSeconds(3)).ConfigureAwait(false);
        Console.WriteLine($"sortie de DomesthingAsync : {Thread.CurrentThread.ManagedThreadId}");
    }
}
