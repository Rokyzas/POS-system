using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Models
{
    public class OrderItem
    {
        public int orderId { get; set; }
        public int itemId { get; set; }

        public int amount { get; set; }

        public virtual Order order { get; set; }
        public virtual Item item { get; set; }

    }

}
