using System;
using MongoDB.Bson.Serialization.Attributes;

namespace CodeMash.ServiceModel
{
    public class Order
    {
        [BsonElement("orderId")]
        public string OrderId { get; set; }

        [BsonElement("ip")]
        public string Ip { get; set; }
        [BsonElement("orderTotal")]
        public int OrderTotal { get; set; }
        [BsonElement("orderDate")]
        public DateTime OrderDate { get; set; }
        [BsonElement("cuponCode")]
        public string CuponCode { get; set; }
    }
}