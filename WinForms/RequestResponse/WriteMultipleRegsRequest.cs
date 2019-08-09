using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForms.RequestResponse
{
    class WriteMultipleRegsRequest
    {
        public byte m_ucAddress { get; set; }
        public byte m_ucFunctionCode { get; set; }
        public ushort m_unStartingAddress { get; set; }
        public ushort m_unNumberOfRegisters { get; set; }
        public byte m_ucByteCount { get; set; }
        public ushort[] m_unRegisters { get; set; }

        public WriteMultipleRegsRequest(byte ucAddress, byte ucFunctionCode, ushort unStartingAddress, ushort unNumberOfRegisters, byte ucByteCount)
        {
            m_ucAddress = ucAddress;
            m_ucFunctionCode = ucFunctionCode;
            m_unStartingAddress = unStartingAddress;
            m_unNumberOfRegisters = unNumberOfRegisters;
            m_ucByteCount = ucByteCount;
            m_unRegisters = new ushort[123];
        }
    }
}
