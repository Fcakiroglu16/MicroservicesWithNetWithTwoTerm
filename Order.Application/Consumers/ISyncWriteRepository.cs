using Order.Domain.Read;

namespace Order.Application.Consumers;

public interface ISyncWriteRepository
{
    public Task Create(ProductWithCategory productWithCategory);
}