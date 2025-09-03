
namespace Major_Project___Productivity_App___Hector_F
{
    class GoalsPage : Page
    {
        string goalsFilePath = "C:\\Progamming Projects\\School\\Major Project\\major-project\\Major Project - Productivity App - Hector F\\Major Project - Productivity App - Hector F\\bin\\Debug\\Goals.txt";

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
            Label lblMonthlyGoals = new Label();
            lblMonthlyGoals.Text = "Monthly Goals";
            lblMonthlyGoals.Location = new Point(100, 50);
            lblMonthlyGoals.AutoSize = true;
            lblMonthlyGoals.ForeColor = Color.White;
            lblMonthlyGoals.TextAlign = ContentAlignment.MiddleCenter;
            pagePanel.Controls.Add(lblMonthlyGoals);

            // Create the textbox for the monthly goals
            txtbxMonthlyGoals = new TextBox();
            txtbxMonthlyGoals.Size = new Size(400, 500);
            txtbxMonthlyGoals.Location = new Point(100, 100);
            txtbxMonthlyGoals.Multiline = true;
            pagePanel.Controls.Add(txtbxMonthlyGoals);

            // Create the label for the yearly goals textbox
            Label lblYearlyGoals = new Label();
            lblYearlyGoals.Text = "Yearly Goals";
            lblYearlyGoals.Location = new Point(600, 50);
            lblYearlyGoals.AutoSize = true;
            lblYearlyGoals.ForeColor = Color.White;
            lblYearlyGoals.TextAlign = ContentAlignment.MiddleCenter;
            pagePanel.Controls.Add(lblYearlyGoals);

            // Create the textbox for the yearly goals
            txtbxYearlyGoals = new TextBox();
            txtbxYearlyGoals.Size = new Size(400, 500);
            txtbxYearlyGoals.Location = new Point(600, 100);
            txtbxYearlyGoals.Multiline = true;
            pagePanel.Controls.Add(txtbxYearlyGoals);

            // Read the saved montly and yearly goals from the 'goals' file and add it to the textboxes
            string fileContent = File.ReadAllText(goalsFilePath);
            string[] splitFileContent = fileContent.Split("\\");
            txtbxMonthlyGoals.Text = splitFileContent[0];
            txtbxYearlyGoals.Text = splitFileContent[1];
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
                writer.WriteLine(txtbxMonthlyGoals.Text + "\\" + txtbxYearlyGoals.Text);
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
