using Domain.GameResults;
//using Domain.Orders;
//using Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Application.Data;

public interface IApplicationDbContext
{
    DbSet<GameResult> GameResults { get; set; }

    //DbSet<Order> Orders { get; set; }

    //DbSet<OrderSummary> OrderSummaries { get; set; }

    //DbSet<Product> Products { get; set; }

    DatabaseFacade Database { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}