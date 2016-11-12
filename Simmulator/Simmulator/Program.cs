using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmulator
{
    class Program
    {
        static string DecitoBi(string mcode)
            
        {
            string bi;
            bi   = Convert.ToString(Convert.ToInt32(mcode), 2);
            return bi;
        }
        static void Main(string[] args)
        {
            string filelocation = @"C:\Users\Subin\Documents\GitHub\ComArc\MCcode.txt";
            string[] lines = System.IO.File.ReadAllLines(filelocation);
            int lenght;
            string front = "";// empty string
            foreach (string s in lines)
            {
               lenght =  DecitoBi(s).Length;
                if(lenght < 32)
                {
                    front = "";// empty string
                    for ( int i = 0; i < 32 - lenght; i++)
                    {
                        front += "0";
                    }
                    front += DecitoBi(s);
                }
                else
                {
                    front = DecitoBi(s);
                }
                Console.WriteLine(front);
                string opcode = front.Substring(7, 3);
                Console.Write( "opcode : " + opcode);
                switch (opcode)
                {
                    case "000":
                        Console.WriteLine("  Add");
                        break;
                    case "001":
                        Console.WriteLine(" Nand");
                        break;
                    case "010":
                        Console.WriteLine(" LW");
                        break;
                    case "011":
                        Console.WriteLine(" SW");
                        break;
                    case "100":
                        Console.WriteLine(" Beq");
                        break; 
                    case "101":
                        Console.WriteLine(" Jalr");
                        break;
                    case "110":
                        Console.WriteLine(" Halt");
                        break;
                    case "111":
                        Console.WriteLine(" Noop");
                        break;

                }
                    
            }


            Console.ReadKey();
        }

    }
}
