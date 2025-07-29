
namespace Major_Project___Productivity_App___Hector_F
{
    class HabitsPage : Page
    {
        // The max number of days of habits that the habit grid (rows) should show at one time
        int numOfDaysToShow = 5;
        // The number of habits there are (columns). The user can add more
        int numOfHabits = 5;

        int yAdjust = 20;
        Size habitsGridSize = new Size(1000, 800);

        public HabitsPage(App mainForm, string pageName, Button menuButton) : base(mainForm, pageName, menuButton) { }

        public override void Create(string pageName)
        {
            base.Create(pageName);

            CreateHabitsGrid();
        }

        private void CreateHabitsGrid()
        {
            // The habits grid is a table including the rows for the days, the columns for the habits, including checkboxes, buttons, text, etc. in the grid as well
            TableLayoutPanel habitsGrid = new TableLayoutPanel();
            habitsGrid.RowCount = numOfDaysToShow;
            habitsGrid.ColumnCount = numOfHabits;
            habitsGrid.AutoSize = true;
            habitsGrid.BackColor = mainForm.buttonColour;//mainForm.pageColour;
            habitsGrid.Size = habitsGridSize;
            habitsGrid.Location = new Point(pagePanel.Width / 2 - habitsGrid.Width / 2, pagePanel.Height / 2 - habitsGrid.Height / 2 - yAdjust);
            pagePanel.Controls.Add(habitsGrid);
            
            for (int i = 0; i < numOfDaysToShow * numOfHabits; i++)
            {
                CheckBox habitCheckbox = new CheckBox();
                
                habitsGrid.Controls.Add(habitCheckbox);
            }
        }

        private void OnButtonClick  (object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackColor = Color.Black;
        }

        private void UpdateHabitsGrid()
        {
            
        }
    }
}
