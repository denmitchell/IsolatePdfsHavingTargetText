using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf;
using IsolatePdfsHavingTargetText;

//increment of files to process before providing feedback
int BATCH_SIZE = 100;

//relevant folders for input and output
string SOURCE_FOLDER = AppDomain.CurrentDomain.BaseDirectory;
string DESTINATION_FOLDER = Path.Combine(SOURCE_FOLDER, "output");

//prompts
Console.WriteLine("This program searches the folder where this .exe exists for all PDF files containing search text that you provide.");
Console.WriteLine("Files containing the search text are copied to an \"output\" subfolder");
Console.WriteLine("To start, enter your search strings, separated by commas, and finish by pressing ENTER");


//get input from the user
string input = Console.ReadLine();

PdfSearcher.Execute(input, SOURCE_FOLDER, DESTINATION_FOLDER, BATCH_SIZE);

