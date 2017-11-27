using System;
using System.Threading;

namespace ASynchFibonacci_NikitaDanilov
{
    class Program
    {
        static void Main(string[] args)
        {
            // The asynchronous method puts the thread id here
            int threadId;

            // Create an instance of the AsyncFibonacci class
            AsyncFibonacci cal = new AsyncFibonacci();

            // Create the delegate
            AsyncFibonacci_Caller caller = new AsyncFibonacci_Caller(cal.NthFibonacciValue);

            // A user will input the nth number in a Fibonacci sequence
            Console.WriteLine(string.Format("Please Enter a Number:"));
            string sUserInput = Console.ReadLine();

            int nthFibonacciNumber = int.Parse(sUserInput) - 1; // Fibonacci sequence starts with 0 index

            // Initiate the asychronous call
            IAsyncResult call = caller.BeginInvoke(nthFibonacciNumber, // sequence starts with 0,
                out threadId, null, null);

            // Poll while simulating work
            while (call.IsCompleted == false)
            {
                Thread.Sleep(1000);
                Console.WriteLine("Main thread id:{0} continuing to do work...",
                    Thread.CurrentThread.ManagedThreadId);
            }

            // Call EndInvoke to retrieve the value for that nth number in the Fibonacci sequence
            int nthFibonacciValue = caller.EndInvoke(out threadId, call);

            // Close the wait handle
            call.AsyncWaitHandle.Close();

            Console.WriteLine("The asychronous call executed on thread {0}", threadId);
            Console.WriteLine("The value of {0}-th number in a Fibonacci sequence is {1}", nthFibonacciNumber + 1, nthFibonacciValue);

            Console.WriteLine("\nPress <ENTER> to quit...");
            Console.ReadKey();
        }
    }
}
