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

namespace AdvancedStratRulette
{
    public partial class Form2 : Form
    {
        public string groupStratEnter {  get; private set; }

        public int roleSelector;

        public string teamFilePath;
    /*
        string dpsFilePath;
        string supportFilePath;
        string tankFilePath;
    */

        string executablePath;

        public Form2()
        {
            InitializeComponent();

            executablePath = Application.ExecutablePath;

            teamFilePath = Path.Combine(Path.GetDirectoryName(executablePath), "group_strats.txt");
         /* 
            dpsFilePath = Path.Combine(Path.GetDirectoryName(executablePath), "dps_strats.txt");
            supportFilePath = Path.Combine(Path.GetDirectoryName(executablePath), "support_strats.txt");
            tankFilePath = Path.Combine(Path.GetDirectoryName(executablePath), "tank_strats.txt"); 
         */
        }

        private void button1_Click(object sender, EventArgs e)
        {
            groupStratEnter = textBox1.Text;
            File.AppendAllText(teamFilePath, groupStratEnter);
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
