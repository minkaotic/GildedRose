namespace GildedRose.Console
{
	partial class Program
	{
		static void Main(string[] args)
		{
			System.Console.WriteLine("OMGHAI!");

			var app = new GuildedRoseApp();
			app.UpdateQuality();

			System.Console.ReadKey();
		}
	}
}