using System;
using System.Threading;

namespace ASynchFibonacci_NikitaDanilov
{
    public delegate int AsyncFibonacci_Caller(int n, out int threadId);
    public class AsyncFibonacci
    {
        // A method will return a value for that nth number in the Fibonacci sequence
        public int NthFibonacciValue(int n, out int threadId)
        {
            threadId = Thread.CurrentThread.ManagedThreadId;
            if (n <= 0) return 0; //To return the first Fibonacci number   
            if (n == 1) return 1; //To return the second Fibonacci number  
            return NthFibonacciValue(n - 1, out threadId) + NthFibonacciValue(n - 2, out threadId);
        }
    }
}
