using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForms.RequestResponse
{
    class WriteCoilsRequest
    {
        public byte m_ucAddress { get; set; }
        public byte m_ucFunctionCode { get; set; }
        public ushort m_unStartingAddress { get; set; }
        public ushort m_unNumberOfCoils { get; set; }
        public byte m_ucByteCount { get; set; }
        public byte[] m_aucCoils { get; set; }

        public WriteCoilsRequest(byte ucAddress, byte ucFunctionCode, ushort unStartingAddress, ushort unNumberOfCoils, byte ucByteCount)
        {
            m_ucAddress = ucAddress;
            m_ucFunctionCode = ucFunctionCode;
            m_unStartingAddress = unStartingAddress;
            m_unNumberOfCoils = unNumberOfCoils;
            m_ucByteCount = ucByteCount;
            m_aucCoils = new byte[255];
        }
    }
}
