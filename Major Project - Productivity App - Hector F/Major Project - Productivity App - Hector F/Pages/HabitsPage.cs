
using System.Diagnostics;

namespace Major_Project___Productivity_App___Hector_F
{
    class HabitsPage : Page
    {
        // The max number of days of habits that the habit grid (rows) should show at one time
        int numOfDaysToShow = 5;
        // The number of habits there are (columns). The user can add more
        int numOfHabits = 5;

        int yAdjust = 20;
        Size habitsGridSize = new Size(800, 400);

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
            habitsGrid.RowCount = numOfDaysToShow + 1;
            habitsGrid.ColumnCount = numOfHabits + 1;
            
            habitsGrid.BackColor = mainForm.buttonColour;//mainForm.pageColour;
            habitsGrid.Size = habitsGridSize;
            habitsGrid.Location = new Point(pagePanel.Width / 2 - habitsGrid.Width / 2, pagePanel.Height / 2 - habitsGrid.Height / 2 - yAdjust);
            pagePanel.Controls.Add(habitsGrid);
            
            for (int y = 0; y < habitsGrid.RowCount; y++)
            {
                for (int x = 0; x < habitsGrid.ColumnCount; x++)
                {
                    // If the iterator loop is currently at the first column of any row
                    if (x == 0)
                    {
                        if (y == 0)
                        {
                            Label day = new Label();
                            day.Text = "Day";
                            day.ForeColor = Color.White;
                            day.TextAlign = ContentAlignment.MiddleCenter;
                            day.Margin = new Padding(0, 0, 0, 10);
                            habitsGrid.Controls.Add(day);
                        }
                        else
                        {
                            // Add the day column
                            Label dayLabel = new Label();
                            dayLabel.Text = "Day " + y.ToString();
                            dayLabel.TextAlign = ContentAlignment.BottomCenter;
                            dayLabel.ForeColor = Color.White;
                            habitsGrid.Controls.Add(dayLabel);
                        }
                    }
                    else if (y == 0)
                    {
                        // Add header
                        Label habitLabel = new Label();
                        habitLabel.Text = "Habit " + x.ToString();
                        habitLabel.ForeColor = Color.White;
                        habitLabel.TextAlign = ContentAlignment.MiddleCenter;
                        habitsGrid.Controls.Add(habitLabel);
                    }
                    else
                    {
                        CheckBox habitCheckbox = new CheckBox();
                        habitCheckbox.AutoSize = false;
                        habitCheckbox.Size = new Size(40, 40);
                        habitCheckbox.CheckAlign = ContentAlignment.TopRight;
                        habitsGrid.Controls.Add(habitCheckbox); 
                    }        
                }            
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
