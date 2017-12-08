using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Common;

namespace Day8
{
    class Program
    {
        static void Main(string[] args)
        {
            int largest = 0;
            Dictionary<string, int> registers = new Dictionary<string, int>();
            Regex inst = new Regex(@"(?<register>.+) (?<inst>inc|dec) (?<qty>[-\d]+) if (?<testreg>.+) (?<op>.{1,2}) (?<opqty>[-\d]+)");

            foreach (string line in FileIterator.Create("./input.txt"))
            {
                Match m = inst.Match(line);

                string register = m.Groups["register"].Captures[0].Value;
                string instruction = m.Groups["inst"].Captures[0].Value;
                int qty = int.Parse(m.Groups["qty"].Captures[0].Value);


                string testReg = m.Groups["testreg"].Captures[0].Value;
                string oper = m.Groups["op"].Captures[0].Value;
                int opQty = int.Parse(m.Groups["opqty"].Captures[0].Value);

                if (!registers.ContainsKey(register))
                {
                    registers.Add(register, 0);
                }

                if (!registers.ContainsKey(testReg))
                {
                    registers.Add(testReg, 0);
                }

                if (IsTestPassed(registers, testReg, oper, opQty))
                {
                    if (instruction == "inc")
                    {
                        registers[register] += qty;
                    }
                    else
                    {
                        registers[register] -= qty;
                    }
                }

                int largestTmp = registers.Max(r => r.Value);

                if (largestTmp > largest)
                {
                    largest = largestTmp;
                }
            }

            

            Console.WriteLine($"The largest value is {largest}");
            Console.ReadKey(true);
        }

        private static bool IsTestPassed(Dictionary<string, int> registers, string register, string oper, int qty)
        {
            switch (oper)
            {
                case "==":
                    return registers[register] == qty;

                case "!=":
                    return registers[register] != qty;

                case ">":
                    return registers[register] > qty;

                case ">=":
                    return registers[register] >= qty;

                case "<":
                    return registers[register] < qty;

                case "<=":
                    return registers[register] <= qty;

                default:
                    throw new Exception($"Bad operator {oper}");
            }
        }
    }
}
