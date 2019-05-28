using System;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private const int k_MaxAirPressure = 26;
        private const int k_AmountOfWheels = 12;
        private float m_VolumeOfCargo;
        private bool m_isHazardousMaterialTruck;

        public Truck(string i_VehicleModelName, string i_VehicleLicenceNumber, string i_WheelManufacturerName)
            : base(i_VehicleModelName, i_VehicleLicenceNumber, k_AmountOfWheels, i_WheelManufacturerName, k_MaxAirPressure)
        {
            Propulsion = new FuelTank(FuelType.eFuelType.Soler, 110);
        }

        public bool IsHazardous
        {
            get
            {
                return m_isHazardousMaterialTruck;
            }

            set
            {
                m_isHazardousMaterialTruck = value;
            }
        }

        public float CargoVolume
        {
            get
            {
                return m_VolumeOfCargo;
            }

            set
            {
                m_VolumeOfCargo = value;
            }
        }

        public void UpdateNewTruckInput(string i_isHazardousMaterialTruck, float i_VolumeOfCargo)
        {
            m_VolumeOfCargo = i_VolumeOfCargo;
            switch (i_isHazardousMaterialTruck)
            {
                case "y":
                    m_isHazardousMaterialTruck = true;
                    break;
                case "n":
                    m_isHazardousMaterialTruck = false;
                    break;
            }
        }

        public override string ToString()
        {
            string hazardousParseString = "Is Hazardous !!!";
            if (IsHazardous == false)
            {
                hazardousParseString = "is not hazardous ";
            }

            return string.Format("{0}Volume Of Cargo: {1} ccm   risk factor: the trucks cargo {2} {3}Fuel Tank Information:   {4}{5}", base.ToString(), m_VolumeOfCargo.ToString(), hazardousParseString, Environment.NewLine, Propulsion.ToString(), Environment.NewLine);
        }
    }
}
