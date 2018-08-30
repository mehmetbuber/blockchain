using System;
using System.Collections.Generic;
using System.Text;
using Blockchain.Models;

namespace Blockchain.Helpers
{
    public static class ChainHelper
    {
        public static Block<object> GenesisBlock => new Block<object>(DateTime.Now, new object(), "");
    }
}
