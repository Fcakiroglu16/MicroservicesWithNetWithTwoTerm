using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application
{
    public interface ICacheService
    {
        void Add<T>(string key, T value, TimeSpan expirationTime);
        T Get<T>(string key);
    }
}