using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;

namespace IsolatePdfsHavingTargetText
{
    public class PdfSearcher
    {

        public static void Execute(string? input, string sourceFolder, string destinationFolder, int batchSize)
        {

            //parse the input into an array
            string[] searchTextArray = input?.Split([','], StringSplitOptions.RemoveEmptyEntries);


            //handle null and empty input
            if (searchTextArray == null || searchTextArray.Length == 0)
            {
                Console.WriteLine("No search strings were provided.  Aborting program.");
                return;
            }

            // Ensure the target directory exists
            Directory.CreateDirectory(destinationFolder);


            // Get a list of all files in the source directory
            string[] files = Directory.GetFiles(sourceFolder);

            int fileCount = files.Length;

            List<string> foundFiles = [];


            // loop through all the files inthe source directory
            for (int i = 0; i < fileCount; i++)
            {
                string file = files[i];

                // Check if the file contains any of the search text
                bool textFound = SearchTextInPdf(file, searchTextArray);


                // if the search text is found, copy the file to the subfolder
                if (textFound)
                {
                    // Move the file to the target directory
                    string fileName = Path.GetFileName(file);
                    string destinationPath = Path.Combine(destinationFolder, fileName);
                    File.Copy(file, destinationPath);

                    foundFiles.Add(fileName);
                }

                if (i == fileCount - 1 || (i > 0 && i % batchSize == 0))
                    Console.WriteLine($"Processed {i} files.");
            }


            // let the user know that we are finished searching files
            Console.WriteLine("Finished searching and moving files.");

        }


        public static bool SearchTextInPdf(string pdfFilePath, string[] searchTextArray)
        {
            try
            {
                // Initialize the PDF reader
                using PdfReader pdfReader = new(pdfFilePath);
                using PdfDocument pdfDocument = new(pdfReader);

                // Iterate through pages and search for the text
                int numberOfPages = pdfDocument.GetNumberOfPages();
                for (int i = 1; i <= numberOfPages; i++)
                {
                    string pageText = PdfTextExtractor.GetTextFromPage(pdfDocument.GetPage(i));
                    foreach (string searchText in searchTextArray)
                    {
                        if (pageText.Contains(searchText, StringComparison.OrdinalIgnoreCase)) // Case-insensitive search
                        {
                            return true;
                        }
                    }
                }
            }
            catch { } //iText throws exceptions for things like missing PDF headers.  Ignore these exceptions.
            return false;
        }

    }
}
