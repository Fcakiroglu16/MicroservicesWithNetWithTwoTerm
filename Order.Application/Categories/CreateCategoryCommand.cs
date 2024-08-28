using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Order.Application.Categories
{
    public record CreateCategoryCommand(string Name) : IRequest<int>;
}