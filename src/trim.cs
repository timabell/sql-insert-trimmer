namespace SqlTrimmer
{
    using System;
    using System.IO;
    using System.Text.RegularExpressions;

    public class Trimmer
    {
        public static int Main(string[] args)
        {
            var e = new Engine9();
            if (args.Length < 1)
            {
                Console.Out.WriteLine("Usage: trim.exe file-to-trim.sql");
                return 1;
            }
            e.Trim(args[0]);
            return 0; // all good
        }
    }

    public class Engine9
    {
        public Engine9()
        {
            BatchSize = 100;
        }

        /// <summary>
        /// How many inserts to combine.
        /// Sql server can only handle 1000 value rows at once,
        /// also have to balance performance http://stackoverflow.com/a/8640583/10245
        /// </summary>
        public int BatchSize { get; set; }

        public void Trim(string path)
        {
            var tempPath = path + ".tmp";
            var first = true;
            var count = 0;
            using (var file = File.OpenText(path))
            {
                using (var output = new StreamWriter(tempPath, false, file.CurrentEncoding))
                {
                    do
                    {
                        var line = file.ReadLine();
                        if (line != null && line.StartsWith("INSERT"))
                        {
                            count++;
                            if (first || count >= BatchSize)
                            {
                                output.WriteLine(line.Replace("VALUES", "VALUES\r\n  "));
                                first = false;
                                count = 0;
                            }
                            else
                            {
                                line = Regex.Replace(line, "INSERT.*VALUES", " ,");
                                output.WriteLine(line);
                            }
                        }
                        else
                        {
                            output.WriteLine(line);
                            first = true;
                            count = 0;
                        }
                    } while (!file.EndOfStream);
                }
            }
            var backupPath = path + ".orig";
            File.Delete(backupPath); // delete old backup
            File.Move(path, backupPath);
            File.Move(tempPath, path);
            Console.Write("done.");
        }
    }
}
