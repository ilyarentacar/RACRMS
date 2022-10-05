using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACRMS.MailServiceShared.Statics
{
    public class MailServiceSettings
    {
        public static string MailServiceRequestUrl
        {
            get
            {
#if DEBUG
                return "http://localhost:26001/api/test";
#else
                return "http://mailservice.ilyarentacar.com.tr/api/test";
#endif
            }
        }
    }
}
