using System;
using System.Collections.Generic;

namespace TestController.sakila
{
    public partial class SaveList
    {
        public string Ingredient { get; set; }
        public int Store { get; set; }
        public string Username { get; set; }

        public virtual User UsernameNavigation { get; set; }
    }
}
