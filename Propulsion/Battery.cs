using System;

namespace Ex03.GarageLogic
{
    public class Battery : Propulsion
    {
        public Battery(float i_MaxBatteryPower) : base(i_MaxBatteryPower)
        {
        }

        public void ChargeBattery(float i_AmountOfMinutesToAdd)
        {
            AddMoreEnergy(i_AmountOfMinutesToAdd);
        }

        public override string ToString()
        {
            return string.Format("{0}There are {1} hours of Battery power remaining {2}", Environment.NewLine, CurrentPropulsionAmount.ToString(), Environment.NewLine);
        }
    }
}
