using RACRMS.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACRMS.ValidationLayer.Abstract
{
    public interface IUserVL
    {
        Task IsThereUsername(string username);
        Task IsThereAnyUser(UserDTO dTO);
    }
}
