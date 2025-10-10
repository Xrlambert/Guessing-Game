﻿using System;
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
        Random randGen = new Random(); // Random number generator
        int userInput;                 // Stores user's guess
        int secretNumber;              // Secret number to guess
        int guessCount;                // Number of guesses made

        public Guessing_Game()
        {
            InitializeComponent();
            secretNumber = randGen.Next(1, 101); // Generate secret number between 1–100
        }

        private void label1_Click(object sender, EventArgs e) { }

        private async void GuessButton_Click(object sender, EventArgs e)
        {
            testDisp.Text = secretNumber.ToString(); // Display secret number (for testing)

            SoundPlayer error = new SoundPlayer(Properties.Resources.errorSfx);
            SoundPlayer ding = new SoundPlayer(Properties.Resources.dingSfx);
            SoundPlayer nope = new SoundPlayer(Properties.Resources.nopeSfx);

            if (GuessBox.Text.Contains("dev")) testDisp.Visible = !testDisp.Visible; // Dev mode toggle

            // Validate input and ensure it's within range
            if (int.TryParse(GuessBox.Text, out userInput) && userInput >= 1 && userInput <= 100)
            {
                int diff = (secretNumber - userInput); // Calculate difference for hint logic

                guessCount++; // Increment guess count
                countBox.Text = "Guess #:" + guessCount;
                countBox.Visible = true;
                feedback.Visible = true;

                // Provide feedback based on guess direction
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
                    feedback.Text = GetHint(diff); // Correct guess
                    ding.Play();
                }
            }
            else
            {
                // Handle invalid input
                feedback.ForeColor = Color.Red;
                feedback.Text = "Invalid Input";
                error.Play();
                await Task.Delay(1200);
                feedback.Text = "";
                feedback.ForeColor = Color.White;
                return;
            }

            if (userInput == secretNumber) resetButton.Visible = true; // Show reset if correct
        }

        private void GuessBox_TextChanged(object sender, EventArgs e) { }

        private void label3_Click(object sender, EventArgs e) { }

        // Generate hint based on proximity to secret number
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
            // Reset game state
            resetButton.Visible = false;
            secretNumber = randGen.Next(1, 101);
            GuessBox.Text = "";
            guessCount = 0;
            countBox.Visible = false;
            feedback.Visible = false;
        }

        private void countBox_Click(object sender, EventArgs e) { }
    }
}
