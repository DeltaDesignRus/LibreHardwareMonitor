using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
using System.Windows.Forms;

namespace LibreHardwareMonitor.Utilities
{
    public class Serial
    {
        string _Port;
        public string Port
        {
            get { return _Port; }
            set
            {
                _Port = value;
                _Properties.Default.ComName = _Port;
                _Properties.Default.Save();

            }
        }
        SerialPort serial;
        public Serial()
        {
            int Number = _Properties.Default.BaudRate;
            Port = _Properties.Default.ComName;
            serial = new SerialPort(string.IsNullOrEmpty(Port) ? "COM1" : Port, Number);

        }
        public bool Open()
        {
            if (serial.IsOpen)
            {
                try { serial.Close(); }
                catch { }
            }

            serial.PortName = Port;

            try
            {
                serial.Open();
            }
            catch (IOException e)
            {
                return false;
            }

            return true;
        }

        public void Close()
        {
            if (serial.IsOpen)
            {
                serial.Close();
            }
        }

        public void Write(byte[] data)
        {
            if (serial.IsOpen)
            {
                try
                {
                    serial.Write(data, 0, data.Length);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка отправки данных в COM-порт:\r\n\r\n" + ex.Message);

                    try { serial.Close(); }
                    catch { }

                    Open();
                }
            }
        }
    }
}
