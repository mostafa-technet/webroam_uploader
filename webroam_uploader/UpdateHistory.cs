using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace webroam_uploader
{
    public partial class UpdateHistory : Form
    {
        string m_UpdateID = "";
        public UpdateHistory(string UpdateID)
            : this()
        {
            m_UpdateID = UpdateID;
        }
        public UpdateHistory()
        {
            InitializeComponent();
        }

        private void UpdateHistory_Load(object sender, EventArgs e)
        {
            if (File.Exists($"output\\{m_UpdateID}\\profile.save"))
            {
                string[] text = File.ReadAllLines($"output\\{m_UpdateID}\\profile.save");
                //textBox1.Text = text[0];
                //textBox2.Text = text[2];
                //textBox3.Text = text[4];
                textBox4.Text = text[6];
                textBox5.Text = text[8];
                textBox6.Text = text[10];
                textBox7.Text = text[12];
                textBox8.Text = text[14];             
            }
        }
    }
}
