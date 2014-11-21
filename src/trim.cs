
namespace SqlTrimmer
{
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
