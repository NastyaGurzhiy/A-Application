using ProtoBuf;
namespace A_Application
{
    [ProtoContract]
    public class CurrencyPair 
    {
        [ProtoMember(1)]
        public string Currency { get; set; }

        [ProtoMember(2)]
        public decimal Price { get; set; }

        [ProtoMember(3)]
        public DateTime TimeStamp { get; set; }
    }
}
