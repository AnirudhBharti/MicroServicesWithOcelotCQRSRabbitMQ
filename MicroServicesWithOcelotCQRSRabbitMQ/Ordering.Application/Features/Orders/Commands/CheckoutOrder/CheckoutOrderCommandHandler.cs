using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
    public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<CheckoutOrderCommandHandler> _logger;
        private readonly IMapper _mapper;
        public CheckoutOrderCommandHandler(IOrderRepository orderRepository, ILogger<CheckoutOrderCommandHandler> logger, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var orderEntiry = _mapper.Map<Order>(request);
            var newOrder = await _orderRepository.AddAsync(orderEntiry);
            _logger.LogInformation($"Order {newOrder.Id} is successfully created.");
            return newOrder.Id;
        }
    }
}
