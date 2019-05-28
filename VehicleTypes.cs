namespace Ex03.GarageLogic
{
    public class VehicleTypes
    {
        public enum eVehicleTypes
        {
            FuelCar,
            FuelMotorcycle,
            Truck,
            ElectricCar,
            ElectricMotorcycle
        }

        public static Vehicle CreateVehicle(int i_VehicleType, string i_VehicleLicenceNumber, string i_VehicleModelName, string i_WheelManufacturerName)
        {
            Vehicle vehicle;
            switch (i_VehicleType)
            {
                case 1:
                    vehicle = new FuelCar(i_VehicleModelName, i_VehicleLicenceNumber, i_WheelManufacturerName);
                    vehicle = vehicle as FuelCar;
                    break;
                case 2:
                    vehicle = new FuelMotorcycle(i_VehicleModelName, i_VehicleLicenceNumber, i_WheelManufacturerName);
                    vehicle = vehicle as FuelMotorcycle;
                    break;
                case 4:
                    vehicle = new ElectricMotorcycle(i_VehicleModelName, i_VehicleLicenceNumber, i_WheelManufacturerName);
                    vehicle = vehicle as ElectricMotorcycle;
                    break;
                case 5:
                    vehicle = new ElectricCar(i_VehicleModelName, i_VehicleLicenceNumber, i_WheelManufacturerName);
                    vehicle = vehicle as ElectricCar;
                    break;
                case 3:
                    vehicle = new Truck(i_VehicleModelName, i_VehicleLicenceNumber, i_WheelManufacturerName);
                    vehicle = vehicle as Truck;
                    break;
                default:
                    vehicle = null;
                    break;
            }

            return vehicle;
        }
    }
}
