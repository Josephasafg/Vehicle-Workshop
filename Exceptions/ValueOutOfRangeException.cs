using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic.Exceptions
{
    public class ValueOutOfRangeException : Exception
    {
        protected float m_MaxValue;
        protected float m_MinValue;

        public float MaximumValue
        {
            get { return m_MaxValue; }
        }

        public float MinimumValue
        {
            get { return m_MinValue; }
        }

        public ValueOutOfRangeException(float i_MaxValue, float i_MinValue) : base(string.Format("A value out of range exception has risen!{0}Values can be between {1} - {2}", Environment.NewLine, i_MinValue, i_MaxValue))
        {
            m_MinValue = i_MinValue;
            m_MaxValue = i_MaxValue;
        }

        public ValueOutOfRangeException(Exception i_InnerException, float i_MaxValue, float i_MinValue) : base(string.Format("An error has occurred while trying to {0}.The value can be between {1} and {2}", i_MinValue, i_MaxValue, i_InnerException.StackTrace[i_InnerException.StackTrace.Count()]), i_InnerException)
        {
            m_MaxValue = i_MaxValue;
            m_MinValue = i_MinValue;
        }
    }
}
