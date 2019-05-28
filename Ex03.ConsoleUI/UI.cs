using System;
using System.Collections.Generic;
using Ex03.GarageLogic;
using Ex03.GarageLogic.Exceptions;

namespace Ex03.ConsoleUI
{
    public class UI
    {
        public static void Run()
        {
            GarageFlow();
        }

        public static int PrintMainMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to our garage!");
            Console.WriteLine("Please choose one of the followings - ");
            Console.WriteLine(@"1. Enter your vehicle to the garage
2. Display licence plate number of checked in vehicles by status.
3. Change vehicle's status.
4. Pump air in wheels.
5. Fuel vehicle.
6. Charge vehicle.
7. Display full details of vehicle.
8. Exit.

Your choice? (1-8).");
            bool isParsed = int.TryParse(Console.ReadLine().ToString(), out int userChoice);
            if (isParsed == false)
            {
                Console.WriteLine(string.Format("Not a valid option"));
            }

            return userChoice;
        }
        
        public static void GarageFlow()
        {
            Garage garageOfVehicles = new Garage();
            bool isQuitGarage = false;
            bool isDecisionOutOfRange = false;
            while(isDecisionOutOfRange != true)
            {
                try
                {
                    int currentDecision = PrintMainMenu();
                    while (isQuitGarage != true)
                    {
                        switch (currentDecision)
                        {
                            case 1:
                                EnterNewVehicleToGarage(garageOfVehicles);
                                break;
                            case 2:
                                ShowAllVehicales(garageOfVehicles);
                                break;
                            case 3:
                                EnterValuesForUpdateStatus(garageOfVehicles);
                                break;
                            case 4:
                                PumpWheels(garageOfVehicles);
                                break;
                            case 5:
                                FuelUsersVehicle(garageOfVehicles);
                                break;
                            case 6:
                                ChargeUsersVehicle(garageOfVehicles);
                                break;
                            case 7:
                                PrintAllVehicleData(garageOfVehicles);
                                break;
                            case 8:
                                isQuitGarage = true;
                                isDecisionOutOfRange = true;
                                return;
                            default:
                                throw new ValueOutOfRangeException(8, 1);
                        }

                        currentDecision = PrintMainMenu();
                    }
                }
                catch (FormatException)
                {
                    PrintFormatExceptionDecimal();
                }
                catch(ValueOutOfRangeException i_ValueOutOfRangeException)
                {
                    Console.WriteLine(string.Format("Value out of range must be between {0} - {1}", i_ValueOutOfRangeException.MinimumValue, i_ValueOutOfRangeException.MaximumValue));
                }
                catch(ArgumentException i_ArgumentException)
                {
                    Console.WriteLine(string.Format("Invalid arguments in step {0}", i_ArgumentException.StackTrace));
                }

                PressAnyKeyToContinue();
            }   
        }

