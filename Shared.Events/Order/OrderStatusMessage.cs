using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Events.Order
{
    public record OrderStatusMessage(string OrderCode, int Status, string FailMessage);
}