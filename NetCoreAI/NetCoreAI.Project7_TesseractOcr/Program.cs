using Tesseract;

class Program
{


	static void Main(string[] args)
	{
		Console.WriteLine("Resim Adresini Girin: ");
		string imagepath = Console.ReadLine();
		Console.WriteLine("");

		string tessDataPath = @"C:/tessdata";

		try
		{
			using (var engine = new TesseractEngine(tessDataPath, "eng", EngineMode.Default))
			{
				using (var img = Pix.LoadFromFile(imagepath))
				{
					using (var page = engine.Process(img))
					{
						string text = page.GetText();
						Console.WriteLine("Resimdeki metin: ");
						Console.WriteLine("");
						Console.WriteLine(text);
					}
				}
			}
		}
		catch (Exception er)
		{
			Console.WriteLine("Hata: " + er.Message);
		}

	}
}