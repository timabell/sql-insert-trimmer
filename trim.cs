
namespace SqlTrimmer
{
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
			System.Console.Write("trimming " + path + "\n");
		}
	}
}
