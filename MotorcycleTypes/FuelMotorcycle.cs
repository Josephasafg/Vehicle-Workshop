using System;

namespace Ex03.GarageLogic
{
    public class FuelMotorcycle : Motorcycle
    {
        public FuelMotorcycle(string i_VehicleModelName, string i_VehicleLicenceNumber, string i_WheelManufacturerName)
            : base(i_VehicleModelName, i_VehicleLicenceNumber, i_WheelManufacturerName)
        {
            Propulsion = new FuelTank(FuelType.eFuelType.Octan95, 8);
        }

        public override string ToString()
        {
            return string.Format("Fuel Based Motorcycle:{0}{1}Fuel Tank Information:   {2}{3}", Environment.NewLine, base.ToString(), Propulsion.ToString(), Environment.NewLine);
        }
    }
}
