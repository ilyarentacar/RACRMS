using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RACRMS.DataTransferObject.Filters
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed public class CustomArrayRequired : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                object[] valueArray = value as object[];

                for (int index = 0; index < valueArray.Length; index++)
                {
                    var valueInArray = valueArray[index];

                    PropertyInfo[] propertyInfos = valueInArray.GetType().GetProperties();

                    for (int innerIndex = 0; innerIndex < propertyInfos.Length; innerIndex++)
                    {
                        PropertyInfo propertyInfo = propertyInfos[innerIndex];

                        if (propertyInfo.Name == "Selected" && (bool)propertyInfo.GetValue(valueInArray))
                            return true;
                    }
                }

                return false;
            }

            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            return ErrorMessage;
        }
    }
}
