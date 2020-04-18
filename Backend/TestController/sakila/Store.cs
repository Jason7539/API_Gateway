using System;
using System.Collections.Generic;

namespace TestController.sakila
{
    public partial class Store
    {
        public Store()
        {
            Upload = new HashSet<Upload>();
        }

        public int StoreId { get; set; }
        public string Owner { get; set; }
        public string StoreName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string StoreDescription { get; set; }
        public string PlaceId { get; set; }

        public virtual User OwnerNavigation { get; set; }
        public virtual ICollection<Upload> Upload { get; set; }
    }
}
