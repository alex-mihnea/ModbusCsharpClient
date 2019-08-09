using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForms.RequestResponse
{
    class ReadCoilsResponse
    {
        public byte m_ucAddress { get; set; }
        public byte m_ucFunctionCode { get; set; }
        public byte m_ucByteCount { get; set; }
        public byte[] m_aucCoilsVal { get; set; }

        public ReadCoilsResponse(byte ucAddress, byte ucFunctionCode, byte ucByteCount, byte[] aucCoilsVal)
        {
            m_ucAddress = ucAddress;
            m_ucFunctionCode = ucFunctionCode;
            m_ucByteCount = ucByteCount;
            for(int i = 0; i < aucCoilsVal.Length; i++)
            {
                m_aucCoilsVal[i] = aucCoilsVal[i];
            }
        }
    }
}
