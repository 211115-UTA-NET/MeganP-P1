using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.UI.Dtos;

namespace Client.UI.Logic {
    internal interface ISaveOrder {
        public void SaveOrder(OrderLogic order);
    }
}
