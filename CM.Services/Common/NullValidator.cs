using CM.Services.CustomExeptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CM.Services.Common
{
    public static class NullValidator
    {
        public static void ValidateIfNull(this object value, string msg = null)
        {
            if (msg == null)
            {
                msg = "Value cannot be null!";
            }

            if (value == null)
            {
                throw new MagicExeption(msg);
            }
        }

    }
}
