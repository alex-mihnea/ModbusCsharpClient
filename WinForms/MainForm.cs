using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinForms.RequestResponse;

namespace WinForms
{
    public partial class MainForm : Form
    {
        Modbus ModbusServer;
        string[] SerialPorts;
        SerialPort _serialPort;

        ReadCoilsRequest g_ReadCoilsReq;
        ReadCoilsResponse g_ReadCoilsResp;
        ExceptionResponse g_ExceptionResp;

        byte[] g_aucCoils = { 1, 1, 1, 1, 1, 1, 0, 1, 1, 0, 1, 1, 1, 1, 1, 1, 0, 0, 1, 0, 0, 1, 1, 1, 0 };

        byte[] g_aucRxADU = new byte[255];
        byte[] g_aucTxADU = new byte[255];
        byte g_ucByteIndex;
        byte MODBUS_ADDRESS = 0x1F;

        byte[] RxMessage = new byte[255];
        byte RxMessageIndex;


        public MainForm()
        {
            InitializeComponent();
            //System.Console.WriteLine("Serus");
            ServerInit();
        }

        public void ServerInit()
        {
            SerialPorts = SerialPort.GetPortNames();

            int contor;
            for(contor = 0; contor < SerialPorts.Length; contor++)
            {
                PortsComboBox.Items.Add(SerialPorts[contor]);
            }

            if(PortsComboBox.Items.Count != 0)
            {
                PortsComboBox.SelectedIndex = 0;
            }
            else
            {
                OpenButton.Enabled = false;
            }

        }

        // folosit la primul test cu placuta
        void Modbus_ReadCoilsResponse(ReadCoilsRequest RCRequest)
        {
            Array.Clear(g_aucCoils, 0, g_aucCoils.Length);
            if (g_ReadCoilsReq.m_ucAddress == 0xEC)
            {
                if (0x0001 <= RCRequest.m_unNumberOfCoils && RCRequest.m_unNumberOfCoils <= 0x07D0)
                {
                    if ((RCRequest.m_unStartingAddress >= 0x0000 && RCRequest.m_unStartingAddress <= 0xFFFF) &&
                    (RCRequest.m_unStartingAddress + RCRequest.m_unNumberOfCoils <= 0xFFFF))
                    {
                        if (RCRequest.m_unNumberOfCoils % 8 == 0)
                        {
                            g_ReadCoilsResp.m_ucByteCount = (byte)(RCRequest.m_unNumberOfCoils / 8);
                        }
                        else
                        {
                            g_ReadCoilsResp.m_ucByteCount = (byte)(RCRequest.m_unNumberOfCoils / 8 + 1);
                        }

                        g_ReadCoilsResp.m_ucAddress = MODBUS_ADDRESS;
                        g_ReadCoilsResp.m_ucFunctionCode = RCRequest.m_ucFunctionCode;
                        ushort i, j;
                        //printf("byte count: %d\n", g_ReadCoilsResp.m_ucByteCount);
                        for (i = 0; i < g_ReadCoilsResp.m_ucByteCount; i++)
                        {
                            for (j = 0; j < 8; j++)
                            {
                                if (i * 8 + j >= RCRequest.m_unNumberOfCoils)
                                {
                                    break;
                                }
                                g_ReadCoilsResp.m_aucCoilsVal[i] += (byte)(g_aucCoils[i * 8 + j] << j);
                                //printf("gauc coils: %x\t", g_aucCoils[i * 8 + j]);
                                //printf("response coils: %x\n", g_ReadCoilsResp.m_ucCoilStatus[i]);
                            }
                        }
                        // copiere in adu <- response
                        //memcpy(g_aucTxADU, &g_ReadCoilsResp, (3 + g_ReadCoilsResp.m_ucByteCount));
                        g_aucTxADU[0] = g_ReadCoilsResp.m_ucAddress;
                        g_aucTxADU[1] = g_ReadCoilsResp.m_ucFunctionCode;
                        g_aucTxADU[2] = g_ReadCoilsResp.m_ucByteCount;
                        Array.Copy(g_aucTxADU, 3, g_ReadCoilsResp.m_aucCoilsVal, 0, g_ReadCoilsResp.m_ucByteCount);
                        _serialPort.Write(g_aucTxADU, 0, 3 + g_ReadCoilsResp.m_ucByteCount);
                        
                    }
                    else
                    {
                        g_ExceptionResp.m_ucExceptionCode = 0x02;
                        g_ExceptionResp.m_ucExceptionFunctionCode = (byte)(RCRequest.m_ucFunctionCode + 0x80);
                        g_aucTxADU[0] = MODBUS_ADDRESS;
                        g_aucTxADU[1] = g_ExceptionResp.m_ucExceptionFunctionCode;
                        g_aucTxADU[2] = g_ExceptionResp.m_ucExceptionCode;
                        _serialPort.Write(g_aucTxADU, 0, 3);
                        //printf("out of bounds");
                    }
                }
                else
                {
                    g_ExceptionResp.m_ucExceptionCode = 0x03;
                    g_ExceptionResp.m_ucExceptionFunctionCode = (byte)(RCRequest.m_ucFunctionCode + 0x80);
                    g_aucTxADU[0] = MODBUS_ADDRESS;
                    g_aucTxADU[1] = g_ExceptionResp.m_ucExceptionFunctionCode;
                    g_aucTxADU[2] = g_ExceptionResp.m_ucExceptionCode;
                    _serialPort.Write(g_aucTxADU, 0, 3);
                    //printf("coils > 2000");
                }
            }
        }
        // folosit la primul test cu placuta
        public void Modbus_ParseMessage(byte ucByte)
        {
            g_aucRxADU[g_ucByteIndex++] = ucByte;

            if (g_ucByteIndex > 2)
            {
                switch (g_aucRxADU[1])
                {
                    case 0x01:  // read coils
                        {
                            if (g_ucByteIndex == 3 + g_aucRxADU[2])
                            {
                                Log(DateTime.Now.ToString("hh:mm:ss") + " | Read Coils Resp | ");
                                for (int i = 0; i < g_ucByteIndex; i++)
                                {
                                     Log(g_aucRxADU[i].ToString("X2") + " ");
                                }
                                Log("\r\n");
                                g_ucByteIndex = 0;
                                RxMessageIndex = 0;
                            }
                            break;
                        }
                    case 0x0f:  // write coils
                        {
                            if (g_ucByteIndex == 6)
                            {
                                Log(DateTime.Now.ToString("hh:mm:ss") + " | Write Coils Resp | ");
                                for (int i = 0; i < g_ucByteIndex; i++)
                                {
                                    Log(g_aucRxADU[i].ToString("X2") + " ");
                                }
                                Log("\r\n");
                                g_ucByteIndex = 0;
                                RxMessageIndex = 0;
                            }
                            break;
                        }

                    case 0x04:  // read input regs
                        {
                            if (g_ucByteIndex == 3 + g_aucRxADU[2])
                            {
                                Log(DateTime.Now.ToString("hh:mm:ss") + " | Read Input Resp | ");
                                for (int i = 0; i < g_ucByteIndex; i++)
                                {
                                    Log(g_aucRxADU[i].ToString("X2") + " ");
                                }
                                Log("\r\n");
                                g_ucByteIndex = 0;
                                RxMessageIndex = 0;
                            }
                            break;
                        }

                    case 0x10:  // write holding regs
                        {
                            if (g_ucByteIndex == 6)
                            {
                                Log(DateTime.Now.ToString("hh:mm:ss") + " | Write Holding Resp | ");
                                for (int i = 0; i < g_ucByteIndex; i++)
                                {
                                    Log(g_aucRxADU[i].ToString("X2") + " ");
                                }
                                Log("\r\n");
                                g_ucByteIndex = 0;
                                RxMessageIndex = 0;
                            }
                            break;
                        }

                    // error codes
                    case 0x81:
                    case 0x8f:
                    case 0x84:
                    case 0x83:
                        {
                            if (g_ucByteIndex == 3)
                            {
                                Log(DateTime.Now.ToString("hh:mm:ss") + " | Error Resp | ");
                                for (int i = 0; i < g_ucByteIndex; i++)
                                {
                                    Log(g_aucRxADU[i].ToString("X2") + " ");
                                }
                                Log("\r\n");
                                g_ucByteIndex = 0;
                                RxMessageIndex = 0;
                            }
                            break;
                        }
                }
            }
            
        }


