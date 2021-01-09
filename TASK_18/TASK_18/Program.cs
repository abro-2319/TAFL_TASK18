using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TASK_18
{
    class Program
    {
        /// <summary>
        /// Задан детерминированный конечный автомат в виде матрицы. 
        /// Напишите программу, которая считает его из файла и построит ДКА, 
        /// который распознает язык из всех слов, 
        /// которые распознает исходный автомат и из всех префиксов этих слов. 
        /// </summary>
        /// <param name="path"></param>
        static void my_task_19(string path)
        {
            string sigma = "";
            var final_state = new List<string>();
            var all_states = new List<string>();
            var content = new List<List<string>>();

            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                string line;
                if ((line = sr.ReadLine()) == null)
                {
                    Console.WriteLine("Sorry. File is empty!");
                    return;
                }
                else
                {
                    sigma = line;
                }
                while ((line = sr.ReadLine()) != null)
                {
                    var sub_content = new List<string>();
                    var sub = new List<string>();
                    sub_content = (line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)).ToList<string>();

                    
                        if (sub_content[0].StartsWith("f_"))
                            final_state.Add(sub_content[0]);

                        all_states.Add(sub_content[0]);

                        for (int k = 1; k < sub_content.Count; ++k)
                        { sub.Add(sub_content[k]); }

                        content.Add(sub);

                    
                }
            }
            bool f = true;
            while (f)
            {
                int c = 0;
                foreach (List<string> line in content)
                {
                    foreach (string s in line)
                        if (!s.StartsWith("f_") && !final_state.Contains("f_" + all_states[c]))
                            f = false;
                        else f = true;
                    ++c;
                }

                    for (int i = 0; i < content.Count; ++i)
                    {
                        for (int j = 0; j < content[i].Count; j++)
                        {
                            if (final_state.Contains(content[i][j]) && content[i][j] != all_states[i])
                                final_state.Add("f_" + all_states[i]);
                        }
                    }

            }

            using (FileStream fs = new FileStream($"res_{path}", FileMode.OpenOrCreate)) { }

            using (StreamWriter sw = new StreamWriter($"res_{path}", false, System.Text.Encoding.Default))
            {
                sw.WriteLine(sigma);
                for (int i = 0; i < all_states.Count; i++)
                {
                    if (final_state.Contains("f_"+all_states[i]))
                        sw.Write("f_"+all_states[i] + " ");
                    else
                        sw.Write(all_states[i] + " ");

                    sw.Write(String.Join(" ", content[i])+"\n");
                }
            }


            //return;
        }
        static void Main(string[] args)
        {
            my_task_19("a.txt");
            Console.WriteLine("Task is complete!");

            Console.ReadKey(true);
        }
    }
}
