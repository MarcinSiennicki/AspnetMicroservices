﻿using MediatR;
using System;
using System.Collections.Generic;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersList
{
    public class GetOrdersListQuery :IRequest<List<OrderDto>>
    {
        public GetOrdersListQuery(string userName)
        {
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
        }

        public string UserName { get; set; }

       
    }
}
