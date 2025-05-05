namespace IbkrTradeRepository.PortalApp.Data
{
    public class CsvFileProcessor : ICsvFileProcessor
    {
        public async Task ParseAsync(Stream csvStream, string fileName)
        {
            using var reader = new StreamReader(csvStream, leaveOpen: true);
            var csvData = await reader.ReadToEndAsync();
            //using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            // iterate or map to a POCO
            //await foreach (var record in csv.GetRecordsAsync<dynamic>())
            //{
            //    // TODO: persist or act on record
            //}
        }
    }
}
