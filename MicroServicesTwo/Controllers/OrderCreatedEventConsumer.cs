using MassTransit;
using Shread.Event;

namespace MicroServicesTwo.Controllers
{
    public class OrderCreatedEventConsumer : IConsumer<OrderCreatedEvent>
    {
        public Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            var orderCreatedEvent = context.Message;
            var orderNew = orderCreatedEvent with { Count = 3 }; //Record sınıftan newlerkenkı degerı degıstıremeyız ama boyle baska bir nesne ornegı olusturabılırız

            Console.WriteLine($"{orderCreatedEvent.OrderCode} - {orderCreatedEvent.Price}");
            return Task.CompletedTask;
        }
    }
}
