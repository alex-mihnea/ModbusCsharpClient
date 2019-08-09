using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForms.RequestResponse
{
    class ReadCoilsRequest
    {
        public byte m_ucAddress { get; set; }
        public byte m_ucFunctionCode { get; set; }
        public ushort m_unStartingAddress { get; set; }
        public ushort m_unNumberOfCoils { get; set; }

        public ReadCoilsRequest( byte Address, byte FunctionCode, ushort StartingAddress, ushort NumberOfCoils)
        {
            m_ucAddress = Address;
            m_ucFunctionCode = FunctionCode;
            m_unStartingAddress = StartingAddress;
            m_unNumberOfCoils = NumberOfCoils;
        }
    }
}
