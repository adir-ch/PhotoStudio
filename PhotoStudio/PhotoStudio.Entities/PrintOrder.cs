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
        public PrintOrder()
        {
            OrderItems = new List<Photo>(); 
        }

        [DataMember]
        public virtual ICollection<Photo> OrderItems { get; set; }
        
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string CustomerName { get; set; }
    }
}
