namespace IsolatePdfsHavingTargetText.Tests
{
    public class ProgramTest
    {

        [Theory]
        [InlineData("doesNotHaveText1.pdf", false)]
        [InlineData("doesNotHaveText2.pdf", false)]
        [InlineData("hasText1.pdf", true)]
        [InlineData("hasText2.pdf", true)]
        public void SearchTextInPdf(string inputFile, bool expectedResult)
        {
            var actualResult = PdfSearcher.SearchTextInPdf(inputFile, ["123","ABC"]);
            Assert.Equal(expectedResult, actualResult);
        }
    }
}
