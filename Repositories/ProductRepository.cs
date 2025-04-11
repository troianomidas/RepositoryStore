using RepositoryStore.Data;
using RepositoryStore.Models;
using RepositoryStore.Repositories.Abstractions;

namespace RepositoryStore.Repositories;

public class ProductRepository(AppDbContext context) : Repository<Product>(context), IProductRepository;