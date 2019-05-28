using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class FuelCar : Car
    {
        public FuelCar(string i_VehicleModelName, string i_VehicleLicenceNumber, string i_WheelManufacturerName)
            : base(i_VehicleModelName, i_VehicleLicenceNumber, i_WheelManufacturerName)
        {
            Propulsion = new FuelTank(FuelType.eFuelType.Octan96, 55);  
        }

        public override string ToString()
        {
            return string.Format("Fuel Based Car:{0}{1}Fuel Tank Information:   {2}{3}", Environment.NewLine, base.ToString(), Propulsion.ToString(), Environment.NewLine);
        }
    }
}
