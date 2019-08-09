using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForms.RequestResponse
{
    class ReadHoldingRegsResponse
    {
        public byte m_ucAddress { get; set; }
        public byte m_ucFunctionCode { get; set; }
        public byte m_ucByteCount { get; set; }
        public ushort[] m_unInputRegisters { get; set; }

        public ReadHoldingRegsResponse(byte ucAddress, byte ucFunctionCode, byte ucByteCount)
        {
            m_ucAddress = ucAddress;
            m_ucFunctionCode = ucFunctionCode;
            m_ucByteCount = ucByteCount;
            m_unInputRegisters = new ushort[125];
        }
    }
}
