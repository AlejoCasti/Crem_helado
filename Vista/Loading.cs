using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Vista
{
    public partial class Loading : Form
    {
        public Loading()
        {
            InitializeComponent();
        }
        int Time = 0;
        Login Log = new Login();
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Time == 2)
            {
                Log.Show();
                timer1.Stop();
                this.Hide();
            }
            else
            {
                Time++;
            }
        }


    }
}
