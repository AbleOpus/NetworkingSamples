using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace BasicAsyncClient
{
    public partial class ClientForm : Form
    {
        private Socket clientSocket;

        public ClientForm()
        {
            InitializeComponent();
        }

        private static void ShowErrorDialog(string message)
        {
            MessageBox.Show(message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ConnectCallback(IAsyncResult AR)
        {
            try
            {
                clientSocket.EndConnect(AR);
                UpdateControlStates(true);
            }
            catch (SocketException ex)
            {
                ShowErrorDialog(ex.Message);
            }
            catch (ObjectDisposedException ex)
            {
                ShowErrorDialog(ex.Message);
            }
        }

        private void SendCallback(IAsyncResult AR)
        {
            try
            {
                clientSocket.EndSend(AR);
            }
            catch (SocketException ex)
            {
                ShowErrorDialog(ex.Message);
            }
            catch (ObjectDisposedException ex)
            {
                ShowErrorDialog(ex.Message);
            }
        }

        /// <summary>
        /// A thread safe way to enable the send button.
        /// </summary>
        private void UpdateControlStates(bool toggle)
        {
            Invoke((Action)delegate
            {
                buttonSend.Enabled = toggle;
                buttonConnect.Enabled = !toggle;
                labelIP.Visible = textBoxAddress.Visible = !toggle;
            });
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            try
            {
                // Serialize the textBoxes text before sending.
                PersonPackage person = new PersonPackage(checkBoxMale.Checked, (ushort)numberBoxAge.Value, textBoxEmployee.Text);

                byte[] buffer = person.ToByteArray();
                clientSocket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, SendCallback, null);
            }
            catch (SocketException ex)
            {
                ShowErrorDialog(ex.Message);
                UpdateControlStates(false);
            }
            catch (ObjectDisposedException ex)
            {
                ShowErrorDialog(ex.Message);
                UpdateControlStates(false);
            }
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            try
            {
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                // Connect to the specified host.
                var endPoint = new IPEndPoint(IPAddress.Parse(textBoxAddress.Text), 3333);
                clientSocket.BeginConnect(endPoint, ConnectCallback, null);
            }
            catch (SocketException ex)
            {
                ShowErrorDialog(ex.Message);
            }
            catch (ObjectDisposedException ex)
            {
                ShowErrorDialog(ex.Message);
            }
        }
    }
}
