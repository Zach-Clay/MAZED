using System;
using System.Collections.Generic;

namespace MazedDB.Models
{
    public partial class product
    {
        public int productId { get; set; }
        public int catalogueId { get; set; }
        public int? orderId { get; set; }
        public int pointValue { get; set; }
        public string? prderQuantity { get; set; }
        /// <summary>
        /// I dont know whether we want a simple bool (yes this item still has stock left) or the specific amount left a product
        /// </summary>
        public string availibility { get; set; } = null!;
        public string? description { get; set; }
        public byte[]? image { get; set; }

        public virtual catalogue Catalogue { get; set; } = null!;
    }
}
