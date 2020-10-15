using System;
using System.Collections.Generic;
using System.Text;

namespace Winc.Library.DbConfigurations
{
    public class DbSettings
    {
        public string SQLConnection { get; set; }
        public int QueryTimeOutInSec { get; set; }
        public int TransactionTimeOutInSec { get; set; }
    }
}
