using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        /* Name: Alexis Rankin
         * Date: December 2nd 2024
         * This program rolls one dice or calculates mark stats.
         * Link to your repo in GitHub: https://github.com/frostydog11/IntroProgrammingLab5
         * */

        //class-level random object
        Random rand = new Random();

        private void Form1_Load(object sender, EventArgs e)
        {
            //select one roll radiobutton
            radOneRoll.Checked = true;

            //add your name to end of form title
            this.Text = Text + "Alexis Rankin";
            
        } // end form load

        private void btnClear_Click(object sender, EventArgs e)
        {
            //call the function
            ClearOneRoll();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            //call the function
            ClearStats();
            
        }

        private void btnRollDice_Click(object sender, EventArgs e)
        {
            int dice1, dice2;
            //call ftn RollDice, placing returned number into integers
            dice1 = RollDice();
            dice2 = RollDice();

            //place integers into labels
            lblDice1.Text = Convert.ToString(dice1);
            lblDice2.Text = Convert.ToString(dice2);
            
            // call ftn GetName sending total and returning name
            int total = dice1 + dice2;
            string name = GetName(ref total);


            //display name in label
            lblRollName.Text = Convert.ToString(name);
            
        }


        private void ClearOneRoll()
        {
            lblDice1.Text = string.Empty;
            lblDice2.Text = string.Empty;
            lblRollName.Text = string.Empty;
        }

        private void ClearStats()
        {
            nudNumber.Value = nudNumber.Minimum;
            lblAverage.Text = string.Empty;
            lblFail.Text = string.Empty;
            lblPass.Text = string.Empty;
            lstMarks.Items.Clear();
        }
        /* Name: ClearStats
        *  Sent: nothing
        *  Return: nothing
        *  Reset nud to minimum value, chkbox unselected, 
        *  clear labels and listbox */

        private int RollDice()
        {
            int dice = rand.Next(1, 6);
            return dice;
        }
        /* Name: RollDice
        * Sent: nothing
        * Return: integer (1-6)
        * Simulates rolling one dice */

        private string GetName(ref int total)
        {
            string name = "";

            switch(total)
            {
                case 2:
                    name = "Snake Eyes";
                    break;
                case 3:
                    name = "Little Joe";
                    break;
                case 5:
                    name = "Fever";
                    break;
                case 7:
                    name = "Most Common";
                    break;
                case 9:
                    name = "Center Field";
                    break;
                case 11:
                    name = "Yo-leven";
                    break;
                case 12:
                    name = "boxcars";
                    break;
                default:
                    name = "No Special Name";
                    break;
            }


            return name;
        }
        /* Name: GetName
        * Sent: 1 integer (total of dice1 and dice2) 
        * Return: string (name associated with total) 
        * Finds the name of dice roll based on total.
        * Use a switch statement with one return only
        * Names: 2 = Snake Eyes
        *        3 = Litle Joe
        *        5 = Fever
        *        7 = Most Common
        *        9 = Center Field
        *        11 = Yo-leven
        *        12 = Boxcars
        * Anything else = No special name*/

        private void btnSwapNumbers_Click(object sender, EventArgs e)
        {

            string numOne = lblDice1.Text;
            string numTwo = lblDice2.Text;
            bool dataPresent = false;
            //call ftn DataPresent twice sending string returning boolean
            dataPresent = DataPresent(ref numOne);
            dataPresent = DataPresent(ref numTwo);


            //if data present in both labels, call SwapData sending both strings
            if (dataPresent == true)
            {
                SwapData(ref numOne, ref numTwo);
            }

            //put data back into labels

            //if data not present in either label display error msg
            if (dataPresent == false)
            {
                MessageBox.Show("No data present");
            }
        }


        private bool DataPresent(ref string num)
        {
            bool dataPresent = false;

            if (num != string.Empty)
            {
                dataPresent = true;
            }

            else
                dataPresent = false;

            return dataPresent;
        }
        /* Name: DataPresent
        * Sent: string
        * Return: bool (true if data, false if not) 
        * See if string is empty or not*/

        private void SwapData(ref string numOne, ref string numTwo)
        {
            lblDice1.Text = numTwo;
            lblDice2.Text = numOne;
        }
        /* Name: SwapData
        * Sent: 2 strings
        * Return: none 
        * Swaps the memory locations of two strings*/

        private void btnGenerate_Click(object sender, EventArgs e)
        {

            //declare variables and array
            int index = (int)nudNumber.Value;
            int[] marks = new int[index];
            int i = 0;
            int passes = 0;
            int fails = 0;
            double average;

            //check if seed value
            if (chkSeed.Checked == true)
            {
                Random rand = new Random(1000);
            }

            //fill array using random number
            lstMarks.Items.Clear();
            while(i < index)
            {
                marks[i] = rand.Next(40, 101);
                lstMarks.Items.Add(marks[i]);

                i++;
            }

            //call CalcStats sending and returning data
            average = CalcStats(ref marks, ref passes, ref fails);

            //display data sent back in labels - average, pass and fail
            // Format average always showing 2 decimal places 
            lblPass.Text = passes.ToString();
            lblFail.Text = fails.ToString();
            lblAverage.Text = average.ToString("f2");

        } // end Generate click

        private double CalcStats(ref int[] marks, ref int passes, ref int fails)
        {
            double average;
            const int PASSING = 60;
            int sum = 0;
            int index = marks.Length;

            foreach(int i in marks)
            {
                if (i >= PASSING)
                    passes++;
                else
                    fails++;

                sum = sum + i;
            }

            average = sum / index;

            return average;

        /* Name: CalcStats
        * Sent: array and 2 integers
        * Return: average (double) 
        * Run a foreach loop through the array.
        * Passmark is 60%
        * Calculate average and count how many marks pass and fail
        * The pass and fail values must also get returned for display*/
        }

        private void chkSeed_CheckedChanged(object sender, EventArgs e)
        {
            DialogResult selection = MessageBox.Show("Are you sure you want to use a seed value?", "Confirm Seed Value", MessageBoxButtons.YesNo);
            if (selection == DialogResult.No)
            {
                chkSeed.Checked = false;
            }
        }

        private void radOneRoll_CheckedChanged(object sender, EventArgs e)
        {
            if (radOneRoll.Checked == true)
            {
                ClearStats();
                grpMarkStats.Hide();
                grpOneRoll.Show();
            }

            else if (radOneRoll.Checked == false)
            {
                ClearOneRoll();
                grpMarkStats.Show();
                grpOneRoll.Hide();
            }

        }
    }
}
