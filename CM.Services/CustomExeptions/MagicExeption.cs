using System;
using System.Collections.Generic;
using System.Text;

namespace CM.Services.CustomExeptions
{
    public class MagicExeption : Exception
    {
        public MagicExeption()
        {
        }

        public MagicExeption(string message)
            : base(message)
        {
        }

    }
}
