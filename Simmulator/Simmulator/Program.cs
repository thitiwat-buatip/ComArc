using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmulator
{
    
    class Program
    {
        static int[] reg;
        static int[] memory;
        static int pc;

        static string Reverse(string text)
        {
            char[] cArray = text.ToCharArray();
            string reverse = String.Empty;
            for (int i = cArray.Length - 1; i > -1; i--)
            {
                reverse += cArray[i];
            }
            return reverse;
        }

        static int BinarytoDecimal (string binary)
        {
            int dec=0;
            string bin = Reverse(binary);
            for (int i = 0; i < binary.Length; i++)
            {
                dec += Convert.ToInt32(bin[i])* (2 ^ i);

            }
            return dec;
        }

        static void Add(string code)
        {
            string regA = code.Substring(11, 3);
            string regB = code.Substring(14, 3);
            string destReg = code.Substring(30, 2);
            int rA = BinarytoDecimal(regA);
            int rB = BinarytoDecimal(regB);
            int dReg = BinarytoDecimal(destReg);
            reg[dReg] = reg[rA] + reg[rB];
            
            
        }

        static void Nand(string code)
        {
            string regA = code.Substring(11, 3);
            string regB = code.Substring(14, 3);
            string destReg = code.Substring(30, 2);
            int rA = BinarytoDecimal(regA);
            int rB = BinarytoDecimal(regB);
            int dReg = BinarytoDecimal(destReg);
            reg[dReg] = ~(reg[rA] & reg[rB]);

        }
        static void LW(string code)
        {
            string regA = code.Substring(11, 3);
            string regB = code.Substring(14, 3);
            string offsetField = code.Substring(17, 14);
            int rA = BinarytoDecimal(regA);
            int rB = BinarytoDecimal(regB);
            int offset = BinarytoDecimal(offsetField);
            reg[rB] = (memory[reg[rA] + offset]);


        }

        static void Beq(string code)
        {

            string regA = code.Substring(11, 3);
            string regB = code.Substring(14, 3);
            string offsetField = code.Substring(17, 14);
            int rA = BinarytoDecimal(regA);
            int rB = BinarytoDecimal(regB);
            int offset = BinarytoDecimal(offsetField);
            if (reg[rA] == reg[rB])
            {
                pc = pc + 1 + offset;   
            }
        }

        static void SW(string code)
        {

            string regA = code.Substring(11, 3);
            string regB = code.Substring(14, 3);
            string offsetField = code.Substring(17, 14);
            int rA = BinarytoDecimal(regA);
            int rB = BinarytoDecimal(regB);
            int offset = BinarytoDecimal(offsetField);
            memory[reg[rA] + offset] = reg[rB];
        }

        static void Halt(string code)
        {

        }

        static void Jalr(string code)
        {
            string regA = code.Substring(11, 3);
            string regB = code.Substring(14, 3);
            int rA = BinarytoDecimal(regA);
            int rB = BinarytoDecimal(regB);
            reg[rB] = pc + 1;
            pc = reg[rA];
            
        }
        static string DecitoBi(string mcode)
            
        {
            string bi;
            bi   = Convert.ToString(Convert.ToInt32(mcode), 2);
            return bi;
        }
        static void Main(string[] args)
        {
            int pc = 0;
            int[] memory = new int[1000];
            int[] reg = new int[8];
            string filelocation = @"C:\Users\Subin\Documents\GitHub\ComArc\MCcode.txt";
            string[] lines = System.IO.File.ReadAllLines(filelocation);
            int lenght;
            string front = "";// empty string
            foreach (string s in lines)
            {
               pc++;
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
