using eTickets.Models;
using eTickets.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Services
{
    public class ActorsService : IActorsService
    {
        private readonly AppDbContext _context;
        public ActorsService(AppDbContext context)
        {
            _context = context;
            Console.WriteLine("ActorsService is injected!");
        }
        public async Task AddAsync(ActorViewModel actor)
        {
            var _actor = new Actor
            {
                FullName = actor.FullName,
                ProfilePictureURL = actor.ProfilePictureURL,
                Bio = actor.Bio,
            };
            await _context.Actors.AddAsync(_actor);
            await _context.SaveChangesAsync();
        }

        public async Task<ActorViewModel> DeleteAsync(int id)
        {
            var _actor = _context.Actors.Find(id);
            var actor = new ActorViewModel()
            {
                Id = _actor.Id,
                FullName = _actor.FullName,
                Bio = _actor.Bio,
                ProfilePictureURL = _actor.ProfilePictureURL,
            };

            _context.Remove(_actor);
            await _context.SaveChangesAsync();

            return actor;
        }

        public async Task<IEnumerable<ActorViewModel>> GetAllAsync()
        {
            var result = await _context.Actors.ToListAsync();
            var actors = new List<ActorViewModel>();
            foreach (var item in result)
            {
                actors.Add(new ActorViewModel()
                {
                    Id = item.Id,
                    FullName = item.FullName,
                    ProfilePictureURL = item.ProfilePictureURL,
                    Bio = item.Bio
                });
            }
            return actors;
        }

        public async Task<ActorViewModel?> GetByIdAsync(int id)
        {
            var result = await _context.Actors.FirstOrDefaultAsync(a => a.Id == id);
            var _actor = new ActorViewModel();
            if (result == null)
                return null;
            else
            {
                _actor.Id = result.Id;
                _actor.FullName = result.FullName;
                _actor.Bio = result.Bio;
                _actor.ProfilePictureURL = result.ProfilePictureURL;
            }

            return _actor;
        }

        public async Task<ActorViewModel> UpdateAsync(int id, ActorViewModel actor)
        {
            var _actor = new Actor()
            {
                Id = actor.Id,
                FullName = actor.FullName,
                ProfilePictureURL = actor.ProfilePictureURL,
                Bio = actor.Bio
            };

            var result = _context.Update(_actor);
            await _context.SaveChangesAsync();

            return actor;

        }
    }
}