        private void OpenButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Button press.");

            if (OpenButton.Text == "Open")
            {
                OpenButton.Text = "Close";
                ReadCoilsButton.Enabled = true;
                WriteCoilsButton.Enabled = true;
                ReadInputRegistersButton.Enabled = true;
                WriteHoldingRegistersButton.Enabled = true;

                _serialPort = new SerialPort();
                _serialPort.PortName = SerialPorts[PortsComboBox.SelectedIndex];
                _serialPort.BaudRate = 9600;
                _serialPort.DataBits = 8;
                _serialPort.StopBits = StopBits.One;
                _serialPort.Parity = Parity.None;
                _serialPort.Handshake = Handshake.None;
                _serialPort.DataReceived += _serialPort_DataReceived;
                try
                {
                    _serialPort.Open();
                }
                catch(Exception)
                {
                    Log(DateTime.Now.ToString("hh:mm:ss") + " || Cannot open serial port :( \r\n");
                }
            }
            else
            {
                OpenButton.Text = "Open";
                ReadCoilsButton.Enabled = false;
                WriteCoilsButton.Enabled = false;
                ReadInputRegistersButton.Enabled = false;
                WriteHoldingRegistersButton.Enabled = false;

                _serialPort.Close();
            }

        }

        private void LogTextBox_TextChanged(object sender, EventArgs e)
        {
            LogTextBox.SelectionStart = LogTextBox.Text.Length;
            LogTextBox.ScrollToCaret();
        }

        delegate void LogCallback(string text);

