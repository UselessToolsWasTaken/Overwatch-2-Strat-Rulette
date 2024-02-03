using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace AdvancedStratRulette
{
    public partial class Form1 : Form
    {

        public string[] teamStrats;
        public string[] dpsStrats;
        public string[] supportStrats;
        public string[] tankStrats;
        //These are all the strat arrays that get populated on application load. 

        public bool groupStratRadio;
        public bool roleStratRadio;
        //Radial BUttons that determine if you're generating one strat for the whole group or one for each role separately.

        public int partySize;
        //Speaks for itself, not implemented yet

        public string saveFolder;
        //Currently not used, might remove it soon or find a way to use it, it's a remnant of a worse implementation of the file system


        public Form1()
        {

            InitializeComponent();


            try //Checks for any issues when creating file paths to the executables directory.
            {
                string executablePath = Application.ExecutablePath;

                string teamFilePath = Path.Combine(Path.GetDirectoryName(executablePath), "group_strats.txt");
                string dpsFilePath = Path.Combine(Path.GetDirectoryName(executablePath), "dps_strats.txt");
                string supportFilePath = Path.Combine(Path.GetDirectoryName(executablePath), "support_strats.txt");
                string tankFilePath = Path.Combine(Path.GetDirectoryName(executablePath), "tank_strats.txt");
                // After setting the path to the executable, I add the specific .txt files to it to be able to read from then in the next could of if statements

                if (File.Exists(teamFilePath))
                {
                    teamStrats = File.ReadAllLines(teamFilePath);                                   //Roses are Red
                }                                                                                   //Violets are Blue
                if (File.Exists(dpsFilePath))                                                       //If statements suck
                {                                                                                   //Should find a new way to do (this part because I'm pretty sure there is a more efficient way for this)
                    dpsStrats = File.ReadAllLines(dpsFilePath);
                }
                if (File.Exists(supportFilePath))
                {
                    supportStrats = File.ReadAllLines(supportFilePath);
                }
                if (File.Exists(tankFilePath))
                {
                    tankStrats = File.ReadAllLines(tankFilePath);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("There are no correct files in directory", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Error); //Does this even work? Wont this just crash out the app?
            }
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            //Huh? Why is this here, now i can't remove it cause my whole Form.1CS will get fucked
        }

        private void groupStrats_CheckedChanged(object sender, EventArgs e)
        {
            if (groupStrats.Checked) 
            {
                groupStratRadio = true;                                     //This is done this way because i don't know a better way to set these
                roleStratRadio = false;                                     //If you do, please call 911 and tell them about it because I don't care
            }                                                               //I'll just leave this as is, it's working, why break it
            else
            {
                groupStratRadio = false;
                roleStratRadio = true;
            }
        }

        private void roleStrats_CheckedChanged(object sender, EventArgs e)
        {
            if(roleStrats.Checked)
            {
                groupStratRadio = false;
                roleStratRadio = true;
            }
            else                                                        //You thought you were safe? LOL
            {
                groupStratRadio = true;
                roleStratRadio = false;
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //Once again, this shouldn't be here but I'm a stupid hoe and double clicked a textbox
        }
        private void playerCount_SelectedIndexChanged(object sender, EventArgs e) //This takes the ListBox and Adds +1 to the index of the selection to give the party size
        {
            if(playerCount.SelectedIndex != -1)
            {
                partySize = playerCount.SelectedIndex + 1;
            }
        }

        private void genButton_Click(object sender, EventArgs e)
        {
            if (groupStratRadio && !roleStratRadio)
            {
                Random random = new Random();
                int groupStratSelection = random.Next(teamStrats.Length);
                dpsRoleStrat.Text = teamStrats[groupStratSelection];
            }
            else if (roleStratRadio && !groupStratRadio)
            {
                Random random = new Random();
                int tankStratSelection = random.Next(tankStrats.Length);                //This will randomly generate strats based on the selection on the RadioButtons down below
                int dpsStratSelection = random.Next(dpsStrats.Length);                  //Same applies to the above if statement
                int supportStratSelection = random.Next(supportStrats.Length);          //Generally speaking this works, don't touch it.

                tankRoleStrat.Text = tankStrats[tankStratSelection];
                dpsRoleStrat.Text = dpsStrats[dpsStratSelection];
                suppRoleStrat.Text = supportStrats[supportStratSelection];
            }
        }


        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {
            //I added this for some reason But I'm not sure. I'll probably find a use for this later
        }

        private void typesOfStratsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("There are two types of strats, Group and Role. \n1. Group Strats involve the whole group in one strat\n2. Role Strats apply strats to specific roles in your party", "Strat Types", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        //Above and below are just some help menus on the tool strip
        private void partySizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("By selecting your party correctly, the strats will adapt to make sure you get the most \nout of the strats presented", "Party Size explanation", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void addRoleStratToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using(Form3 roleForm = new Form3())
            {
                DialogResult result = roleForm.ShowDialog();
                if(result != DialogResult.OK)
                {
                    string roleText = roleForm.roleStratEnter;
                    string dpsPath = roleForm.dpsFilePath;
                    string tankPath = roleForm.tankFilePath;
                    string supportPath = roleForm.supportFilePath;

                    switch (roleForm.roleSelector)
                    {
                        case 1:
                            MessageBox.Show("Entered text: " + roleText + "\nwas added to: " + dpsPath, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        case 2:
                            MessageBox.Show("Entered text: " + roleText + "\nwas added to: " + supportPath, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        case 3:
                            MessageBox.Show("Entered text: " + roleText + "\nwas added to: " + tankPath, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                    }
                }
            }
        }

        private void addGroupStratToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using(Form2 groupForm = new Form2())
            {
                DialogResult result = groupForm.ShowDialog();
                if(result == DialogResult.OK)
                {
                    string groupText = groupForm.groupStratEnter;
                    string textPath = groupForm.teamFilePath;
                    MessageBox.Show("Entered Text: " +  groupText + "\nwas added to: " + textPath,"Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void addingYourOwnStratToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You can add your own Group and Role strats\nby pressing File -> Add Group/Role Strat\n *Note: You will only add strats to full 5 player strats this way to add strats that are party size specific\nyou will have to manualy edit the .txt files in the Executable folder.", "How to add Strats", MessageBoxButtons.OK, MessageBoxIcon.Information);
        } //Another help menu
    }
}