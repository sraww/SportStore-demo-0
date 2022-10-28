using System;
using System.Collections.Generic;

namespace SportStore.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderProducts = new HashSet<OrderProduct>();
        }

        public int Id { get; set; }
        public string OrderInfo { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int PickupPointId { get; set; }
        public int UserId { get; set; }
        public string Code { get; set; } = null!;
        public string Status { get; set; } = null!;

        public virtual PickupPoint PickupPoint { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
