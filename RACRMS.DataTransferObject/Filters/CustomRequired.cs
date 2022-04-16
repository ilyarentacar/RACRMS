using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACRMS.DataTransferObject.Filters
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed public class CustomRequired : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            int intValue = 0;

            if (int.TryParse(value.ToString(), out intValue))
            {
                if (intValue > 0)
                    return true;

                return false;
            }

            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return ErrorMessage;
        }
    }
}
