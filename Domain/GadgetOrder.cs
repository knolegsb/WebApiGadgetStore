using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class GadgetOrder
    {
        public int GadgetOrderId { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int GadgetId { get; set; }
        public Gadget Gadget { get; set; }
    }
}
