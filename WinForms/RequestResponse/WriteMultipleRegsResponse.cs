using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForms.RequestResponse
{
    class WriteMultipleRegsResponse
    {
        public byte m_ucAddress { get; set; }
        public byte m_ucFunctionCode { get; set; }
        public ushort m_unStartingAddress { get; set; }
        public ushort m_unNumberOfRegisters { get; set; }

        public WriteMultipleRegsResponse(byte ucAddress, byte ucFunctionCode, ushort unStartingAddress, ushort unNumberOfRegisters)
        {
            m_ucAddress = ucAddress;
            m_ucFunctionCode = ucFunctionCode;
            m_unStartingAddress = unStartingAddress;
            m_unNumberOfRegisters = unNumberOfRegisters;
        }
    }
}
