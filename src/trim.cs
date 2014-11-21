
namespace SqlTrimmer
{
    /*
    # based on https://gist.github.com/timabell/94b55a12db4c6ee42e10
    # takes generated SQL for INSERTing records on mass and strips repetition of column names
    # modern sql server accepts multiple rows separated by commas in one insert block

    # before
    # INSERT (x,y,z) VALUES (1,2,3)
    # INSERT (x,y,z) VALUES (1,2,3)
    # INSERT (x,y,z) VALUES (1,2,3)

    # after
    # INSERT (x,y,z) VALUES
    #   (1,2,3)
    # , (1,2,3)
    # , (1,2,3)
     */
    using System;
    using System.IO;
    public class Trimmer
    {
        public static int Main()
        {
            var e = new Engine9();
            e.Trim("test.sql");
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
            Console.Write("trimming " + path + "\n");
            using (var file = File.Open(path, FileMode.Open))
            {
            }
            Console.Write("done.");
        }
    }
}
