using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RutasWeb3.Models
{
    public class View
    {
        public HttpPostedFileBase jsonNodos { get; set; }
        public BestRoute dBestRoute { get; set; }
        public BestRoute fbBestRoute { get; set; }
    }
}