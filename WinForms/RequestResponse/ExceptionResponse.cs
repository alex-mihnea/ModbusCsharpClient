using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForms.RequestResponse
{
    class ExceptionResponse
    {
        public byte m_ucExceptionFunctionCode { get; set; }
        public byte m_ucExceptionCode { get; set; }

        public ExceptionResponse(byte ucFunctionCode, byte ucExceptionCode)
        {
            m_ucExceptionFunctionCode = ucFunctionCode;
            m_ucExceptionCode = ucExceptionCode;
        }
    }
}
