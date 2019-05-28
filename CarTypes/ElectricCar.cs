using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;

namespace Ex03.GarageLogic
{
    public class ElectricCar : Car
    {
        public ElectricCar(string i_VehicleModelName, string i_VehicleLicenceNumber, string i_WheelManufacturerName)
            : base(i_VehicleModelName, i_VehicleLicenceNumber,  i_WheelManufacturerName)
        {
            Propulsion = new Battery(1.8f);
        }

        public override string ToString()
        {
            return string.Format("Electricity Based Car:{0}{1}Battery Information:   {2}{3}", Environment.NewLine, base.ToString(), Propulsion.ToString(), Environment.NewLine);
        }
    }
}
