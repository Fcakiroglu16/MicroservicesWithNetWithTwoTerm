using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.Runtime.Internal;
using MediatR;
using Order.Application.Products.Dto;

namespace Order.Application.Products.Queries.GetAll
{
    public record GetAllProductsQuery : IRequest<List<ProductWithCategoryDto>>;
}