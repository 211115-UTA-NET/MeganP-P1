﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Api {
    internal interface ISaveOrder {
        public void SaveOrder(Order order);
    }
}
