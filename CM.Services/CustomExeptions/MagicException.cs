using System;
using System.Collections.Generic;
using System.Text;

namespace CM.Services.CustomExceptions
{
    public class MagicException : Exception
    {
        public MagicException()
        {
        }

        public MagicException(string message)
            : base(message)
        {
        }

    }
}