        public void Log(string text)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new LogCallback(Log), new object[] { text });
            }
            else
            {
                LogTextBox.AppendText(text);
                //LogTextBox.AppendText(DateTime.Now.ToString("hh:mm:ss:fff") + " | " + text + "\r\n");
            }
        }
   
        private void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //int ReturnCode;
            //byte RxByte;
            int NoOfBytes = _serialPort.BytesToRead;
            byte ByteIndex;

            _serialPort.Read(RxMessage, RxMessageIndex, NoOfBytes);

            for (ByteIndex = RxMessageIndex; ByteIndex < (RxMessageIndex + NoOfBytes); ByteIndex++)
            {
                Modbus_ParseMessage(RxMessage[ByteIndex]);
            }

            RxMessageIndex += (byte) NoOfBytes;

            /*
            for(int i = 0; i < _serialPort.BytesToRead; i++)
            {
                ReturnCode = _serialPort.ReadByte();
                if (ReturnCode != -1)
                {
                    Log(((byte)ReturnCode).ToString("X2")+ " " );
                }

                //Modbus_ParseMessage((byte)_serialPort.ReadByte());
            }
            //_serialPort.ReadByte();
            */
        }


        private void ReadCoilsButton_Click(object sender, EventArgs e)
        {
            if(_serialPort.IsOpen)
            {
                //g_aucTxADU = new byte[] { 0xEC,  0x01, 0x00, 0x00, 0x07, 0xD1 };
                g_aucTxADU = new byte[] { 0xEC, 0x01, 0x00, 0x00, 0x00, 0x08 };
                try
                {
                    _serialPort.Write(g_aucTxADU, 0, g_aucTxADU.Length);

                    Log(DateTime.Now.ToString("hh:mm:ss") + " | Read Coils Request | ");
                    for (int i = 0; i < g_aucTxADU.Length; i++)
                    {
                        Log(g_aucTxADU[i].ToString("X2") + " ");
                    }
                    Log("\r\n");
                }
                catch(IOException ex)
                {
                    Log(DateTime.Now.ToString("hh:mm:ss") + ex.ToString() + "\r\n");
                }
            }
            else
            {
                Log(DateTime.Now.ToString("hh:mm:ss") + " | Port is not open :(\r\n");
            }
        }

        private void WriteCoilsButton_Click(object sender, EventArgs e)
        {
            if(_serialPort.IsOpen)
            {
                g_aucTxADU = new byte[] { 0XEC, 0X0F, 0X00, 0X13, 0X00, 0X0A, 0X02, 0XCD, 0X01 };
                try
                {
                    _serialPort.Write(g_aucTxADU, 0, g_aucTxADU.Length);

                    Log(DateTime.Now.ToString("hh:mm:ss") + " | Write Coils Request | ");
                    for (int i = 0; i < g_aucTxADU.Length; i++)
                    {
                        Log(g_aucTxADU[i].ToString("X2") + " ");
                    }
                    Log("\r\n");
                }
                catch(IOException ex)
                {
                    Log(DateTime.Now.ToString("hh:mm:ss") + ex.ToString() + "\r\n");
                }
            }
            else
            {
                Log(DateTime.Now.ToString("hh:mm:ss") + " | Port is not open :(\r\n");
            }
        }

        private void ReadInputRegistersButton_Click(object sender, EventArgs e)
        {
            if (_serialPort.IsOpen)
            {
                g_aucTxADU = new byte[] { 0XEC, 0x04, 0x00, 0x01, 0x00, 0x02 };
                try
                {
                    _serialPort.Write(g_aucTxADU, 0, g_aucTxADU.Length);

                    Log(DateTime.Now.ToString("hh:mm:ss") + " | Read Input Request | ");
                    for (int i = 0; i < g_aucTxADU.Length; i++)
                    {
                        Log(g_aucTxADU[i].ToString("X2") + " ");
                    }
                    Log("\r\n");
                }
                catch (IOException ex)
                {
                    Log(DateTime.Now.ToString("hh:mm:ss") + ex.ToString() + "\r\n");
                }
            }
            else
            {
                Log(DateTime.Now.ToString("hh:mm:ss") + " | Port is not open :(\r\n");
            }
        }

        private void WriteHoldingRegistersButton_Click(object sender, EventArgs e)
        {
            if (_serialPort.IsOpen)
            {
                g_aucTxADU = new byte[] { 0XEC, 0x10, 0x00, 0x01, 0x00, 0x02, 0x04, 0x00, 0x0A, 0x01, 0x02 };
                try
                {
                    _serialPort.Write(g_aucTxADU, 0, g_aucTxADU.Length);

                    Log(DateTime.Now.ToString("hh:mm:ss") + " | Write Holding Request | ");
                    for (int i = 0; i < g_aucTxADU.Length; i++)
                    {
                        Log(g_aucTxADU[i].ToString("X2") + " ");
                    }
                    Log("\r\n");
                }
                catch (IOException ex)
                {
                    Log(DateTime.Now.ToString("hh:mm:ss") + ex.ToString() + "\r\n");
                }
            }
            else
            {
                Log(DateTime.Now.ToString("hh:mm:ss") + " | Port is not open :(\r\n");
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            LogTextBox.Text = "";
        }
    }
}
