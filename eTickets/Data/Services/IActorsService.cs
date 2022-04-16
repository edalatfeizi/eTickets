using eTickets.Models;
using eTickets.ViewModels;

namespace eTickets.Data.Services
{
    public interface IActorsService
    {
        Task<IEnumerable<ActorViewModel>> GetAllAsync();
        Task<ActorViewModel> GetByIdAsync(int id);
        Task AddAsync(ActorViewModel actor);
        Task<ActorViewModel> UpdateAsync(int id, ActorViewModel actor);
        Task<ActorViewModel> DeleteAsync(int id);   

    }
}
