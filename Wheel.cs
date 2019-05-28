using Ex03.GarageLogic.Exceptions;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private readonly float r_MaximalWheelAirPressure;
        private string m_WheelManufacturerName;
        private float m_CurrentWheelAirPressure;

        public Wheel(string i_WheelManufacturerName, float i_MaximalWheelAirPressure)
        {
            m_WheelManufacturerName = i_WheelManufacturerName;
            r_MaximalWheelAirPressure = i_MaximalWheelAirPressure;
        }

        public string WheelName
        {
            get
            {
                return m_WheelManufacturerName;
            }

            set
            {
                m_WheelManufacturerName = value;
            }
        }

        public float CurrentAirPressure
        {
            get
            {
                return m_CurrentWheelAirPressure;
            }

            set
            {
                if((value > r_MaximalWheelAirPressure) || (value < 0))
                {
                    throw new ValueOutOfRangeException(r_MaximalWheelAirPressure, 0);
                }

                m_CurrentWheelAirPressure = value;
            }
        }

        public float MaxAirPressure
        {
            get
            {
                return r_MaximalWheelAirPressure;
            }
        }
       
        public void PumpAirIntoWheel(float i_AmountOfAirToAdd)
        {
            if (r_MaximalWheelAirPressure >= (i_AmountOfAirToAdd + m_CurrentWheelAirPressure))
            {
                m_CurrentWheelAirPressure += i_AmountOfAirToAdd;
            }
        }

        public override string ToString()
        {
            return string.Format("Wheel Manufacturer Name: {0}    Max Air Pressure: {1}   Current Air Pressure: {2}", WheelName.ToString(), MaxAirPressure.ToString(), this.CurrentAirPressure.ToString());
        }
    }
}
