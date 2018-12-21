using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpDelegates
{
    public delegate void GetInfo(object sender, string e);
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // Bring up our info form

            secondForm newForm = new secondForm();
            newForm.delegateProperty.OnInfoObtained += DelegateProperty_OnInfoObtained;
            newForm.Show();
        }

        private void DelegateProperty_OnInfoObtained(object sender, string e)
        {
            Console.WriteLine("Delegate string is " + e);
            
        }
    }
}
