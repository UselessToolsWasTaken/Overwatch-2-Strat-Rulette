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
using System.Windows.Forms.VisualStyles;

namespace AdvancedStratRulette
{
    public partial class Form3 : Form
    {

        public int roleSelector;

        public string dpsFilePath;
        public string supportFilePath;
        public string tankFilePath;
        public string executablePath;

        public string roleStratEnter {  get; private set; }

        public Form3()
        {
            InitializeComponent();

            executablePath = Application.ExecutablePath;

            //string teamFilePath = Path.Combine(Path.GetDirectoryName(executablePath), "group_strats.txt");
            dpsFilePath = Path.Combine(Path.GetDirectoryName(executablePath), "dps_strats.txt");
            supportFilePath = Path.Combine(Path.GetDirectoryName(executablePath), "support_strats.txt");
            tankFilePath = Path.Combine(Path.GetDirectoryName(executablePath), "tank_strats.txt");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            roleStratEnter = textBox1.Text;
            try
            {
                switch (roleSelector)
                {
                    case 1:
                        File.AppendAllText(dpsFilePath, roleStratEnter);
                        break;
                    case 2:
                        File.AppendAllText(supportFilePath, roleStratEnter);
                        break;
                    case 3:
                        File.AppendAllText(tankFilePath, roleStratEnter);
                        break;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"error writing to file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            this.Close(); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addTankRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (addTankRadio.Checked)
            {
                roleSelector = 1;
            }
        }

        private void addDpsRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (addDpsRadio.Checked)
            {
                roleSelector = 2;
            }
        }

        private void addSupportRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (addSupportRadio.Checked)
            {
                roleSelector = 3;
            }
        }
    }
}
