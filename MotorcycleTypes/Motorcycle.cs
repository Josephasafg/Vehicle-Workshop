using System;

namespace Ex03.GarageLogic
{
    public abstract class Motorcycle : Vehicle
    {
        private const int k_MaxAirPressure = 33;
        private const int k_NumberOfWheels = 2;
        private eLicenceType m_LicenceType;
        private int m_EngineDisplacement;

        public Motorcycle(string i_VehicleModelName, string i_VehicleLicenceNumber, string i_WheelManufacturerName)
            : base(i_VehicleModelName, i_VehicleLicenceNumber, k_NumberOfWheels, i_WheelManufacturerName, k_MaxAirPressure)
        {
        }

        public eLicenceType Licence
        {
            get
            {
                return m_LicenceType;
            }

            set
            {
                m_LicenceType = value;
            }
        }

        public int EngineDisplacement
        {
            get
            {
                return m_EngineDisplacement;
            }

            set
            {
                m_EngineDisplacement = value;
            }
        }

        public enum eLicenceType
        {
            A,
            A1,
            A2,
            B
        }

        public void UpdateNewMotorcycleInput(int i_LicenceType, int i_EngineDisplacement)
        {
            m_EngineDisplacement = i_EngineDisplacement;
            switch (i_LicenceType)
            {
                case 1:
                    m_LicenceType = eLicenceType.A;
                    break;
                case 2:
                    m_LicenceType = eLicenceType.A1;
                    break;
                case 3:
                    m_LicenceType = eLicenceType.A2;
                    break;
                case 4:
                    m_LicenceType = eLicenceType.B;
                    break;
            }
        }

        public override string ToString()
        {
            return string.Format("{0}Licence Type: {1}    Engine Displacement: {2} ccm{3}", base.ToString(), m_LicenceType.ToString(), m_EngineDisplacement.ToString(), Environment.NewLine);
        }
    }
}
