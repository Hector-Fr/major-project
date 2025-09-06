using System;

namespace Major_Project___Productivity_App___Hector_F.Pages
{
    class ProgressPage: Page
    {
        public HabitsPage habitsPage;

        Label scoreToday;
        Label todayMotivation;
        Label averageScore;
        Label weekMotivation;

        public ProgressPage(App mainForm, string pageName, Button menuButton) : base(mainForm, pageName, menuButton) { }

        public override void Create(string pageName)
        {
            base.Create(pageName);

            CreateProgressPage();
        }

        private void CreateProgressPage() 
        {
            scoreToday = new Label();
            scoreToday.Font = new Font(mainForm.Font.FontFamily, 18);
            scoreToday.AutoSize = true;
            scoreToday.Location = new Point(400, 100);
            scoreToday.ForeColor = Color.White;
            scoreToday.TextAlign = ContentAlignment.MiddleCenter;
            pagePanel.Controls.Add(scoreToday);

            todayMotivation = new Label();
            todayMotivation.Font = new Font(mainForm.Font.FontFamily, 12);
            todayMotivation.AutoSize = true;
            todayMotivation.Location = new Point(400, 150);
            todayMotivation.ForeColor = Color.White;
            todayMotivation.TextAlign = ContentAlignment.MiddleCenter;
            todayMotivation.Size = new Size(300, 100);
            pagePanel.Controls.Add(todayMotivation);

            averageScore = new Label();
            averageScore.Font = new Font(mainForm.Font.FontFamily, 18);
            averageScore.AutoSize = true;
            averageScore.Location = new Point(200, 250);
            averageScore.ForeColor = Color.White;
            averageScore.TextAlign = ContentAlignment.MiddleCenter;
            pagePanel.Controls.Add(averageScore);

            weekMotivation = new Label();
            weekMotivation.Font = new Font(mainForm.Font.FontFamily, 12);
            weekMotivation.AutoSize = true;
            weekMotivation.Location = new Point(200, 300);
            weekMotivation.ForeColor = Color.White;
            weekMotivation.TextAlign = ContentAlignment.MiddleCenter;
            weekMotivation.Size = new Size(300, 100);
            pagePanel.Controls.Add(weekMotivation);
        }

        public override void OnPageActivated()
        {
            base.OnPageActivated();
            DisplayScores();
        }

        private void DisplayScores()
        {
            // Retrieve the habits data from the habits page when the user goes to the progress page
            bool[,] habitData = habitsPage.GetHabitData();

            if (habitData == null) return;
            double[] averageScores = GetScores(habitData);

            double averageScoreOver5Days = 0;

            for (int i = 0; i < averageScores.Length; i++)
            {
                averageScoreOver5Days += averageScores[i];
            }

            averageScoreOver5Days /= 5;


            scoreToday.Text = "Today's score: " + averageScores[0].ToString("F2") + "%";
            averageScore.Text = "Average score over the past five days: " + averageScoreOver5Days.ToString("F2") + "%";

            if (averageScores[0] == 0)
            {
                todayMotivation.Text = "It's time to take that first step to seize the day!";
            }
            else if (averageScores[0] <= 33.33)
            {
                todayMotivation.Text = "You've made a great start on today! \nGreat things come from small steps";
            }
            else if (averageScores[0] <= 66.66)
            {
                todayMotivation.Text = "You've already won! Look how far you have come.";
            }
            else if (averageScores[0] < 100)
            {
                todayMotivation.Text = "Almost there!";
            }
            else
            {
                todayMotivation.Text = "You did it! Now it's time to relax. You've earned it!";
            }


            if (averageScoreOver5Days == 0)
            {
                weekMotivation.Text = "It's a new week. Time to start fresh!";
            }
            else if (averageScoreOver5Days <= 33.33)
            {
                weekMotivation.Text = "Consistency is key!";
            }
            else if (averageScoreOver5Days <= 66.66)
            {
                weekMotivation.Text = "Let's push through to the end of the week!";
            }
            else if (averageScoreOver5Days < 100)
            {
                weekMotivation.Text = "You're acing this week!";
            }
            else
            {
                weekMotivation.Text = "You did it! Another week smashed.";
            }
        }

        private double[] GetScores(bool[,] habitData)
        {
            // Create an array of the average scores for each day
            double[] averageScores = new double[habitData.GetLength(1)];

            
            for (int day = 0; day < averageScores.Length; day++)
            {
                double numChecked = 0;

                for (int habit = 0; habit < (double)habitData.GetLength(1); habit++)
                {
                    if (habitData[day, habit] == true) numChecked++;
                }

                double scorePercentage = (numChecked / habitData.GetLength(1) * 100);
                averageScores[day] = scorePercentage;
            }

            return averageScores;
        }
    }
}
