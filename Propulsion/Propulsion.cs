using Ex03.GarageLogic.Exceptions;

namespace Ex03.GarageLogic
{
    public abstract class Propulsion
    {
        private readonly float r_MaxPropulsionCapacity;
        private float m_CurrentAmountPropulsion;

        public Propulsion(float i_MaxPropulsionCapacity)
        {
            r_MaxPropulsionCapacity = i_MaxPropulsionCapacity;
            m_CurrentAmountPropulsion = r_MaxPropulsionCapacity;
        }

        public void AddMoreEnergy(float i_AmountOfFuelToAdd)
        {
            if (r_MaxPropulsionCapacity >= (i_AmountOfFuelToAdd + m_CurrentAmountPropulsion))
            {
                m_CurrentAmountPropulsion += i_AmountOfFuelToAdd;
            }
            else
            {
                throw new ValueOutOfRangeException(r_MaxPropulsionCapacity - m_CurrentAmountPropulsion, 0);
            }
        }

        public float CurrentPropulsionAmount
        {
            get
            {
                return m_CurrentAmountPropulsion;
            }

            set
            {
                if ((value > r_MaxPropulsionCapacity) || value < 0)
                {
                    throw new ValueOutOfRangeException(r_MaxPropulsionCapacity, 0);
                }

                m_CurrentAmountPropulsion = value;
            }
        }

        public float MaxPropulsion
        {
            get
            {
                return r_MaxPropulsionCapacity;
            }
        }
    }
}
