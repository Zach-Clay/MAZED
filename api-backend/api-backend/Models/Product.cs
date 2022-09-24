using System;
using System.Collections.Generic;

namespace MazedDB.Models
{
    public partial class Product
    {
        public int Id { get; set; }
        public int CatalogueId { get; set; }
        public int? OrderId { get; set; }
        public int PointValue { get; set; }
        public string? OrderQuantity { get; set; }
        /// <summary>
        /// I dont know whether we want a simple bool (yes this item still has stock left) or the specific amount left a product
        /// </summary>
        public string Availibility { get; set; } = null!;
        public string? Description { get; set; }
        public byte[]? Image { get; set; }

        public virtual Catalogue Catalogue { get; set; } = null!;
    }
}
