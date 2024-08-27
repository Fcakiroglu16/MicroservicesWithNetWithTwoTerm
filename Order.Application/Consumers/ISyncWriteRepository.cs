using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Order.Domain.Read;

namespace Order.Application.Consumers
{
    public interface ISyncWriteRepository
    {
        public Task Create(ProductWithCategory productWithCategory);
    }
}