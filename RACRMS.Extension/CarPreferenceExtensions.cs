using RACRMS.DataTransferObject;
using RACRMS.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RACRMS.Extension
{
    public static class CarPreferenceExtensions
    {
        public static CarPreferenceDTO ToDTO(this CarPreference entity)
        {
            try
            {
                return new CarPreferenceDTO()
                {
                    Id = entity.Id,
                    CarId = entity.CarId,
                    PlateNumber = entity.Car != null ? entity.Car.PlateNumber : string.Empty,
                    PreferenceId = entity.PreferenceId,
                    PreferenceName = entity.Preference != null ? entity.Preference.Name : string.Empty,
                    Car = entity.Car != null ? entity.Car.ToDTO() : new CarDTO(),
                    Preference = entity.Preference != null ? entity.Preference.ToDTO() : new PreferenceDTO()
                };
            }
            catch
            {
                throw;
            }
        }
    }
}
