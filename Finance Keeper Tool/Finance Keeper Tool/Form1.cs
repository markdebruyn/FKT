using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Finance_Keeper_Tool
{
    public partial class Form1 : Form
    {
        DBConnection dBConnection;
        public Form1()
        {
            dBConnection = DBConnection.Instance();
            dBConnection.DatabaseName = "financialdb";
            InitializeComponent();
        }

        private void Login_btn_Click(object sender, EventArgs e)
        {
            if (dBConnection.Connect())
            {
                //dBConnection.ReciveData("SELECT * FROM financialdb.users;");
                bool test = dBConnection.Login(Username_txb.Text, Password_txb.Text);
                if (test)
                {
                    // open new screen
                    MessageBox.Show("Login Works");
                }

            }
            
        }
    }
}
