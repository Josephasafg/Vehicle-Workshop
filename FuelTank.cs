using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class FuelTank : Energy
    {
        private FuelType.eFuelType m_FuelType;
        //private readonly float m_MaxTankCapacity;
        //private float m_CurrentAmountOfFuel;

        public FuelTank(FuelType.eFuelType i_FuelType, int i_MaxTankCapacity)
            : base(i_MaxTankCapacity)
        {
            m_FuelType = i_FuelType;
        }

        //public FuelTank(FuelType.eFuelType i_FuelType, int i_MaxTankCapacity)
        //{
        //    m_FuelType = i_FuelType;
        //    m_MaxTankCapacity = i_MaxTankCapacity;
        //    m_CurrentAmountOfFuel = m_MaxTankCapacity;
        //}

        //public float MaxTank
        //{
        //    get
        //    {
        //        return m_MaxTankCapacity;
        //    }
        //}

        public FuelType.eFuelType FuelType
        {
            get
            {
                return m_FuelType;
            }
        }

        //public float CurrentFuelAmount
        //{
        //    get
        //    {
        //        return m_CurrentAmountOfFuel;
        //    }
        //    set
        //    {
        //        m_CurrentAmountOfFuel = value;
        //    }
        //}

        //maybe make two fueling methods with different input paramters? (fuel/power)
        public void PumpFuelToTank(float i_AmountOfFuelToAdd, FuelType.eFuelType i_PumpFuelType)
        {
            if (i_PumpFuelType == m_FuelType)
            {
                FillEnergy(i_AmountOfFuelToAdd);
            }
        }
    }
}
