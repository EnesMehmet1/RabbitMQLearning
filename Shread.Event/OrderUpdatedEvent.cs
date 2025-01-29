namespace Shread.Event
{
    public record OrderUpdatedEvent //.Net9 ile gelen Record olmasının sebebi bir kez oluşturulduktan sonra değiştirilmemesini istememiz.
    {
        public string OrderCode { get; init; } = default!; //init oluyor ki sadece newlenirken değer değiştirlebilisin.
        public decimal Price { get; init; }
        public int Count { get; init; }
    }
}