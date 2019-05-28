using System;

namespace Ex03.GarageLogic
{
    public class ElectricMotorcycle : Motorcycle
    {
        public ElectricMotorcycle(string i_VehicleModelName, string i_VehicleLicenceNumber, string i_WheelManufacturerName)
            : base(i_VehicleModelName, i_VehicleLicenceNumber, i_WheelManufacturerName)
        {
            Propulsion = new Battery(1.4f);
        }

        public override string ToString()
        {
            return string.Format("Electricity Based Motorcycle:{0}{1}Battery Information:   {2}{3}", Environment.NewLine, base.ToString(), Propulsion.ToString(), Environment.NewLine);
        }
    }
}
