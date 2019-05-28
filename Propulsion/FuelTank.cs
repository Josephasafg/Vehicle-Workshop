using System;

namespace Ex03.GarageLogic
{
    public class FuelTank : Propulsion
    {
        private FuelType.eFuelType m_FuelType;

        public FuelTank(FuelType.eFuelType i_FuelType, int i_MaxTankCapacity)
            : base(i_MaxTankCapacity)
        {
            m_FuelType = i_FuelType;
        }

        public FuelType.eFuelType FuelType
        {
            get
            {
                return m_FuelType;
            }
        }

        public void PumpFuelToTank(float i_AmountOfFuelToAdd,  FuelType.eFuelType i_PumpFuelType)
        {
            if (i_PumpFuelType == m_FuelType)
            {
                AddMoreEnergy(i_AmountOfFuelToAdd);
            }      
        }

        public override string ToString()
        {
            return string.Format("{0}Fuel Type: {1}   There are {2} liters of fuel available, out of a {3} liter tank capacity", Environment.NewLine, FuelType.ToString(), CurrentPropulsionAmount.ToString(), MaxPropulsion.ToString());
        }
    }
}
