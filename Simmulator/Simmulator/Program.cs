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
        static int j;
        static string Invers(string code)
        {
            string Text = "";
            int k = code.Length;
            for (int i = 0; i < k; i++)
            {
                if (code[i] == 1)
                {
                    Text += "0";
                }
                else if (code[i] == 0)
                {
                    Text += "1";
                }
            }
            return Text;
        }

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

        static int BinarytoDecimal(string binary)
        {
            int dec = 0;
            dec = Convert.ToInt16(binary, 2);
            return dec;
        }
        static string GenCode(int mem)
        {
            string code = Convert.ToString(mem);
            string front = "";
            int lenght;
            lenght = DecitoBi(code).Length;
            if (lenght < 32)
            {
                front = "";// empty string
                for (int i = 0; i < 32 - lenght; i++)
                {
                    front += "0";
                }
                front += DecitoBi(code);
            }
            else
            {
                front = DecitoBi(code);
            }
            return front;

        }

        static void Add(string code)
        {
            pc++;
            string regA = code.Substring(10, 3);
            string regB = code.Substring(13, 3);
            string destReg = code.Substring(29,3);
            int rA = BinarytoDecimal(regA);
            int rB = BinarytoDecimal(regB);
            int dReg = BinarytoDecimal(destReg);
            reg[dReg] = reg[rA] + reg[rB];
            Console.Write(rA + " " + rB + " " + dReg);



        }

        static void Nand(string code)
        {
            pc++;
            string regA = code.Substring(10, 3);
            string regB = code.Substring(13, 3);
            string destReg = code.Substring(29, 3);
            int rA = BinarytoDecimal(regA);
            int rB = BinarytoDecimal(regB);
            int dReg = BinarytoDecimal(destReg);
            reg[dReg] = ~(reg[rA] & reg[rB]);


        }
        static void LW(string code)
        {
            pc++;
            string regA = code.Substring(10, 3);
            string regB = code.Substring(13, 3);
            string offsetField = code.Substring(17, 15);

            int rA = BinarytoDecimal(regA);
            int rB = BinarytoDecimal(regB);
            int offset = BinarytoDecimal(offsetField);
            reg[rB] = (memory[reg[rA] + offset]);



        }

        static void Beq(string code)
        {
            pc++;
            string regA = code.Substring(10, 3);
            string regB = code.Substring(13, 3);
            string offsetField = code.Substring(16, 16);
            int rA = BinarytoDecimal(regA);
            int rB = BinarytoDecimal(regB);
            int offset = Convert.ToInt16(offsetField,2);
            Console.Write(rA + " " + rB + " " + offset + "\n");

            if (reg[rA]==reg[rB])
            {
                pc = pc + offset;
            }

        }

        static void SW(string code)
        {
            pc++;
            string regA = code.Substring(10, 3);
            string regB = code.Substring(13, 3);
            string offsetField = code.Substring(16, 16);
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
            string regA = code.Substring(10, 3);
            string regB = code.Substring(13, 3);
            int rA = BinarytoDecimal(regA);
            int rB = BinarytoDecimal(regB);
            reg[rB] = pc + 1;
            pc = reg[rA];

        }
        static void Noop(string code)
        {

        }
        static string DecitoBi(string mcode)

        {
            string bi;
            bi = Convert.ToString(Convert.ToInt32(mcode), 2);
            return bi;
        }

        static void PrintState(int pc)
        {
            Console.WriteLine("@@@");
            Console.WriteLine("state:");
            Console.WriteLine("\t" + "PC " + pc);
            Console.WriteLine("\t" + "memory:");
            for (int i = 0; i < j; i++)
            {
                Console.WriteLine("\t" + "\t" + "mem[" + i + "]" + memory[i]);
            }
            Console.WriteLine("\t" + "register:");
            for (int i = 0; i < 8; i++)
            {
                Console.WriteLine("\t" + "\t" + "reg[" + i + "]" + reg[i]);
            }
            Console.WriteLine("end state");

        }
        static void Main(string[] args)
        {
            pc = 0;
            memory = new int[1000];
            reg = new int[8];
            //set reg = 0
            for (int k = 0; k < 8; k++)
            {
                reg[k] = 0;
            }

            string filelocation = @"C:\Users\Subin\Documents\GitHub\ComArc\Assember\Assember\bin\Debug\MACCODE.txt";   
            string[] lines = System.IO.File.ReadAllLines(filelocation);

            j = 0;
            string code;

            foreach (string s in lines)
            {
                memory[j] = Convert.ToInt32(s);
                code = GenCode(memory[j]);
                Console.WriteLine("memmory[" + j + "]=" + memory[j]);
                Console.WriteLine(code);

                j++;

            }


            bool i = true;
            int inst = 0;
            while (i)
            {
                PrintState(pc);
                code = GenCode(memory[pc]);
                string opcode = code.Substring(7, 3);
                if (opcode == "110")
                {
                    i = false;
                }
                switch (opcode)
                {
                    case "000":
                        Add(code);
                        Console.Write("add");
                        break;
                    case "001":
                        Nand(code);
                        Console.Write("nand");
                        break;
                    case "010":
                        LW(code);
                        break;
                    case "011":
                        SW(code);
                        break;
                    case "100":
                        Beq(code);
                        break;
                    case "101":
                        Jalr(code);
                        break;
                    case "110":
                        Halt(code);
                        break;
                    case "111":
                        Noop(code);
                        break;

                }
                
                inst++;
                
            }
            Console.WriteLine("machine halted");
            Console.WriteLine("total of " + inst + " Instruction executed");
            Console.WriteLine("final state of machine:");
            pc++;
            PrintState(pc);
            Console.ReadKey();






        }
    }

}
