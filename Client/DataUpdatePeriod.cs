using System;
using System.Collections.Generic;
using System.Text;

namespace Client
{
    [Serializable]
    public class DataUpdatePeriod
    {
        public int dataUpdate { get; set; }
        public DataUpdatePeriod(int period)
        {
            this.dataUpdate = period;
        }
    }
}
