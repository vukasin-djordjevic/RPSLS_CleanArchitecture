using Domain.GameResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    internal sealed class GameResultRepository:IGameResultRepository
    {
        private readonly ApplicationDbContext _context;

        public GameResultRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(GameResult result, CancellationToken cancellationToken = default)
        {
            await _context.GameResults.AddAsync(result, cancellationToken);
        }

        public async Task<List<GameResult>> GetScoreboardAsync(int count, CancellationToken cancellationToken = default)
        {
            return await _context.GameResults.AsNoTracking().OrderByDescending(r => r.Created).Take(count)
                .ToListAsync(cancellationToken);
        }
        
        public async Task DeleteAllAsync(CancellationToken cancellationToken = default)
        {
            await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE game_results");
        }
    }
}
