
namespace Major_Project___Productivity_App___Hector_F
{
    class GoalsPage : Page
    {
        string goalsFilePath = "C:\\Progamming Projects\\School\\Major Project\\major-project\\Major Project - Productivity App - Hector F\\Major Project - Productivity App - Hector F\\bin\\Debug\\Goals.txt";

        TextBox txtbxWeeklyGoals;
        TextBox txtbxMonthlyGoals;
        TextBox txtbxYearlyGoals;

        public GoalsPage(App mainForm, string pageName, Button menuButton) : base(mainForm, pageName, menuButton) { }

        public override void Create(string pageName)
        {
            base.Create(pageName);

            mainForm.FormClosing += new FormClosingEventHandler(MainForm_FormClosing);

            CreateGoalsPage();
        }

        private void CreateGoalsPage()
        {
            // Create the label for the monthly goals textbox
            Label lblWeeklyGoals = new Label();
            lblWeeklyGoals.Text = "Weekly Goals";
            lblWeeklyGoals.Location = new Point(50, 50);
            lblWeeklyGoals.AutoSize = true;
            lblWeeklyGoals.ForeColor = Color.White;
            lblWeeklyGoals.TextAlign = ContentAlignment.MiddleCenter;
            pagePanel.Controls.Add(lblWeeklyGoals);

            // Create the textbox for the monthly goals
            txtbxWeeklyGoals = new TextBox();
            txtbxWeeklyGoals.Size = new Size(300, 500);
            txtbxWeeklyGoals.Location = new Point(50, 100);
            txtbxWeeklyGoals.Multiline = true;
            pagePanel.Controls.Add(txtbxWeeklyGoals);

            // Create the label for the monthly goals textbox
            Label lblMonthlyGoals = new Label();
            lblMonthlyGoals.Text = "Monthly Goals";
            lblMonthlyGoals.Location = new Point(400, 50);
            lblMonthlyGoals.AutoSize = true;
            lblMonthlyGoals.ForeColor = Color.White;
            lblMonthlyGoals.TextAlign = ContentAlignment.MiddleCenter;
            pagePanel.Controls.Add(lblMonthlyGoals);

            // Create the textbox for the monthly goals
            txtbxMonthlyGoals = new TextBox();
            txtbxMonthlyGoals.Size = new Size(300, 500);
            txtbxMonthlyGoals.Location = new Point(400, 100);
            txtbxMonthlyGoals.Multiline = true;
            pagePanel.Controls.Add(txtbxMonthlyGoals);

            // Create the label for the yearly goals textbox
            // Create the label for the yearly goals textbox
            Label lblYearlyGoals = new Label();
            lblYearlyGoals.Text = "Yearly Goals";
            lblYearlyGoals.Location = new Point(800, 50);
            lblYearlyGoals.AutoSize = true;
            lblYearlyGoals.ForeColor = Color.White;
            lblYearlyGoals.TextAlign = ContentAlignment.MiddleCenter;
            pagePanel.Controls.Add(lblYearlyGoals);

            // Create the textbox for the yearly goals
            txtbxYearlyGoals = new TextBox();
            txtbxYearlyGoals.Size = new Size(300, 500);
            txtbxYearlyGoals.Location = new Point(800, 100);
            txtbxYearlyGoals.Multiline = true;
            pagePanel.Controls.Add(txtbxYearlyGoals);

            // Read the saved montly and yearly goals from the 'goals' file and add it to the textboxes
            string fileContent = File.ReadAllText(goalsFilePath);
            if (!string.IsNullOrWhiteSpace(fileContent))
            {
                string[] splitFileContent = fileContent.Split("\\");
                txtbxWeeklyGoals.Text = splitFileContent[0];
                txtbxMonthlyGoals.Text = splitFileContent[1];
                txtbxYearlyGoals.Text = splitFileContent[2];
            }
        }

        /// <summary>
        /// Reads the goals textboxes to the 'goals' file
        /// </summary>
        private void SaveGoalsData()
        {
            // Open the file to write
            using (StreamWriter writer = new StreamWriter(goalsFilePath, false))
            {
                // Write the contents of the two textboxes to the file, and separate them to distinguish month/year goals for reading the file
                writer.WriteLine(txtbxWeeklyGoals.Text + "\\" + txtbxMonthlyGoals.Text + "\\" + txtbxYearlyGoals.Text);
            }
        }

        /// <summary>
        /// When the app closes, save the goals data to the file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveGoalsData();
        }
    }
}
