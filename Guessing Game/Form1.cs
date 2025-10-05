using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Guessing_Game
{
    public partial class Guessing_Game : Form
    {
        Random randGen = new Random();
        int userInput;
        int secretNumber;
        int guessCount = 1;
        public Guessing_Game()
        {
            InitializeComponent();
            secretNumber = randGen.Next(1, 101);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private async void GuessButton_Click(object sender, EventArgs e)
        {
            
            testDisp.Text = secretNumber.ToString();
            SoundPlayer error = new SoundPlayer(Properties.Resources.errorSfx);
            SoundPlayer ding = new SoundPlayer(Properties.Resources.dingSfx);
            SoundPlayer nope = new SoundPlayer(Properties.Resources.nopeSfx);
            // Parse user input from text box


            if (int.TryParse(GuessBox.Text, out userInput) && userInput >= 1 && userInput <= 100)
            {
                //create difference between the two numbers for hot cold comparison
                int diff = (secretNumber - userInput);

                guessCount++;

                //call function for response
                if (diff < 0)
                {
                    feedback.Text = GetHint(diff) + ", Too High!";
                    nope.Play();

                }
                else if (diff > 0)
                {
                    feedback.Text = GetHint(diff) + ", Too Low";
                    nope.Play();

                }
                else
                {
                    feedback.Text = GetHint(diff);
                    ding.Play();
                    resetButton.Text = GetHint(diff);
                }


            }
            else
            {
                feedback.ForeColor = Color.Red;
                feedback.Text = "Invalid Input";
                error.Play();
                await Task.Delay(1200);
                feedback.Text = "";
                feedback.ForeColor = Color.White;
                return;
            }
            
            if (userInput == secretNumber)
            {
                resetButton.Visible = true;
            }


        }

        private void GuessBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        //function for finding the most appropriate response for user input compared with the number
        private string GetHint(int difference)
        {
            difference = Math.Abs(difference);
            if (difference >= 50) return "Freezing";
            if (difference >= 25) return "Cold";
            if (difference >= 15) return "Cool";
            if (difference >= 10) return "Warm";
            if (difference >= 5) return "Hot";
            if (difference >= 1) return "Boiling";
            return "Correct! You guessed the number!";
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            resetButton.Visible = false;
            secretNumber = randGen.Next(1, 101);
        }
    }
}
