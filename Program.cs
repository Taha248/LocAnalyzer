using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
/**
* @author Taha Tauquir  
* @version 1.0.1
**/
namespace LocMatricsCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            String fileDirectory = "test.java";
            CalculateLOC(fileDirectory);
        }

        private static void CalculateLOC(string Path)
        {
            int LOC = 0, CLOC = 0, SLOC = 0, BLOC = 0, temp = 0;

            try { 
            string[] lines = System.IO.File.ReadAllLines(@"" + Path);

            using (var reader = new StreamReader(Path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrEmpty(line))
                        BLOC++;
                    if (!string.IsNullOrEmpty(line) && line.Contains("{") ||
                        !string.IsNullOrEmpty(line) && line.Contains("}") ||
                        !string.IsNullOrEmpty(line) && line.Contains("import"))
                    {
                        var x = Regex.Split(line, "({|}|import)");
                        foreach (var z in x)
                        {
                            if (!string.IsNullOrEmpty(z))
                                temp++;
                        }
                    }

                    if (line.Contains("/*") && !line.Contains("*/"))
                    {
                        var x = "";
                        CLOC++;
                        while (!x.Contains("*/"))
                        {
                            x = reader.ReadLine();
                            CLOC++;
                            LOC++;
                        }
                    }
                    else if (line.Contains("/*") && line.Contains("*/"))
                    {
                        CLOC++;
                    }
                    if (line.Contains("//") && !line.Contains("*/") && !line.Contains("/*"))
                    {
                        CLOC++;
                    }
                    LOC++;
                    var l = line.Split(new string[] { "//" }, StringSplitOptions.None);
                    var l1 = line.Split(new string[] { "/*" }, StringSplitOptions.None);

                    if (!string.IsNullOrEmpty(l[0]) && !string.IsNullOrEmpty(l1[0]))
                        SLOC++;


                }



            }

                Console.WriteLine("CLOC=" + CLOC);
                Console.WriteLine("LOC=" + LOC);
                Console.WriteLine("SLOC=" + SLOC);
                Console.WriteLine("BLOC=" + BLOC);
                Console.WriteLine("NCBLOC=" + (SLOC - temp));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.Read();
        }
    }
}
