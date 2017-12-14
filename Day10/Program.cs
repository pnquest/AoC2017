using System;
using System.IO;
using System.Threading;
using Common;

namespace Day10
{
    class Program
    {
        static void Main(string[] args)
        {
            //version with cheap hacker effect for fun
            Console.CursorVisible = false;
            
            
            string txtInp = File.ReadAllText("./input.txt");
            
            Console.WriteLine(txtInp);
            Thread.Sleep(1000);
            Console.Clear();


            var result = KnotHasher.ComputeKnotHash(txtInp);
            Console.WriteLine(result);
            Console.ReadKey(true);
        }
    }
}
