using RACRMS.DataTransferObject;
using RACRMS.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RACRMS.Extension
{
    public static class PreferenceExtensions
    {
        public static PreferenceDTO ToDTO(this Preference entity)
        {
            try
            {
                return new PreferenceDTO()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    CreateDate = entity.CreateDate,
                    UpdateDate = entity.UpdateDate
                };
            }
            catch
            {
                throw;
            }
        }
    }
}