        ////Comment for tester - Exception for parse will be caught in method which calls this one.
        public static void AskForVehicleBasicInput(Garage i_Garage, ref Vehicle io_CurrentNewVehicle, string i_CurrentVehicleLicenceNumber)
        {
            Console.WriteLine(@"Enter your vehicle type by precedening number - 
1. Fuel Car   2. Fuel Motorcycle
3. Truck      4. Electric Motorcycle
5. Electric Car");
            bool isInputValid = false;
            bool isParsed = int.TryParse(Console.ReadLine().ToString(), out int vehicleType);
            if (isParsed == false)
            {
                Console.WriteLine(string.Format("Not a valid option"));
            }

            while (isInputValid == false)
            {
                isInputValid = (vehicleType <= 5) && (vehicleType >= 1);
                if (isInputValid == false)
                {
                    throw new ValueOutOfRangeException(5, 1);
                }
            }

            Console.WriteLine("Please enter Vehicel model name");
            string vehicleModelName = Console.ReadLine();
            Console.WriteLine("Please enter wheel manufacturer name");
            string wheelManuName = Console.ReadLine();
            io_CurrentNewVehicle = i_Garage.InputVehicleRecord(vehicleType, i_CurrentVehicleLicenceNumber, vehicleModelName, wheelManuName);
        }

        public static void EnterValuesForUpdateStatus(Garage i_Garage)
        {
            bool isValidInput = false;
            while(isValidInput != true)
            {
                try
                {
                    Console.Clear();

                    Console.WriteLine("In order to change your vehicle's status please insert the followings - ");
                    string licenceNumber = AskForLicenceNumberInGarage(i_Garage);
                    if(licenceNumber == "q")
                    {
                        return;
                    }

                    Vehicle currentVehicle = i_Garage.GetVehicleFromGarage(licenceNumber);

                    Console.WriteLine(@"New vehicle statue - 
1. Under repair.
2. Fixed.
3. Paid");
                    bool isParsed = int.TryParse(Console.ReadLine().ToString(), out int vehicleStatus);
                    if (isParsed == false)
                    {
                        Console.WriteLine(string.Format("Not a valid option"));
                    }

                    isValidInput = true;
                    i_Garage.UpdateStatusOfCar(currentVehicle, licenceNumber, (VehicleRecord.eVehicleStatus)vehicleStatus);
                    Console.WriteLine("Successfully updated vehicle's status!");
                }
                catch (FormatException i_FormatException)
                {
                    PrintFormatExceptionDecimal();
                }
                catch(ArgumentException i_ArgumentException)
                {
                    Console.WriteLine("Argument isn't valid!");
                }
                catch(ValueOutOfRangeException i_ValueOutOfRangeException)
                {
                    Console.WriteLine(string.Format("Value out of range! To update it, it must be between {0} - {1}", i_ValueOutOfRangeException.MinimumValue, i_ValueOutOfRangeException.MaximumValue));
                }
            }

            PressAnyKeyToContinue();
        }

        public static string AskForLicenceNumberInGarage(Garage i_Garage)
        {
            bool isValid = false;
            string licenceNumber;
            Console.Write("Please insert your licence plate number:   ");
            licenceNumber = Console.ReadLine();
            do
            {
                i_Garage.CheckIfStringIsNumber(licenceNumber);
                isValid = i_Garage.IsVehicleInGarageBool(licenceNumber);
                if (isValid == false)
                {
                    Console.WriteLine("Licence number cannot be found in our garage...");
                    Console.WriteLine("If you wish to quit this option press 'q' else insert licence number again");
                    licenceNumber = Console.ReadLine();
                    if (licenceNumber == "q")
                    {
                        break;
                    }
                }                
            }
            while (isValid != true);

            return licenceNumber; 
        }

        public static void PumpWheels(Garage i_Garage)
        {
            try
            {
                string licenceNumber = AskForLicenceNumberInGarage(i_Garage);
                i_Garage.PumpAllWheelsToMax(licenceNumber);

                int wheelAmount = i_Garage.AllGarageVehicles[licenceNumber.GetHashCode()].AmountOfWheels;
                for (int i = 0; i < wheelAmount; i++)
                {
                    Console.WriteLine(string.Format("Wheel {0} - air pressure is now -> {1}", i + 1, i_Garage.AllGarageVehicles[licenceNumber.GetHashCode()].WheelBoltedToVehicle[i].CurrentAirPressure));
                }

                Console.WriteLine(string.Format("Successfully filled air in your wheels!"));
                PressAnyKeyToContinue();
            }
            catch (ArgumentException i_CurrentException)
            {
                Console.WriteLine("Value oassed should only be licence number!");
            }   
        }

        public static void PrintAllLicenceNumbers(Garage i_Garage)
        {
            short vehicleIndex = 1;
            foreach (KeyValuePair<int, Vehicle> item in i_Garage.AllGarageVehicles)
            {
                Console.WriteLine(string.Format("{0}   {1}", vehicleIndex++, item.Value.LicenceNumber));
            }

            PressAnyKeyToContinue();
        }

        public static void PressAnyKeyToContinue()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }

        public static void DisplayChosenVehicles(Garage i_Garage, VehicleRecord.eVehicleStatus i_ChosenType)
        {
            Console.WriteLine(string.Format("Vehicles with status - {0}", i_ChosenType));
            short vehicleIndex = 1;
            foreach (KeyValuePair<int, Vehicle> item in i_Garage.AllGarageVehicles)
            {
                if(item.Value.VehicleInGarageRecord.CurrentVehicleStatus == i_ChosenType)
                {
                    Console.WriteLine(string.Format("{0}   {1}", vehicleIndex, item.Value.LicenceNumber));
                    vehicleIndex++;
                }
            }

            PressAnyKeyToContinue();
        }

        public static void ShowAllVehicales(Garage i_Garage)
        {
            int viewVehicles = 0;
            VehicleRecord.eVehicleStatus chosenType = 0;
            try
            {
                Console.WriteLine(@"Pick how you want to see the vehicles - 
1. All           2. Under Repairs
3. Fixed         4. Paid for
5. Exit");
                bool isParsed = int.TryParse(Console.ReadLine().ToString(), out viewVehicles);
                if (isParsed == false)
                {
                    Console.WriteLine(string.Format("Not a valid option"));
                }

                do
                {
                    switch (viewVehicles)
                    {
                        case 1:
                            PrintAllLicenceNumbers(i_Garage);
                            break;
                        case 2:
                            chosenType = VehicleRecord.eVehicleStatus.UnderRepairs;
                            break;
                        case 3:
                            chosenType = VehicleRecord.eVehicleStatus.Fixed;
                            break;
                        case 4:
                            chosenType = VehicleRecord.eVehicleStatus.PaidFor;
                            break;
                        case 5:
                            Console.WriteLine("Exit succeeded.");
                            return;
                        default:
                            Console.WriteLine("Not Valid... Please insert again");
                            isParsed = int.TryParse(Console.ReadLine().ToString(), out viewVehicles);
                            if (isParsed == false)
                            {
                                Console.WriteLine(string.Format("Not a valid option"));
                            }

                            break;
                    }

                    if(chosenType != 0)
                    {
                        DisplayChosenVehicles(i_Garage, chosenType);
                    }

                    break;
                }
                while (viewVehicles != 5);
            }
            catch (FormatException i_FormatException)
            {
                Console.WriteLine("Value must be a number!");
                PressAnyKeyToContinue();
            }
        }

        public static VehicleRecord AskForOwnerInfo(Garage i_Garage)
        {
            bool isNumber = false;
            bool isLength = false;
            bool isAllValid = false;
            bool isNameNotString = true;
            string userPhoneInput = string.Empty;
            string userNameInput = string.Empty;
            VehicleRecord vehicleRecord = null;
            while(isAllValid != true)
            {
                try
                {
                    while (isNameNotString != false)
                    {
                        Console.WriteLine(string.Format("Please Enter vehicle owners name"));
                        userNameInput = Console.ReadLine();
                        i_Garage.CheckIfStringIsLetters(userNameInput);
                        isNameNotString = false;
                    }

                    while ((isNumber != true) || (isLength != true))
                    {
                        Console.WriteLine(string.Format("Please Enter vehicle owners phone number"));
                        userPhoneInput = Console.ReadLine();
                        i_Garage.CheckIfStringIsNumber(userPhoneInput);
                        isNumber = true;
                        i_Garage.PhoneIsTenChars(userPhoneInput);
                        isLength = true;
                    }

                    vehicleRecord = new VehicleRecord(userNameInput, userPhoneInput);
                    isAllValid = true;
                }
                catch (ArgumentException i_ArgumentException)
                {
                    Console.WriteLine(i_ArgumentException.Message);
                }
            }
            
            return vehicleRecord;
        }

        public static void AskForCurrentAirPressure(Vehicle i_CurrentVehicle)
        {
            for (int i = 0; i < i_CurrentVehicle.WheelBoltedToVehicle.Count; i++)
            {
                try
                {
                    Console.WriteLine(string.Format("Enter current air pressure for wheel number {0}", i + 1));
                    bool isParsed = float.TryParse(Console.ReadLine().ToString(), out float currentAirPressure);
                    if (isParsed == false)
                    {
                        Console.WriteLine(string.Format("Not a valid option"));
                    }

                    i_CurrentVehicle.WheelBoltedToVehicle[i].CurrentAirPressure = currentAirPressure;
                }
                catch (FormatException i_FormatException)
                {
                    PrintFormatExceptionDecimal();
                }
                catch (ValueOutOfRangeException i_ValueOutOfRangeException)
                {
                    Console.WriteLine(string.Format("Value not in range! Must be between {0} - {1}", i_ValueOutOfRangeException.MinimumValue, i_ValueOutOfRangeException.MaximumValue));
                    i--;
                }
            }
        }
            
        public static void PrintFormatExceptionDecimal()
        {
            Console.WriteLine("Exception! Value must be a decimal number!");
        }
        
        public static string AskForNewLicenceNumber(Garage i_Garage)
        {
            Console.WriteLine("Please insert your licence plater number");
            string licencenumber = Console.ReadLine();
            i_Garage.CheckIfStringIsEmpty(licencenumber);
            i_Garage.CheckIfStringIsNumber(licencenumber);
            return licencenumber;
        }

        public static void EnterNewVehicleToGarage(Garage i_Garage)
        {
            bool isStringValid = false;
            while(isStringValid != true)
            {
                try
                {
                    string userLicenceInput = AskForNewLicenceNumber(i_Garage);
                    isStringValid = true;

                    Vehicle currentVehicle = i_Garage.GetVehicleFromGarage(userLicenceInput);
                    if (currentVehicle == null)
                    {
                        AskForVehicleBasicInput(i_Garage, ref currentVehicle, userLicenceInput);
                        VehicleRecord currentVehicleRecord = AskForOwnerInfo(i_Garage);
                        i_Garage.RecordAttachment(currentVehicle, currentVehicleRecord);
                        AskForCurrentAirPressure(currentVehicle);
                        AskForCurrentPropulsionAmount(currentVehicle);
                        if (currentVehicle is Car)
                        {
                            AskForCarExtraInput(i_Garage, ref currentVehicle);
                        }
                        else if (currentVehicle is Motorcycle)
                        {
                            AskForMotorcycleExtraInput(i_Garage, ref currentVehicle);
                        }
                        else if (currentVehicle is Truck)
                        {
                            AskForTruckExtraInput(i_Garage, ref currentVehicle);
                        }

                        i_Garage.AllGarageVehicles.Add(currentVehicle.GetHashCode(), currentVehicle);
                        Console.WriteLine("Successfully added your vehicle to the garage!");
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine(string.Format("A vehicle with an identical licence number is already in garage records "));
                        Console.WriteLine(string.Format("Owner name: {0}  owner phone number: {1}", currentVehicle.VehicleInGarageRecord.OwnersName, currentVehicle.VehicleInGarageRecord.OwnerPhoneNumber));
                        currentVehicle.VehicleInGarageRecord.CurrentVehicleStatus = VehicleRecord.eVehicleStatus.UnderRepairs;
                    }
                }
                catch (ArgumentException i_ArgumentException)
                {
                    Console.WriteLine(i_ArgumentException.Message);
                }
            }
            
            PressAnyKeyToContinue();
        }

        public static void AskForCurrentPropulsionAmount(Vehicle i_Vehicle)
        {
            bool isInputValid = false;
            bool isBattery = false;
            bool isParsed = false;
            float userPropulsionAmount = 0;
            string invalidityMessage = string.Empty;
            while (isInputValid != true)
            {
                try
                {
                    do
                    {
                        if (i_Vehicle.Propulsion is Battery)
                        {
                            isBattery = true;
                            Console.WriteLine("Please insert current amount of battery power, in minutes");
                            invalidityMessage = string.Format("the current amount of battery power, can not be more than the maximum amount : {0}.  Please try again{1}Enter the current amount of battery power, ", i_Vehicle.Propulsion.MaxPropulsion, Environment.NewLine);
                        }
                        else if (i_Vehicle.Propulsion is FuelTank)
                        {
                            Console.WriteLine("Please insert the current amount of fuel in the tank, in liters");
                            invalidityMessage = string.Format("the current amount of fuel, can not be more than the maximum amount : {0}.  Please try again{1}Enter the current amount of fuel in the tank, ", i_Vehicle.Propulsion.MaxPropulsion, Environment.NewLine);
                        }

                        isParsed = float.TryParse(Console.ReadLine().ToString(), out userPropulsionAmount);
                        Console.WriteLine(string.Format("Not a valid option"));
                        if (isBattery == true)
                        {
                            userPropulsionAmount /= 60f;
                        }
                    }
                    while (isParsed == false);
                    i_Vehicle.UpdateVehicleCurrentPropulsion(userPropulsionAmount);
                    isInputValid = true;
                }
                catch (FormatException i_FormatException)
                {
                    Console.WriteLine("Exception! Value must be a decimal number!");
                }
                catch (ValueOutOfRangeException i_ValueOutOfRangeException)
                {
                    Console.WriteLine(string.Format("Value out of range! value must be between {0} - {1}", i_Vehicle.Propulsion.MaxPropulsion, 0));
                }
            }
        }

        public static void AskForMotorcycleExtraInput(Garage i_Garage, ref Vehicle io_CurrentNewMotorcycle)
        {
            bool allIsValid = false;
            while (allIsValid != true)
            {
                bool isInputValid = false;
                Console.WriteLine(@"Enter your motorcycle licence type by precedening number - 
1. A   2. A1
3. A2  4. B");

                try
                {
                    bool isParsed = int.TryParse(Console.ReadLine().ToString(), out int motorcycleLicencesType);
                    if (isParsed == false)
                    {
                        Console.WriteLine(string.Format("Not a valid option"));
                    }

                    while (isInputValid == false)
                    {
                        isInputValid = (motorcycleLicencesType <= 4) && (motorcycleLicencesType >= 1);
                        if (isInputValid == false)
                        {
                            throw new ValueOutOfRangeException(4, 1);
                        }
                    }

                    Console.WriteLine("Please enter motorcycle engine displacement");
                    isParsed = int.TryParse(Console.ReadLine().ToString(), out int engineDisplacement);
                    if (isParsed == false)
                    {
                        Console.WriteLine(string.Format("Not a valid option"));
                    }

                    (io_CurrentNewMotorcycle as Motorcycle).UpdateNewMotorcycleInput(motorcycleLicencesType, engineDisplacement);
                    allIsValid = true;
                }
                catch (FormatException i_FormatException)
                {
                    PrintFormatExceptionDecimal();
                }
                catch (ValueOutOfRangeException i_ValueOutOfRangeException)
                {
                    Console.WriteLine(string.Format("Exception! Value out of range! Value must be between {0} - {1}", i_ValueOutOfRangeException.MinimumValue, i_ValueOutOfRangeException.MaximumValue));
                }
            }   
        }

        public static void AskForCarExtraInput(Garage i_Garage, ref Vehicle io_CurrentNewCar)
        {
            bool isAllValid = false;
            while(isAllValid != true)
            {
                bool isInputValid = false;
                Console.WriteLine(@"Enter your car color type by precedening number - 
1. Red   2. Blue
3. Grey  4. Black");
                
                try
                {
                    bool isParsed = int.TryParse(Console.ReadLine().ToString(), out int carColor);
                    if (isParsed == false)
                    {
                        Console.WriteLine(string.Format("Not a valid option"));
                    }

                    while (isInputValid == false)
                    {
                        isInputValid = (carColor <= 4) && (carColor >= 1);
                        if (isInputValid == false)
                        {
                            throw new ValueOutOfRangeException(4, 1);
                        }
                    }

                    Console.WriteLine(@"Please enter amount of doors in the car - 
2 - Two doors
3 - three doors
4 - four doors
5 - five doors");
                    isInputValid = false;
                    isParsed = int.TryParse(Console.ReadLine().ToString(), out int amountOfDoors);
                    if (isParsed == false)
                    {
                        Console.WriteLine(string.Format("Not a valid option"));
                    }

                    while (isInputValid == false)
                    {
                        isInputValid = (amountOfDoors <= 5) && (amountOfDoors >= 2);
                        if (isInputValid == false)
                        {
                            throw new ValueOutOfRangeException(4, 1);
                        }
                    }

                    (io_CurrentNewCar as Car).UpdateNewCarInput(amountOfDoors, amountOfDoors);
                    isAllValid = true;
                }
                catch (FormatException i_FormatException)
                {
                    PrintFormatExceptionDecimal();
                }
                catch (ValueOutOfRangeException i_ValueOutOfRangeException)
                {
                    Console.WriteLine(string.Format("Value must be a number between {0} - {1}!", i_ValueOutOfRangeException.MinimumValue, i_ValueOutOfRangeException.MaximumValue));
                }
                catch (ArgumentException i_ArgumentException)
                {
                    Console.WriteLine("Something went wrong.... please try to re-enter values");
                }
                catch (NullReferenceException i_NullReferenceException)
                {
                    Console.WriteLine("Something went wrong.... please try again");
                }
            }
        }

        public static void AskForTruckExtraInput(Garage i_Garage, ref Vehicle io_CurrentNewTruck)
        {
            bool isAllValidInput = false;
            while (isAllValidInput != true)
            {
                try
                {
                    Console.WriteLine("Enter your trucks volume of cargo");
                    bool isParsed = float.TryParse(Console.ReadLine().ToString(), out float volumeOfCargo);
                    if (isParsed == false)
                    {
                        Console.WriteLine(string.Format("Not a valid option"));
                    }

                    Console.WriteLine("Does the truck contain any hazardous materials ?   y/n");
                    string isHazardousMaterialTruck = Console.ReadLine();
                    if (isHazardousMaterialTruck == "y" || isHazardousMaterialTruck == "n")
                    {
                        isAllValidInput = true;
                        (io_CurrentNewTruck as Truck).UpdateNewTruckInput(isHazardousMaterialTruck, volumeOfCargo);
                    }
                    else
                    {
                        throw new ArgumentException();
                    }
                }
                catch (FormatException i_FormatException)
                {
                    PrintFormatExceptionDecimal();
                }
                catch(ArgumentException i_ArgumentException)
                {
                    Console.WriteLine("Values must be y/n only!");
                }
            }  
        }

        public static void FuelUsersVehicle(Garage i_Garage)
        {
            bool isInputValid = false;
            while(isInputValid != true)
            {
                try
                {
                    string vehicleLicenceNumber = AskForLicenceNumberInGarage(i_Garage);
                    if (CheckIfQuit(vehicleLicenceNumber) == true)
                    {
                        return;
                    }

                    Console.WriteLine(@"Enter your vehicles fuel type by precedening number - 
1. Octan 98    2. Octan 96
3. Octan 95    4. Soler");
                    bool isParsed = int.TryParse(Console.ReadLine().ToString(), out int selectedFuelType);
                    if (isParsed == false)
                    {
                        Console.WriteLine(string.Format("Not a valid option"));
                    }

                    isInputValid = (selectedFuelType <= 4) && (selectedFuelType >= 1);
                    if(isInputValid != true)
                    {
                        throw new ValueOutOfRangeException(4, 1);
                    }
       
                    Console.WriteLine("Enter the amount of fuel you want to add");
                    isParsed = int.TryParse(Console.ReadLine().ToString(), out int amountOfFuelToAdd);
                    if (isParsed == false)
                    {
                        Console.WriteLine(string.Format("Not a valid option"));
                    }

                    i_Garage.PumpFuelToVehicle(vehicleLicenceNumber, selectedFuelType, amountOfFuelToAdd);
                    Console.WriteLine("Successfully fueled vehicle!");
                }
                catch (FormatException i_FormatException)
                {
                    PrintFormatExceptionDecimal();
                }
                catch (ValueOutOfRangeException i_ValueOutOfRangeException)
                {
                    Console.WriteLine(string.Format("Value out of range must be between {0} - {1}", i_ValueOutOfRangeException.MinimumValue, i_ValueOutOfRangeException.MaximumValue));
                }
                catch(ArgumentException i_ArgumentException)
                {
                    Console.WriteLine(i_ArgumentException.Message);
                }
                catch(NullReferenceException i_NullReferenceException)
                {
                    Console.WriteLine("Cannot fuel an electric vehicle!");
                }
            }

            PressAnyKeyToContinue();
        }

        public static void ChargeUsersVehicle(Garage i_Garage)
        {
            try
            {
                string vehicleLicenceNumber = AskForLicenceNumberInGarage(i_Garage);
                if (CheckIfQuit(vehicleLicenceNumber) == true)
                {
                    return;
                }

                Console.WriteLine("Enter the amount of minutes you want to charge");
                bool isParsed = float.TryParse(Console.ReadLine().ToString(), out float amountOfMinutesToAdd);
                if (isParsed == false)
                {
                    Console.WriteLine(string.Format("Not a valid option"));
                }

                i_Garage.ChargeVehicle(vehicleLicenceNumber, amountOfMinutesToAdd / 60f);
                Console.WriteLine(string.Format("Successfully Charged Your vehicle with {0} minutes!", amountOfMinutesToAdd));   
            }
            catch(FormatException i_FormatException)
            {
                PrintFormatExceptionDecimal();
            }
            catch(ValueOutOfRangeException i_ValueOutOfRangeException)
            {
                Console.WriteLine(string.Format("Value out of range must be between {0} - {1}", i_ValueOutOfRangeException.MinimumValue, i_ValueOutOfRangeException.MaximumValue));
            }
            catch (NullReferenceException i_NullReferenceException)
            {
                Console.WriteLine("Cannot charge a fuel type vehicle!");
            }
            catch(ArgumentException i_ArgumentException)
            {
                Console.WriteLine(i_ArgumentException.Message);
            }

            PressAnyKeyToContinue();
        }

        public static bool CheckIfQuit(string i_IsQuit)
        {
            bool isValid = false;
            if(i_IsQuit == "q")
            {
                isValid = true;
            }

            return isValid;
        }

        public static void PrintAllVehicleData(Garage i_Garage)
        {
            string licenceNumber = AskForLicenceNumberInGarage(i_Garage);
            if(CheckIfQuit(licenceNumber) == true)
            {
                return;
            }

            Vehicle currentVehicle = i_Garage.GetVehicleFromGarage(licenceNumber);
            if (currentVehicle != null)
            {
                Console.WriteLine(currentVehicle.ToString());
                PressAnyKeyToContinue();
            }   
        }
    }
}