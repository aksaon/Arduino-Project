using System.Threading;
using System.Windows.Forms;
using System.IO.Ports;
using System.Net.Mail;
 
namespace FinalProject
{
    public partial class Form1 : Form
    {
        public SerialPort serialPort = new SerialPort();
        string data;
        public Form1()
        {
            InitializeComponent();
        }
 
        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();//gets the names of the ports
            cmbBox_Ports.Items.AddRange(ports);//adds them to the comboBox
            cmbBox_Ports.SelectedIndex = 0; //selects the first port as a defualt
            btn_Close_prt.Enabled = false;
            threshold_txtBox.Enabled = false;//until the threshold checkbox is checked will be disabled
        }
 
        private void btn_Open_prt_Click(object sender, EventArgs e)
        {
            serialPort.PortName = cmbBox_Ports.Text;
            serialPort.BaudRate = 9600;
            serialPort.Open();
            if (serialPort.IsOpen)
            {
                btn_Close_prt.Enabled = true;
               btn_Open_prt.Enabled = false;
 
            }
        }
 
        private void btn_Close_prt_Click(object sender, EventArgs e)
        {
            serialPort.Close();
            if (!serialPort.IsOpen)
            {
                btn_Close_prt.Enabled = false;
                btn_Open_prt.Enabled = true;
            }
        }
        //sends data from Arduino to a given email
        private void btn_Send_email_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort.IsOpen)
                {
                    //will send an email containing what is checked
                    if (checkbx_wtrlvl.Checked == true)
                    {
                        serialPort.Write("w");
                    }
                    if (checkbx_sndlvl.Checked == true)
                    {
                        serialPort.Write("s");
                    }
                    if (checkbx_threshold.Checked == true)
                    {
                        serialPort.Write("c");
                        serialPort.Write(threshold_txtBox.Text);
                    }
                    Thread.Sleep(2000);
                    data = serialPort.ReadExisting();
                    SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                    client.EnableSsl = true;
                    client.Timeout = 10000;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.Credentials = new System.Net.NetworkCredential(txtBox_User.Text, txtBox_Pass.Text);
                    MailMessage msg = new MailMessage(txtBox_To.Text, txtBox_User.Text);
                    msg.Subject = DateTime.Now.ToString("HH:mm:ss");
                    msg.Body = data;
                    client.Send(msg);
                    Thread.Sleep(2000);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK);
            }
            
        }
 
        private void checkbx_threshold_CheckedChanged(object sender, EventArgs e)
        {
            if (checkbx_threshold.Checked == true)
                threshold_txtBox.Enabled = true;//allows user to enter a new threshold if checkbox is checked
            if (checkbx_threshold.Checked == false)
                threshold_txtBox.Enabled = false;//disables if checkbox becomes unchecked
        }
 
        private void btn_submitCredentials_Click(object sender, EventArgs e)
        {
            //disables email info text boxes so they won't be changed while user leaves program running
            txtBox_User.Enabled = false;
            txtBox_Pass.Enabled = false;
            txtBox_To.Enabled = false;
        }
        //button reopens text boxes so they can be changed
        private void btn_reset_Click(object sender, EventArgs e)
        {
            txtBox_User.Enabled = true;
           txtBox_Pass.Enabled = true;
            txtBox_To.Enabled = true;
        }
    }
}
