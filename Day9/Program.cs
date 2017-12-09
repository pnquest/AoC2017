using System;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace Day9
{
    class Program
    {
        static void Main(string[] args)
        {
            Group curGroup = null;
            using (StreamReader sr =
                new StreamReader(new FileStream("./input.txt", FileMode.Open, FileAccess.Read, FileShare.Read)))
            {
                
                char[] buffer = new char[1];
                bool lastWasCanceled = false;
                bool inGarbage = false;
                while (!sr.EndOfStream)
                {
                    sr.Read(buffer, 0, 1);
                    char c = buffer[0];
                    if (lastWasCanceled)
                    {
                        curGroup.AppendContent(c);
                        lastWasCanceled = false;
                    }
                    else if (c == '!' && inGarbage)
                    {
                        lastWasCanceled = true;
                        curGroup.AppendContent(c);
                    }
                    else if (c == '<')
                    {
                        if (inGarbage)
                        {
                            curGroup.GarbageCount++;
                        }
                        inGarbage = true;
                        curGroup.AppendContent(c);
                    }
                    else if (c == '>')
                    {
                        inGarbage = false;
                        curGroup.AppendContent(c);
                    }
                    else if (c == '{' && !inGarbage)
                    {
                        Group g = new Group();
                        g.Parent = curGroup;
                        curGroup?.Groups.Add(g);

                        curGroup = g;
                        g.AppendContent(c);
                        
                    }
                    else if (c == '}' && !inGarbage)
                    {
                        curGroup.AppendContent(c);
                        if (curGroup.Parent != null)
                        {
                            curGroup = curGroup.Parent;
                        }
                    }
                    else
                    {
                        if (inGarbage)
                        {
                            curGroup.GarbageCount++;
                        }
                        curGroup.AppendContent(c);
                    }
                }
            }

            Console.WriteLine($"Total Garbage is {curGroup.GarbageSum}");
            Console.ReadKey(true);
        }
    }
}
