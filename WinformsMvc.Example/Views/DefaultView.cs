using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinformsMvc.Example.Controllers;

namespace WinformsMvc.Example.Views
{
    public partial class DefaultView : Form, IView
    {
        public DefaultView()
        {
            InitializeComponent();
        }

        public Form Form
        {
            get
            {
                return this;
            }
        }

        public string Title
        {
            get
            {
                return Text;
            }
            set
            {
                Text = value;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AppManager.Instance.Load<LoginController>();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AppManager.Instance.Load<AccountController>();
        }
    }
}
