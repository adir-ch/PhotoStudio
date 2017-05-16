using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudio.Entities
{
    [DataContract]
    public class PrintOrder
    {
        [DataMember]
        public List<Photo> OrderItems { get; set; }
        
        [DataMember]
        public int OrderId { get; set; }

        [DataMember]
        public string CustomerName { get; set; }
    }
}
