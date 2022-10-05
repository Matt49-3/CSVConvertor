using IO.Swagger.Services;
using AutoFixture;
using Moq;

namespace TestProject1;


public class TestCsvConvertor
{
    [Fact]
    public void IfFileNotFoundReturnException()
    {
        
        var mockHttpHandler = new Mock<IHttpHandler>();
        var mockCsvReaderWrapper = new Mock<ICsvReaderWrapper>();
        var csvuri = new Fixture().Create<string>();

        mockHttpHandler.Setup(m => m.GetAsync(It.IsAny<string>())).Throws<FileNotFoundException>();
        
        var csvConvert = new CsvConvertor(mockHttpHandler.Object, mockCsvReaderWrapper.Object);
   
        Assert.Throws<FileNotFoundException>(() =>csvConvert.Convert(csvuri));
    }

    public void ReturnIAsyncEnumerableWith()
    {
        var mockHttpHandler = new Mock<IHttpHandler>();
        var mockCsvReaderWrapper = new Mock<ICsvReaderWrapper>();
        var mockHttpResponseMessage = new Mock<HttpResponseMessage>();
        var csvuri = new Fixture().Create<string>();
        
        var csvConvert = new CsvConvertor(mockHttpHandler.Object, mockCsvReaderWrapper.Object);

        mockHttpHandler.Setup(m => m.GetAsync(It.IsAny<string>())).ReturnsAsync(mockHttpResponseMessage.Object);
    }
    
    
}