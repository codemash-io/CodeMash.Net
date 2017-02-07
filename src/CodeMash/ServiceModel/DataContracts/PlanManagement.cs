using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace CodeMash.ServiceModel
{
    public class PlanManagement
    {
        public PlanManagement()
        {
            Orders = new List<Order>();
            MembershipPlan = MembershipPlan.Free;
        }
        [BsonElement("planName")]
        public string PlanName { get; set; }

        [BsonElement("membershipPlan")]
        public MembershipPlan MembershipPlan { get; set; }

        [BsonElement("validTill")]
        public DateTime ValidTill { get; set; }

        [BsonElement("orders")]
        public List<Order> Orders { get; set; }
        [BsonElement("customerId")]
        public string CustomerId { get; set; }
    }
}