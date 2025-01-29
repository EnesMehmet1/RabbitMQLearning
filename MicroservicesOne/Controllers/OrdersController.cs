using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shread.Event;

namespace MicroservicesOne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(IPublishEndpoint publishEndpoint,ISendEndpointProvider sendEndpointProvider) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create()
        {
            Enumerable.Range(1, 100).ToList().ForEach(x =>
            {
                var orderCreated = new OrderCreatedEvent() { Count = 2, OrderCode = x.ToString(), Price = 300 };
                publishEndpoint.Publish(orderCreated);
            });
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update()
        {
            var sendEndpoint = await sendEndpointProvider.GetSendEndpoint(new Uri("queue:microservice-two.order.updated.event.queue"));
            Enumerable.Range(1, 100).ToList().ForEach(async x =>
            {
                var orderUpdatedEvent = new OrderUpdatedEvent() { Count = 2, OrderCode = x.ToString(), Price = 300 };
                await publishEndpoint.Publish(orderUpdatedEvent);
            });
            return Ok();
        }
    }
}