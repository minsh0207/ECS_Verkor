using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestAPIServer
{
    public partial class RestApiServer : UserControl
    {
        #region [Class Vars]

        //private HttpListener _HttpListener;
        private string _Endpoint;
        #endregion

        #region [Properties]
        int _Port;
        [DisplayName("Port"), Description("Server Port"), Category("API Server Setting")]
        public int Port
        {
            get { return _Port; }
            set { _Port = value; }
        }
        #endregion
        public RestApiServer()
        {
            InitializeComponent();
            InitControl();
        }

        private void InitControl()
        {
            
        }
        public void Start()
        {
            try { 
                //StartStopButton_Click(this, null);
                if (!CRestServer._IsServerRunning)
                {
                    bool bRet = CRestServer.Start("https://localhost", Port.ToString());

                    // 하늘색 바탕 
                    lbEndPoint.BackColor = Color.SkyBlue;
                    lbEndPoint.ForeColor = Color.Black;

                    btStartStop.Text = "Stop";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void Stop()
        {
            try
            {
                if (CRestServer._IsServerRunning)
                {
                    CRestServer.Stop();

                    // 빨간색 바탕 
                    lbEndPoint.BackColor = Color.Red;
                    lbEndPoint.ForeColor = Color.White;

                    btStartStop.Text = "Start";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void StartStopButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (CRestServer._IsServerRunning)
                {
                    CRestServer.Stop();

                    // 빨간색 바탕 
                    lbEndPoint.BackColor = Color.Red;
                    lbEndPoint.ForeColor = Color.White;

                    btStartStop.Text = "Start";
                }
                else
                {
                    bool bRet = CRestServer.Start("https://localhost", Port.ToString());

                    // 하늘색 바탕 
                    lbEndPoint.BackColor = Color.SkyBlue;
                    lbEndPoint.ForeColor = Color.Black;

                    btStartStop.Text = "Stop";
                }
            } catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private string MyIP()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            foreach(IPAddress ip in host.AddressList)
            {
                if(ip.AddressFamily == AddressFamily.InterNetwork)
                    return ip.ToString();
            }

            return null;
        }

        private void RestApiServer_Load(object sender, EventArgs e)
        {

            _Endpoint = $"https://{MyIP()}:{Port}/";
            lbEndPoint.Text = _Endpoint;
            
            //StartStopButton_Click(sender, e);
        }

    }
}
