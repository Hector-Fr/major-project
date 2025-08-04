
using System.Resources;

namespace Major_Project___Productivity_App___Hector_F
{
    class HabitsPage : Page
    {
        // The max number of days of habits that the habit grid (rows) should show at one time
        int numOfDaysToShow = 5;
        // The number of habits there are (columns). The user can add more
        int numOfHabits = 5;

        TableLayoutPanel habitsGrid;
        int yAdjust = 20;
        Size habitsGridSize = new Size(1000, 600);
        Button btnAddHabit;

        public HabitsPage(App mainForm, string pageName, Button menuButton) : base(mainForm, pageName, menuButton) { }

        public override void Create(string pageName)
        {
            base.Create(pageName);

            CreateHabitsGrid();

            
        }

        private void CreateHabitsGrid()
        {
            // The habits grid is a table including the rows for the days, the columns for the habits, including checkboxes, buttons, text, etc. in the grid as well
            habitsGrid = new TableLayoutPanel();
            habitsGrid.RowCount = numOfDaysToShow + 2;
            habitsGrid.ColumnCount = numOfHabits + 2;
            habitsGrid.BackColor = mainForm.buttonColour;//mainForm.pageColour;
            habitsGrid.Size = habitsGridSize;
            habitsGrid.Location = new Point(pagePanel.Width / 2 - habitsGrid.Width / 2, pagePanel.Height / 2 - habitsGrid.Height / 2 - yAdjust);
            habitsGrid.AutoScroll = true;
            habitsGrid.GrowStyle = TableLayoutPanelGrowStyle.AddColumns;
            pagePanel.Controls.Add(habitsGrid);
            
            for (int y = 0; y < habitsGrid.RowCount; y++)
            {
                for (int x = 0; x < habitsGrid.ColumnCount - 1; x++)
                {
                    // In the first column
                    if (x == 0)
                    {
                        // The top-right-most cell is the first column header: day header
                        if (y == 0)
                        {
                            // Create day header label
                            Label day = new Label();
                            day.Text = "Day";
                            day.AutoSize = true;
                            day.ForeColor = Color.White;
                            day.TextAlign = ContentAlignment.MiddleCenter;
                            day.Margin = new Padding(0, 0, 0, 10);
                            habitsGrid.Controls.Add(day, 0, 0);
                        }
                        else
                        {
                            if (y != 1)
                            {
                                // Add the day column (today, yesterday, day before, etc.)
                                Label dayLabel = new Label();
                                dayLabel.Text = "Day " + (y - 1).ToString();
                                dayLabel.AutoSize = true;
                                dayLabel.TextAlign = ContentAlignment.BottomCenter;
                                dayLabel.ForeColor = Color.White;
                                habitsGrid.Controls.Add(dayLabel, 0, y);
                            }
                            
                        }
                    }
                    // In the first row
                    else if (y == 0)
                    {
                        // If the iterator is currently in the first row, create each habit as a header
                        Label habitLabel = new Label();
                        habitLabel.Text = "Habit " + x.ToString();
                        habitLabel.ForeColor = Color.White;
                        habitLabel.TextAlign = ContentAlignment.MiddleCenter;
                        habitsGrid.Controls.Add(habitLabel, x, 0);
                    }
                    else
                    {
                        if (y == 1)
                        {
                            CreateDeleteButton(x, y);
                        }
                        else
                        {
                            // If the cell is not a header, it's a checkbox
                            CreateCheckbox(x, y);
                        }
                        
                    }        
                }            
            }

            btnAddHabit = new Button();
            btnAddHabit.BackgroundImage = Image.FromFile("C:\\Progamming Projects\\School\\Major Project\\major-project\\Major Project - Productivity App - Hector F\\Major Project - Productivity App - Hector F\\Resources\\Icons\\Add Icon.png");
            btnAddHabit.Size = new Size(35, 35);
            btnAddHabit.BackgroundImageLayout = ImageLayout.Stretch;
            btnAddHabit.Click += new EventHandler(btnAddHabit_Click);
            habitsGrid.Controls.Add(btnAddHabit, habitsGrid.ColumnCount, 0);
        }

        private void AddNewHabit()
        {
            // Show the new habit box - for naming, icon, etc.



            // Add new column for habit
            habitsGrid.ColumnCount += 1;

            // Move the add button to this last, newly added column, leaving a column for the new habit
            habitsGrid.Controls.Remove(btnAddHabit);
            habitsGrid.Controls.Add(btnAddHabit, habitsGrid.ColumnCount, 0);

            // Create the new habit header at row 0 of the missing, new column
            Label newHabit = new Label();
            newHabit.Text = "New habit";
            newHabit.ForeColor = Color.White;
            newHabit.AutoSize = true;
            habitsGrid.Controls.Add(newHabit, habitsGrid.ColumnCount - 1, 0);

            // Create new button for deletion of new habit
            CreateDeleteButton(habitsGrid.ColumnCount - 1, 1);

            // Create the column of checkboxes for this next habit
            for (int y = 2; y <= habitsGrid.RowCount - 1; y++)
            {
                CreateCheckbox(habitsGrid.ColumnCount - 1, y);
            }
        }

        private void CreateCheckbox (int x, int y)
        {
            CheckBox habitCheckbox = new CheckBox();
            habitCheckbox.AutoSize = false;
            habitCheckbox.Size = new Size(40, 40);
            habitCheckbox.Anchor = AnchorStyles.Top;
            habitCheckbox.CheckAlign = ContentAlignment.TopCenter;
            habitCheckbox.Padding = new Padding(0, 0, 0, 0);
            habitsGrid.Controls.Add(habitCheckbox, x, y);
        }

        private void CreateDeleteButton (int x, int y)
        {
            Button btnDeleteHabit = new Button();
            btnDeleteHabit.BackgroundImage = Image.FromFile("C:\\Progamming Projects\\School\\Major Project\\major-project\\Major Project - Productivity App - Hector F\\Major Project - Productivity App - Hector F\\Resources\\Icons\\Delete Icon.png");
            btnDeleteHabit.BackgroundImageLayout = ImageLayout.Stretch;
            btnDeleteHabit.Size = new Size(35, 35);
            btnDeleteHabit.Anchor = AnchorStyles.None;
            btnDeleteHabit.Click += new EventHandler(btnDeleteHabit_Click);

            habitsGrid.Controls.Add(btnDeleteHabit, x, y);
        }

        private void btnAddHabit_Click (object sender, EventArgs e)
        {
            AddNewHabit();
        }
        private void btnDeleteHabit_Click (object sender, EventArgs e)
        {
            // Get button that called the event
            Button btnDeleteHabit = (Button)sender;

            // Remove the column (habit)
            TableLayoutPanelCellPosition cellInColumnToDelete = habitsGrid.GetPositionFromControl(btnDeleteHabit);
            for (int y = 0; y < habitsGrid.ColumnCount; y++)
            {
                habitsGrid.Controls.Remove(habitsGrid.GetControlFromPosition(cellInColumnToDelete.Column, y));
            }
        }
    }
}
