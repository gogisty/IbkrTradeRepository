using IbkrTradeRepository.PortalApp.Domain;

namespace IbkrTradeRepository.PortalApp.Infrastructure.Persistance.Repositories
{
    public interface IIbkrCodeRepository
    {
        Task<IbkrCode?> GetIbkrCodeByIdAsync(string code);
        Task<IEnumerable<IbkrCode>> GetAllIbkrCodesAsync();
        Task AddIbkrCodeAsync(IbkrCode ibkrCode);
        Task RemoveIbkrCodeAsync(string code);
    }
}
