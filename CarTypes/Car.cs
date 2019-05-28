using System;

namespace Ex03.GarageLogic
{
    public abstract class Car : Vehicle
    {
        private const int k_MaxAirPressure = 31;
        private const int k_AmountOfWheels = 4;
        private eCarDoors m_AmountOfDoors;
        private eCarColor m_CarColor;

        public Car(string i_VehicleModelName, string i_VehicleLicenceNumber, string i_WheelManufacturerName) 
            : base(i_VehicleModelName, i_VehicleLicenceNumber, k_AmountOfWheels, i_WheelManufacturerName, k_MaxAirPressure)
        {
        }

        public eCarColor Color
        {
            get
            {
                return m_CarColor;
            }

            set
            {
                m_CarColor = value;
            }
        }

        public eCarDoors Doors
        {
            get
            {
                return m_AmountOfDoors;
            }

            set
            {
                m_AmountOfDoors = value;
            }
        }

        public enum eCarColor
        {
            Red,
            Blue,
            Grey,
            Black
        }

        public enum eCarDoors
        {
            TWO = 2,
            THREE,
            FOUR,
            FIVE
        }

        public void UpdateNewCarInput(int i_CarColor, int i_AmountOfDoors)
        {
            switch (i_AmountOfDoors)
            {
                case 2:
                    m_AmountOfDoors = eCarDoors.TWO;
                    break;
                case 3:
                    m_AmountOfDoors = eCarDoors.THREE;
                    break;
                case 4:
                    m_AmountOfDoors = eCarDoors.FOUR;
                    break;
                case 5:
                    m_AmountOfDoors = eCarDoors.FIVE;
                    break;
            }

            switch (i_CarColor)
            {
                case 1:
                    m_CarColor = eCarColor.Red;
                    break;
                case 2:
                    m_CarColor = eCarColor.Blue;
                    break;
                case 3:
                    m_CarColor = eCarColor.Grey;
                    break;
                case 4:
                    m_CarColor = eCarColor.Black;
                    break;
            }
        }

        public override string ToString()
        {
            return string.Format("{0}Amount Of Doors: {1}    Car Color: {2}{3}", base.ToString(), m_AmountOfDoors.ToString(), m_CarColor.ToString(), Environment.NewLine);
        }
    }
}
