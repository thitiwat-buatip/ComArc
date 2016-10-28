using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assember
{
    class Label
    {
        public string label;
        public string value;
        public Label(string l,string v)
        {
            label = l;
            value = v;

        }
    }


    class Program
    {
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
                return "KuySubin";
        }
        static string ConvertR(string code)
        {
            string re = "0000000";
            string m;
            int l;
            string[] word = code.Split('\t');
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
                re += "0000000000000000000000";

            }
            else if (word[1] == "noop")
            {
                re += "111";
                re += "0000000000000000000000";

            }
            else if (word[1] == "beq")
            {
                re += "100";
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
            else if (word[1] == ".fill")
            {
                re = Convert.ToString(Convert.ToInt32(word[2]), 2);
            }
            re = Convert.ToString(Convert.ToInt32(re, 2));
            return re;
        }
        static void Main(string[] args)
        {
            string filelocation = @"C:\Users\Subin\Documents\GitHub\ComArc\Acode.txt";
            string[] lines = System.IO.File.ReadAllLines(filelocation);
            
            List<Label> labelei = new List<Label>();
            List<Label> fill = new List<Label>();

            foreach (string s in lines)
                Console.WriteLine(s);

            for (int i = 0; i < lines.Length; i++)
            {
                string[] word = lines[i].Split('\t');
                                
                if (word[0] != "")
                {
                    if (word[1] == ".fill")
                    {
                        fill.Add(new Label(word[0], word[2]));
                    }
                    else
                    labelei.Add(new Label(word[0], Convert.ToString(i)));
                }
                
            }

            
            for (int i = 0; i < lines.Length; i++)
            {
                foreach(Label k in fill)
                {
                    lines[i] = lines[i].Replace(k.label, k.value);
                }
            }
            for (int i = 0; i < lines.Length; i++)
            {
                
                foreach (Label k in labelei)
                {
                    string[] word = lines[i].Split('\t');
                    if (word[1] == "beq" && word[4].Length > 2)
                    {                        
                        word[4] = Convert.ToString(Convert.ToInt32(k.value) - (i + 1));
                        lines[i] = word[0] + "\t" + word[1] + "\t" + word[2] + "\t" + word[3] + "\t" + word[4];
                    }
                                      
                        lines[i] = lines[i].Replace(k.label, k.value);
                    
                }
            }

            foreach (Label l in labelei)
            {
                Console.WriteLine(l.label+"  "+l.value);
            }
            Console.WriteLine("\n");
            foreach (Label l in fill)
            {
                Console.WriteLine(l.label + "  " + l.value);
            }
            
            foreach (string s in lines)
            {
                
                Console.WriteLine(ConvertR(s));
            }
            Console.ReadKey();





        }

        
    }   

}
