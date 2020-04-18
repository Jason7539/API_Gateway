using System;
using System.Collections.Generic;

namespace TestController.sakila
{
    public partial class Upload
    {
        public int UploadId { get; set; }
        public int StoreId { get; set; }
        public string IngredientName { get; set; }
        public string Uploader { get; set; }
        public DateTime PostTimeDate { get; set; }
        public string Description { get; set; }
        public string Rating { get; set; }
        public string Photo { get; set; }
        public double Price { get; set; }
        public int Upvote { get; set; }
        public int Downvote { get; set; }
        public bool InProgress { get; set; }

        public virtual Store Store { get; set; }
        public virtual User UploaderNavigation { get; set; }
    }
}
