using MassTransit;
using Shread.Event;

namespace MicroServicesTwo.Controllers
{
    public class OrderUpdatedEventConsumer : IConsumer<OrderUpdatedEvent>
    {
        public Task Consume(ConsumeContext<OrderUpdatedEvent> context)
        {
            var orderUpdatedEvent = context.Message;
            var orderNew = orderUpdatedEvent with { Count = 3 }; //Record sınıftan newlerkenkı degerı degıstıremeyız ama boyle baska bir nesne ornegı olusturabılırız

            Console.WriteLine($"Order Updated : {orderUpdatedEvent.OrderCode} - {orderUpdatedEvent.Price}");
            return Task.CompletedTask;
        }
    }
}
