using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemblyconv
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(ConvertR(Console.ReadLine()));
            Console.ReadKey();
                
        }
        static string R(string re)
        {
            if (re == "0")
            {
                return "000";
            }
            else if (re == "1")
            {
                return "001";
            }
            else if (re == "2")
            {
                return "010";
            }
            else if (re == "3")
            {
                return "011";
            }
            else if (re == "4")
            {
                return "100";
            }
            else if (re == "5")
            {
                return "101";
            }
            else if (re == "6")
            {
                return "110";
            }
            else if (re == "7")
            {
                return "111";
            }
            else
                return "don't Know";
        }
        static string ConvertR(string code)
        {
            string re ="0000000";
            string m;
            int l;
            string[] word = code.Split(' ' );
            if (word[1] == "add")
            {
                re += "000";
                re += R(word[2]);
                re += R(word[3]);
                re += "0000000000000";
                re += R(word[4]);
           
            }
            else if (word[1] == "nand")
            {
                re += "001";
                re += R(word[2]);
                re += R(word[3]);
                re += "0000000000000";
                re += R(word[4]);
               
            }
            else if (word[1] == "jalr")
            {
                re += "101";
                re += R(word[2]);
                re += R(word[3]);
                re += "000000000000000";
               
            }
            else if (word[1] == "halt")
            {
                re += "110";
                re += "000000000000000000000";
                
            }
            else if (word[1] == "noop")
            {
                re += "111";
                re += "000000000000000000000";
                
            }
            else if (word[1] == "beq")
            {
                re += "111";
                re += R(word[2]);
                re += R(word[3]);
                if (Convert.ToInt32(word[4]) < 0)
                {
                    m = Convert.ToString(Convert.ToInt16(word[4]), 2);
                    re += m;                    
                }
                else
                {
                    l = Convert.ToString(Convert.ToInt32(word[4]), 2).Length;
                    for(int i=0; i < 16 - l; i++)
                    {
                        re += "0";
                    }
                    re += Convert.ToString(Convert.ToInt32(word[4]), 2);
                }

                
            }
            else if (word[1] == "lw")
            {
                re += "010";
                re += R(word[2]);
                re += R(word[3]);
                if (Convert.ToInt32(word[4]) < 0)
                {
                    m = Convert.ToString(Convert.ToInt16(word[4]), 2);
                    re += m;
                }
                else
                {
                    l = Convert.ToString(Convert.ToInt32(word[4]), 2).Length;
                    for (int i = 0; i < 16 - l; i++)
                    {
                        re += "0";
                    }
                    re += Convert.ToString(Convert.ToInt32(word[4]), 2);
                }


            }
            else if (word[1] == "sw")
            {
                re += "011";
                re += R(word[2]);
                re += R(word[3]);
                if (Convert.ToInt32(word[4]) < 0)
                {
                    m = Convert.ToString(Convert.ToInt16(word[4]), 2);
                    re += m;
                }
                else
                {
                    l = Convert.ToString(Convert.ToInt32(word[4]), 2).Length;
                    for (int i = 0; i < 16 - l; i++)
                    {
                        re += "0";
                    }
                    re += Convert.ToString(Convert.ToInt16(word[4]), 2);
                }


            }
            return re;
        }
       
    }
}
