using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Website.Models; 

namespace Website.ViewModels
{
    public class StoreIndexViewModel
    {
        public StoreIndexViewModel()
        {
            Photos = new List<Photo>(); 
        }

        public List<Photo> Photos { get; set; }
        public string SearchQuery { get; set; }

    }
}