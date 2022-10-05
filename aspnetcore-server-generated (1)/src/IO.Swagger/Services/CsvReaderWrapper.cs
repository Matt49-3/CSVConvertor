using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using CsvHelper;

namespace IO.Swagger.Services;

public interface ICsvReaderWrapper
{
    IAsyncEnumerable<T> GetRecordsAsync<T> (StreamReader streamReader);
}

public class CsvReaderWrapper:ICsvReaderWrapper
{
    public async IAsyncEnumerable<T> GetRecordsAsync<T>(StreamReader streamReader)
    {
        using (CsvReader csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
        {
            //return csvReader.GetRecordsAsync<dynamic>();
            await foreach (var item in csvReader.GetRecordsAsync<dynamic>())
            {
                yield return item;
            }
        }
    }
}