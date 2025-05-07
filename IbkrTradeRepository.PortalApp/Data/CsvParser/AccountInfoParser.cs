using CsvHelper;
using CsvHelper.Configuration;
using IbkrTradeRepository.PortalApp.Domain;
using IbkrTradeRepository.PortalApp.Infrastructure.Persistance.Repositories;
using System.Globalization;

namespace IbkrTradeRepository.PortalApp.Data.CsvParser
{
    public class AccountInfoParser : ICsvParserAndSaveStrategy
    {
        private readonly IAccountRepository _accountRepository;

        public AccountInfoParser(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task ParseAndSaveAsync(Stream csvStream, string fileName)
        {
            var accounts = await ParseAsync(csvStream);

            if (accounts.Any()) 
            {
                await ValidateAndSaveAccountsAsync(accounts);
            }
        }

        private async Task<IEnumerable<Account>> ParseAsync(Stream csvStream)
        {
            using var reader = new StreamReader(csvStream);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<AccountMap>();
            var records = new List<Account>();

            await foreach (var record in csv.GetRecordsAsync<Account>())
            {
                records.Add(record);
            }

            return records;
        }

        private async Task ValidateAndSaveAccountsAsync(IEnumerable<Account> accounts)
        {
            foreach (var account in accounts)
            {
                var existingAccount = await _accountRepository.GetAccountByNumber(account.AccountNumber);
                if (existingAccount == null)
                {
                    account.Id = Guid.NewGuid();
                    await _accountRepository.AddAccountAsync(account);
                }
            }
        }

        private sealed class AccountMap : ClassMap<Account>
        {
            public AccountMap()
            {
                Map(m => m.BrokerName).Name("BrokerName");
                Map(m => m.AccountNumber).Name("AccountNumber");
                Map(m => m.BaseCurrency).Name("BaseCurrency");
            }
        }
    }
}
