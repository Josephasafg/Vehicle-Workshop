using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public struct Battery
    {
        private float m_PowerLeftInMinutes;
        private readonly float m_MaxBatteryInMinutes;

        public Battery(float i_MaxBatteryPower)
        {
            m_MaxBatteryInMinutes = i_MaxBatteryPower;
            m_PowerLeftInMinutes = m_MaxBatteryInMinutes;
        }

        public float CurrentPower
        {
            get
            {
                return m_PowerLeftInMinutes;
            }
            set
            {
                m_PowerLeftInMinutes = value;
            }
        }

        public float MaxPower
        {
            get
            {
                return m_MaxBatteryInMinutes;
            }
        }

        //maybe make two fueling methods with different input paramters? (fuel/power)
        public void ChargeVehicleBattery(float i_AmountOfMinutesToAdd) 
        {
            try
            {
                if (m_MaxBatteryInMinutes >= (i_AmountOfMinutesToAdd + m_PowerLeftInMinutes))
                {
                    m_PowerLeftInMinutes += i_AmountOfMinutesToAdd;
                }
            }
            catch (Exception i_CurrentException)
            {
                float MaxValue = m_MaxBatteryInMinutes - m_PowerLeftInMinutes;
                float MinValue = 0;
                throw new ValueOutOfRangeException(i_CurrentException, MaxValue, MinValue);
            }
        }
    }
}
