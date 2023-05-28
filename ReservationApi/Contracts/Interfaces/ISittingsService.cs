using ReservationApi.Data;

namespace ReservationApi.Contracts.Interfaces
{
    public interface ISittingsService
    {
        Task<Sitting> GetSitting(int id);
        Task<List<Sitting>> GetSittings();
        Task<Sitting> CreateSitting(Sitting sitting);
        Task<List<Sitting>> GetSittingsRange(DateTime start, DateTime end);
        Task<List<Sitting>> CreateSittingGroup(List<Sitting> sittings);
        Task<Sitting> UpdateSitting(Sitting sitting);
        Task<List<Sitting>> UpdateSittingGroup(List<Sitting> sittings);
        Task DeleteSitting(Sitting sitting);
        Task<List<SittingType>> GetSittingTypes();
        Task<SittingType> GetSittingTypeIdAsync(int id);
    }
}
