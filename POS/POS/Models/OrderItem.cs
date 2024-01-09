using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Models
{
    public class OrderItem
    {
        public int OrderId { get; set; }
        public int ItemId { get; set; }

        public int Amount { get; set; }

        public virtual Order Order { get; set; }
        public virtual Item Item { get; set; }

    }

}
