using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACRMS.ValidationLayer.Abstract
{
    public interface IContractVL
    {
        Task AreContractDatesAvailable(DateTime startDate, DateTime endDate);
    }
}
