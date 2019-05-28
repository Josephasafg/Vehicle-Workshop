using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private Dictionary<int, Vehicle> m_AllGarageVehicles;

        public Garage()
        {
            AllGarageVehicles = new Dictionary<int, Vehicle>();
        }

        public Dictionary<int, Vehicle> AllGarageVehicles
        {
            get
            {
                return m_AllGarageVehicles;
            }

            set
            {
                m_AllGarageVehicles = value;
            }
        }

        public bool IsVehicleInGarageBool(string i_VehicleLicenceNumber)
        {
            bool isInDictionary = false;
            if (AllGarageVehicles.ContainsKey(i_VehicleLicenceNumber.GetHashCode()) == true)
            {
                isInDictionary = true;
            }

            return isInDictionary;
        }

        public Vehicle GetVehicleFromGarage(string i_VehicleLicenceNumber)
        {
            Vehicle currentVehicle;

            if (AllGarageVehicles.TryGetValue(i_VehicleLicenceNumber.GetHashCode(), out currentVehicle) == false)
            {
                currentVehicle = null;
            }
            
            return currentVehicle;
        }

        public void UpdateStatusOfCar(Vehicle i_CurrentVehicle, string i_VehicleLicenceNumber, VehicleRecord.eVehicleStatus i_UpdatedVehicleStatus)
        {
            i_CurrentVehicle.VehicleInGarageRecord.CurrentVehicleStatus = i_UpdatedVehicleStatus;
        }

        public void CheckIfStringIsEmpty(string i_StringToBeAnalayzed)
        {
            if(i_StringToBeAnalayzed.Equals(string.Empty))
            {
                throw new ArgumentException("String cannot be left empty!");
            }
        }

        public void CheckIfStringIsNumber(string i_StringToBeAnalayzed)
        {
            bool isNumber = true;
            for (int i = 0; (i < i_StringToBeAnalayzed.Length) && (isNumber != false); i++)
            {
                if (!char.IsDigit(i_StringToBeAnalayzed[i]))
                {
                    throw new ArgumentException("Value must only be digits!");
                }
            }
        }

        public void PhoneIsTenChars(string i_PhoneNumber)
        {
            if(i_PhoneNumber.Length != 10)
            {
                throw new ArgumentException("Phone number must be 10 characters!");
            }
        }

        public void CheckIfStringIsLetters(string stringToBeAnalayzed)
        {
            bool isLetters = false;
            for (int i = 0; (i < stringToBeAnalayzed.Length) && (isLetters != true); i++)
            {
                if (!char.IsLetter(stringToBeAnalayzed[i]))
                {
                    throw new ArgumentException("Value mustn't be numbers!");
                }
            }
        }

        public void PumpAllWheelsToMax(string i_VehicleLicenceNumber)
        {
            AllGarageVehicles.TryGetValue(i_VehicleLicenceNumber.GetHashCode(), out Vehicle o_CurrentVehicle);
            for (int i = 0; i < o_CurrentVehicle.WheelBoltedToVehicle.Count; i++)
            {
                float MaxAmountOfAirToAdd = o_CurrentVehicle.WheelBoltedToVehicle[i].MaxAirPressure - o_CurrentVehicle.WheelBoltedToVehicle[i].CurrentAirPressure;
                o_CurrentVehicle.WheelBoltedToVehicle[i].PumpAirIntoWheel(MaxAmountOfAirToAdd);
            }
        }

        public void PumpFuelToVehicle(string i_VehicleLicenceNumber, int i_SelectedFuelType, float i_AmountOfPropulsionToAdd)
        {
            FuelType.eFuelType currentPumpedFuel;
            AllGarageVehicles.TryGetValue(i_VehicleLicenceNumber.GetHashCode(), out Vehicle o_CurrentVehicle);
            switch (i_SelectedFuelType)
            {
                case 1:
                    currentPumpedFuel = FuelType.eFuelType.Octan98;
                    break;
                case 2:
                    currentPumpedFuel = FuelType.eFuelType.Octan96;
                    break;
                case 3:
                    currentPumpedFuel = FuelType.eFuelType.Octan95;
                    break;
                case 4:
                    currentPumpedFuel = FuelType.eFuelType.Soler;
                    break;
                default:
                    currentPumpedFuel = 0;
                    break;
            }

            FuelTank tankToBeFueld = o_CurrentVehicle.Propulsion as FuelTank;
            if (currentPumpedFuel == tankToBeFueld.FuelType)
            {
                tankToBeFueld.PumpFuelToTank(i_AmountOfPropulsionToAdd, currentPumpedFuel);
                o_CurrentVehicle.UpdateVehicleCurrentPropulsion();
            }
            else
            {
                throw new ArgumentException("Fuel type does not match! Didn't fuel vehicle...");
            }
        }

        public void ChargeVehicle(string i_VehicleLicenceNumber, float i_AmountOfPropulsionToAdd)
        {
            AllGarageVehicles.TryGetValue(i_VehicleLicenceNumber.GetHashCode(), out Vehicle o_CurrentVehicle);
            (o_CurrentVehicle.Propulsion as Battery).ChargeBattery(i_AmountOfPropulsionToAdd);
            o_CurrentVehicle.UpdateVehicleCurrentPropulsion();
        }

        public void RecordAttachment(Vehicle i_NewVehicle, VehicleRecord i_NewVehicleRecord)
        {
            i_NewVehicle.VehicleInGarageRecord = i_NewVehicleRecord;
        }

        public Vehicle InputVehicleRecord(int i_CarType, string i_VehicleLicenceNumber, string i_VehicleModelName, string i_WheelManufacturerName)
        {
            Vehicle currentVehicle = VehicleTypes.CreateVehicle(i_CarType, i_VehicleLicenceNumber, i_VehicleModelName, i_WheelManufacturerName);
            return currentVehicle;
        }
    }
}
