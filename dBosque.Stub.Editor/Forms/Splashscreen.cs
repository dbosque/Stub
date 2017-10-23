using Appccelerate.EventBroker;
using Appccelerate.EventBroker.Handlers;
using System;
using System.ComponentModel;
using System.Threading;

namespace dBosque.Stub.Editor.Forms
{
    public partial class Splashscreen : MetroFramework.Forms.MetroForm
    {
        private Thread t = new Thread(new ThreadStart(Program.Start));

        [EventSubscription("topic://ApplicationLoaded", typeof(OnUserInterface))]
        public void OnApplicationLoaded(object sender, EventArgs e)
        {
            Hide();
        }

        [EventSubscription("topic://ApplicationClosed", typeof(OnUserInterface))]
        public void OnApplicationClosed(object sender, EventArgs e)
        {
            Close();
        }

        public Splashscreen(IEventBroker broker)
            : this()
        {
            broker.Register(this);
        }

        [DesignOnly(true)]
        public Splashscreen()
        {
            t.SetApartmentState(ApartmentState.STA);
            Load += Splashscreen_Load;           
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

        }

        private void Splashscreen_Load(object sender, EventArgs e)
        {
            t.Start();
        }


        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect, // x-coordinate of upper-left corner
            int nTopRect, // y-coordinate of upper-left corner
            int nRightRect, // x-coordinate of lower-right corner
            int nBottomRect, // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
        );
    }
}
