using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;

namespace IO.Swagger.Services;

public interface ICsvReaderWrapper
{
    IAsyncEnumerable<IDictionary<string, Object>> GetRecordsAsync (StreamReader streamReader);
}

public class CsvReaderWrapper:ICsvReaderWrapper
{
    public const string Separator = ";";
    
    public async IAsyncEnumerable<IDictionary<string, Object>> GetRecordsAsync(StreamReader streamReader)
    {
        var readerConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture);
        readerConfiguration.Delimiter = Separator;
        using (CsvReader csvReader = new CsvReader(streamReader, readerConfiguration))
        {
            //return csvReader.GetRecordsAsync<dynamic>();
            await foreach (var item in csvReader.GetRecordsAsync<dynamic>())
            {
                yield return item as IDictionary<string,object>;
            }
            
        }
    }
}