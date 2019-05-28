using System;
using Ex03.GarageLogic.Exceptions;

namespace Ex03.GarageLogic
{
    public class VehicleRecord
    {
        private string m_VehicleOwnerName;
        private string m_VehicleOwnerPhoneNumber;
        private eVehicleStatus m_VehicleInGarageStatus;
        private Vehicle m_VehicleOnRecord;
        
        public VehicleRecord(string i_VehicleOwnerName, string i_VehicleOwnerPhoneNumber)
        {
            m_VehicleInGarageStatus = eVehicleStatus.UnderRepairs;
            if(i_VehicleOwnerName.Equals(string.Empty) || i_VehicleOwnerPhoneNumber.Equals(string.Empty))
            {
                throw new ArgumentException();
            }

            m_VehicleOwnerName = i_VehicleOwnerName;
            m_VehicleOwnerPhoneNumber = i_VehicleOwnerPhoneNumber;
        }

        public string OwnersName
        {
            get
            {
                return m_VehicleOwnerName;
            }

            set
            {
                m_VehicleOwnerName = value;
            }
        }

        public string OwnerPhoneNumber
        {
            get
            {
                return m_VehicleOwnerPhoneNumber;
            }

            set
            {
                m_VehicleOwnerPhoneNumber = value;
            }
        }

        public Vehicle ReferenceToVehicle
        {
            get
            {
                return m_VehicleOnRecord;
            }

            set
            {
                m_VehicleOnRecord = value;
            }
        }

        public eVehicleStatus CurrentVehicleStatus
        {
            get
            {
                return m_VehicleInGarageStatus;
            }

            set
            {
                if((value <= (eVehicleStatus)3) && (value >= (eVehicleStatus)1))
                {
                    m_VehicleInGarageStatus = value;
                }
                else
                {
                    throw new ValueOutOfRangeException(3, 1);
                }
            }
        }

        public enum eVehicleStatus
        {
            UnderRepairs = 1,
            Fixed = 2,
            PaidFor = 3
        }

        public override string ToString()
        {
            return string.Format("{0}Vehicle Owner Name : {1}   Phone Number : {2}    Vehicle Status in the garage : {3}{4}", Environment.NewLine, m_VehicleOwnerName.ToString(), m_VehicleOwnerPhoneNumber.ToString(), m_VehicleInGarageStatus, Environment.NewLine);
        }
    }
}
