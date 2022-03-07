using System.Collections.Generic;

namespace Ecomerce_test1.Models.viewModel
{
    public class AdminPage
    {
        public AdminPage() {

            this.prCategories = new Dictionary<string, int>();
        }
        public int productNumber { get; set; }
        public int categoryNumber { get; set; }
        public int UserNumber { get; set; }

        public int resellerNumber { get; set; }
        public int AdminNumber { get; set; }

        public Dictionary<string, int> prCategories { get; set; }


  
    }
}
