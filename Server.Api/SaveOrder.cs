using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Api.Dtos;

namespace Server.Api {
    internal interface ISaveOrder {
        public void SaveOrder(Order order);
    }
}
