using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        protected readonly string r_VehicleModelName;
        protected readonly string r_VehicleLicenceNumber;
        protected readonly float r_MaxAirPressure;
        protected float m_EnergyPercentage;
        private int m_AmoutOfWheels;
        protected List<Wheel> m_VehicleWheelCollection;
        protected VehicleRecord m_vehicleInGarageRecord = null;
        protected Propulsion m_PropulsionComponnet;

        protected Vehicle(string i_VehicleModelName, string i_VehicleLicenceNumber, int i_AmountOfWheels, string i_WheelManufacturerName, float i_MaxAirPressure)
        {
            r_VehicleModelName = i_VehicleModelName;
            r_VehicleLicenceNumber = i_VehicleLicenceNumber;
            m_AmoutOfWheels = i_AmountOfWheels;
            r_MaxAirPressure = i_MaxAirPressure;
            m_VehicleWheelCollection = attachWheelsToVehicle(i_AmountOfWheels, i_WheelManufacturerName, r_MaxAirPressure);
        }

        private List<Wheel> attachWheelsToVehicle(int i_WheelsToAdd, string i_WheelManufacturer, float i_MaxAirPressure)
        {
            List<Wheel> m_VehicleWheels = new List<Wheel>();
            for (int i = 0; i < i_WheelsToAdd; i++)
            {
                Wheel wheel = new Wheel(i_WheelManufacturer, i_MaxAirPressure);
                m_VehicleWheels.Add(wheel);
            }

            return m_VehicleWheels;
        }

        public Propulsion Propulsion
        {
            get
            {
                return m_PropulsionComponnet;
            }

            set
            {
                m_PropulsionComponnet = value;
            }
        }

        public float MaxAirPressure
        {
            get
            {
                return r_MaxAirPressure;
            }
        }

        public string ModelName
        {
            get
            {
                return r_VehicleModelName;
            }
        }

        public string LicenceNumber
        {
            get
            {
                return r_VehicleLicenceNumber;
            }
        }

        public float EnergyPercentage
        {
            get
            {
                return m_EnergyPercentage;
            }

            set
            {
                m_EnergyPercentage = value;
            }
        }

        public List<Wheel> WheelBoltedToVehicle
        {
            get
            {
                return m_VehicleWheelCollection;
            }

            set
            {
                m_VehicleWheelCollection = value;
            }
        }

        public VehicleRecord VehicleInGarageRecord
        {
            get
            {
                return m_vehicleInGarageRecord;
            }

            set
            {
                m_vehicleInGarageRecord = value;
            }
        }       

        public int AmountOfWheels
        {
            get
            {
                return m_AmoutOfWheels;
            }
        }

        public override int GetHashCode()
        {
            return this.r_VehicleLicenceNumber.GetHashCode();
        }

        public void UpdateVehicleCurrentPropulsion(float i_userPropulsionAmount)
        {
            Propulsion.CurrentPropulsionAmount = i_userPropulsionAmount;
            EnergyPercentage = (Propulsion.CurrentPropulsionAmount / Propulsion.MaxPropulsion) * 100;
        }

        public void UpdateVehicleCurrentPropulsion()
        {
            EnergyPercentage = (Propulsion.CurrentPropulsionAmount / Propulsion.MaxPropulsion) * 100;
        }

        public override string ToString()
        {
            System.Text.StringBuilder formatStrBuilder = new System.Text.StringBuilder();
            formatStrBuilder.Append(string.Format("Vehicle Licence Number: {0}    Vehicle Model Name: {1}{2}Energy Percentage: {3}%    Amount Of Wheels: {4}{5}Wheels Information{6}", r_VehicleLicenceNumber.ToString(), r_VehicleModelName.ToString(), Environment.NewLine, m_EnergyPercentage.ToString(), m_AmoutOfWheels.ToString(), VehicleInGarageRecord.ToString(), Environment.NewLine));
            foreach (Wheel wheel in m_VehicleWheelCollection)
            {
                formatStrBuilder.Append("      ").Append(wheel.ToString()).Append(Environment.NewLine);
            }

            return formatStrBuilder.ToString();
        }
    }
}
