
using System.Resources;

namespace Major_Project___Productivity_App___Hector_F
{
    class HabitsPage : Page
    {
        string[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"};

        // The max number of days of habits that the habit grid (rows) should show at one time
        int numOfDaysToShow = 5;
        // The number of habits there are (columns). The user can add more
        int numOfHabits = 0;

        TableLayoutPanel habitsGrid;
        int yAdjust = 20;
        Size habitsGridSize = new Size(1000, 600);
        Button btnAddHabit;
        TextBox txtbxEnterHabitName;

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
                            Label dayLabel = new Label();
                            DateTime dateOfHabitRow = DateTime.Now.AddDays(2 - y);
                            string ordinal = "";

                            if (y <= 3)
                            {
                                switch (y)
                                {
                                    case 1:
                                        break;
                                    case 2:
                                        dayLabel.Text = "Today";
                                        break;
                                    case 3:
                                        dayLabel.Text = "Yesterday";
                                        break;
                                }
                            }
                            else
                            {
                                switch (dateOfHabitRow.Day % 10)
                                {
                                    case 1:
                                        if ((dateOfHabitRow.Day / 10) % 10 != 1)
                                            ordinal = "st";
                                        break;
                                    case 2:
                                        if ((dateOfHabitRow.Day / 10) % 10 != 1)
                                            ordinal = "nd";
                                        break;
                                    case 3:
                                        if ((dateOfHabitRow.Day / 10) % 10 != 1)
                                            ordinal = "rd";
                                        break;
                                    default:
                                        ordinal = "th";
                                        break;
                                }

                                string month = months[dateOfHabitRow.Month - 1];

                                dayLabel.Text = dateOfHabitRow.Day.ToString() + ordinal + " " + month;
                            }
                            
                            
                            dayLabel.AutoSize = true;
                            dayLabel.TextAlign = ContentAlignment.BottomCenter;
                            dayLabel.ForeColor = Color.White;
                            habitsGrid.Controls.Add(dayLabel, 0, y);
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
            btnAddHabit.Size = new Size(30, 30);
            btnAddHabit.BackgroundImageLayout = ImageLayout.Stretch;
            btnAddHabit.Click += new EventHandler(btnAddHabit_Click);
            btnAddHabit.FlatStyle = FlatStyle.Flat;
            btnAddHabit.FlatAppearance.BorderSize = 0;
            habitsGrid.Controls.Add(btnAddHabit, habitsGrid.ColumnCount, 0);

            txtbxEnterHabitName = new TextBox();
            txtbxEnterHabitName.Size = new Size(180, txtbxEnterHabitName.Size.Height);
            txtbxEnterHabitName.Visible = false;
            txtbxEnterHabitName.KeyDown += new KeyEventHandler(txtbxEnterHabitName_KeyDown);
            txtbxEnterHabitName.GotFocus += new EventHandler(txtbxEnterHabitName_EnterFocus);
            txtbxEnterHabitName.LostFocus += new EventHandler(txtbxEnterHabitName_ExitFocus);
            pagePanel.Controls.Add(txtbxEnterHabitName);
        }

        private void ShowHabitNamingBox()
        {
            // Show the new habit box - for naming, icon, etc.
            txtbxEnterHabitName.Location = new Point(btnAddHabit.Location.X + 10, btnAddHabit.Location.Y + 60);
            txtbxEnterHabitName.ForeColor = Color.Gray;
            txtbxEnterHabitName.Text = "Enter habit name...";
            txtbxEnterHabitName.Visible = true;

            // Show the textbox until the keydown event is called (enter key is pressed) and direct to habit creation method
        }


        private void AddNewHabit(string habitName)
        {
            // Add new column for habit
            habitsGrid.ColumnCount += 1;

            // Move the add button to this last, newly added column, leaving a column for the new habit
            habitsGrid.Controls.Remove(btnAddHabit);
            habitsGrid.Controls.Add(btnAddHabit, habitsGrid.ColumnCount, 0);

            // Create the new habit header at row 0 of the missing, new column
            Label newHabit = new Label();
            newHabit.Text = habitName;
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
            // Create the checkbox
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
            // Create the delete button
            Button btnDeleteHabit = new Button();
            btnDeleteHabit.BackgroundImage = Image.FromFile("C:\\Progamming Projects\\School\\Major Project\\major-project\\Major Project - Productivity App - Hector F\\Major Project - Productivity App - Hector F\\Resources\\Icons\\Delete Icon.png");
            btnDeleteHabit.BackgroundImageLayout = ImageLayout.Stretch;
            btnDeleteHabit.Size = new Size(30, 30);
            btnDeleteHabit.Anchor = AnchorStyles.None;
            btnDeleteHabit.FlatStyle = FlatStyle.Flat;
            btnDeleteHabit.FlatAppearance.BorderSize = 0;
            btnDeleteHabit.Click += new EventHandler(btnDeleteHabit_Click);
            // Add it to the grid at the position (x, y)
            habitsGrid.Controls.Add(btnDeleteHabit, x, y);
        }

        private void btnAddHabit_Click (object sender, EventArgs e)
        {
            ShowHabitNamingBox();
        }
        private void btnDeleteHabit_Click (object sender, EventArgs e)
        {
            // Get button that called the event
            Button btnDeleteHabit = (Button)sender;

            // Get the column that the delete button is associated with
            TableLayoutPanelCellPosition cellInColumnToDelete = habitsGrid.GetPositionFromControl(btnDeleteHabit);
            for (int y = 0; y < habitsGrid.RowCount; y++)
            {
                // Loop through all the items in the column and remove the control
                habitsGrid.Controls.Remove(habitsGrid.GetControlFromPosition(cellInColumnToDelete.Column, y));
            }
        }
    
        private void txtbxEnterHabitName_KeyDown (object sender, KeyEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (e.KeyCode == Keys.Return)
            {
                AddNewHabit(textBox.Text);
                textBox.Text = "";
                textBox.Visible = false;
            }
        }

        private void txtbxEnterHabitName_EnterFocus (object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Text = "";
            textBox.ForeColor = Color.Black;
        }
        private void txtbxEnterHabitName_ExitFocus (object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Text = "";
            textBox.Visible = false;

        }
    }
}
