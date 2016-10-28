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
        static void Main(string[] args)
        {
            string filelocation = @"C:\Users\Thitiwat\Documents\Project ComArc\Acode.txt";
            List<Label> labelei = new List<Label>();
            List<Label> fill = new List<Label>();
            string[] lines = System.IO.File.ReadAllLines(filelocation);

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
                Console.WriteLine(s);
            Console.ReadKey();



        }

        
    }   

}
