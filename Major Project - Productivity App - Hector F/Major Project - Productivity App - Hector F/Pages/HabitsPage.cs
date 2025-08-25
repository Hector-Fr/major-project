
namespace Major_Project___Productivity_App___Hector_F
{
    class HabitsPage : Page
    {
        // --------------- VARIABLES -------------

        string[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

        // The max number of days of habits that the habit grid (rows) should show at one time
        int numOfDaysToShow = 5;

        // Habit Grid
        TableLayoutPanel habitsGrid;
        Size habitsGridSize = new Size(1000, 600);
        // The adjustment on the y-axis that centres the habit grid
        int yAdjust = 20;

        // Controls
        Button btnAddHabit;
        TextBox txtbxEnterHabitName;
        Label lblEnterHabitName;

        // File IO
        string habitStatesFilePath = "C:\\Progamming Projects\\School\\Major Project\\major-project\\Major Project - Productivity App - Hector F\\Major Project - Productivity App - Hector F\\bin\\Debug\\HabitStates.txt";


        // Constructor that initialises + inheriting from base class
        public HabitsPage(App mainForm, string pageName, Button menuButton) : base(mainForm, pageName, menuButton) { }

        /// <summary>
        /// Override method that inherits the functionality from the base 'page' class, but adds on functionality unique to the habits page
        /// </summary>
        /// <param name="pageName"></param>
        public override void Create(string pageName)
        {
            // Inherit from base page class
            base.Create(pageName);
            // Subscribe event for saving/writing to file when the form closes
            mainForm.FormClosing += new FormClosingEventHandler(MainForm_FormClosing);

            // Read the habit data (as a tuple of the habit name string array and the 2D boolean array)
            var habitData = LoadHabitDataFromFile();
            // Use the data to create habit grid
            CreateHabitsGrid(habitData.Item1, habitData.Item2);
        }

        /// <summary>
        /// Reads the habit data from the file - the names of the habits and the 2D array of habit states (booleans)
        /// </summary>
        /// <returns></returns>
        private (string[], bool[,]) LoadHabitDataFromFile()
        {
            string[] habitNames;
            int rows = 0, columns = 0;

            // If there is nothing in the file, there is no habit data to load => create brand new habit grid
            if (string.IsNullOrWhiteSpace(File.ReadAllText(habitStatesFilePath)))
            {
                return (null, null);
            }
            // Open and read through the file
            using (StreamReader reader = new StreamReader(habitStatesFilePath))
            {
                // Loop through each line of the file
                while (!reader.EndOfStream)
                {
                    // Calculate the number of rows and columns needed in the habits grid
                    string[] items = reader.ReadLine().Split(' ');
                    columns = items.Length;
                    rows++;
                }
            }

            bool[,] habitData = new bool[rows, columns];

            using (StreamReader reader = new StreamReader(habitStatesFilePath))
            {
                int rowIndex = 0;

                habitNames = reader.ReadLine().Split("/");

                while (!reader.EndOfStream)
                {
                    // Get the raw contents in each line/row (day), separated by a space
                    string[] rawRow = reader.ReadLine().Split(' ');

                    // Converts the string array to a bool array of checkbox states => e.g {true, false, false, true} by looping through the contents in the line
                    for (int x = 0; x < rawRow.Length; x++)
                    {
                        habitData[rowIndex, x] = bool.Parse(rawRow[x]);
                    }

                    rowIndex++;
                }
            }

            return (habitNames, habitData);
        }

        /// <summary>
        /// The habits grid is a table including the rows for the days, the columns for the habits, including checkboxes, buttons, text, etc. in the grid as well
        /// </summary>
        /// <param name="habitData"></param>
        private void CreateHabitsGrid(string[] habitNames, bool[,] habitData)
        {
            // Create the grid - set # of rows and columns
            habitsGrid = new TableLayoutPanel();
            habitsGrid.RowCount = numOfDaysToShow + 2;

            // If there was data in the file
            if (habitData != null)
            {
                // Add two extra columns to the habits grid - one for the days, and one for the plus button (empty column)
                habitsGrid.ColumnCount = habitData.GetLength(1) + 2;
            }
            else
            {
                // If there was not data in the file, there will be no habits, but there are just the two required columns of the grid
                habitsGrid.ColumnCount = 2;
            }

            // Initialise other aspects of the grid
            habitsGrid.BackColor = mainForm.buttonColour;
            habitsGrid.Size = habitsGridSize;
            // Centre the grid
            habitsGrid.Location = new Point(pagePanel.Width / 2 - habitsGrid.Width / 2, pagePanel.Height / 2 - habitsGrid.Height / 2 - yAdjust);
            // If the habits exceed the size of the grid, add scrolling
            habitsGrid.AutoScroll = true;
            habitsGrid.GrowStyle = TableLayoutPanelGrowStyle.AddColumns;
            pagePanel.Controls.Add(habitsGrid);

            // Loop through each item in the grid
            for (int y = 0; y < habitsGrid.RowCount; y++)
            {
                for (int x = 0; x < habitsGrid.ColumnCount - 1; x++)
                {
                    // In the first column
                    if (x == 0)
                    {
                        // The top-left-most cell is the first column header: day header
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
                        // All the days under the header
                        else
                        {
                            Label dayLabel = new Label();
                            // For the days before 'yesterday', get the day date number (e.g 23rd of August -> 23)
                            DateTime dateOfHabitRow = DateTime.Now.AddDays(2 - y);

                            // For the past couple of days, use 'today' and 'yesterday'
                            if (y <= 3)
                            {
                                switch (y)
                                {
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
                                // Set the date text (e.g '15 August')
                                string month = months[dateOfHabitRow.Month - 1];
                                dayLabel.Text = month + " " + dateOfHabitRow.Day.ToString();
                            }
                            // Other label initialisatione
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
                        habitLabel.Text = habitNames[x - 1].ToString();
                        habitLabel.ForeColor = Color.White;
                        habitLabel.TextAlign = ContentAlignment.MiddleCenter;
                        habitLabel.AutoSize = true;
                        habitsGrid.Controls.Add(habitLabel, x, 0);
                    }
                    else
                    {
                        // The first row is a row of delete buttons
                        if (y == 1)
                        {
                            CreateDeleteButton(x, y);
                        }
                        // The rest of the rows that are not the first or last column are checkboxes
                        else
                        {
                            // If there is no data in the file
                            if (habitData == null)
                            {
                                // Just create the checkboxes
                                CreateCheckbox(x, y, false);
                            }
                            // If there was data in the file
                            else
                            {
                                // If there is habit data (dimensions > 0)
                                if (habitData.GetLength(0) != 0 && habitData.GetLength(1) != 0)
                                {
                                    // Load data from file into this checkbox - checkboxes already checked
                                    CreateCheckbox(x, y, habitData[y - 2, x - 1]);
                                }
                                
                            }
                        }
                    }

                }
            }
        
            // Create the 'add habit' button
            btnAddHabit = new Button();
            btnAddHabit.BackgroundImage = Image.FromFile("C:\\Progamming Projects\\School\\Major Project\\major-project\\Major Project - Productivity App - Hector F\\Major Project - Productivity App - Hector F\\Resources\\Icons\\Add Icon.png");
            btnAddHabit.Size = new Size(30, 30);
            btnAddHabit.BackgroundImageLayout = ImageLayout.Stretch;
            btnAddHabit.Click += new EventHandler(btnAddHabit_Click);
            btnAddHabit.FlatStyle = FlatStyle.Flat;
            btnAddHabit.FlatAppearance.BorderSize = 0;
            habitsGrid.Controls.Add(btnAddHabit, habitsGrid.ColumnCount, 0);
            new ToolTip().SetToolTip(btnAddHabit, "Add a new habit");

            // Create the text box to enter the name of the habit
            txtbxEnterHabitName = new TextBox();
            txtbxEnterHabitName.Size = new Size(180, txtbxEnterHabitName.Size.Height);
            txtbxEnterHabitName.Visible = false;
            txtbxEnterHabitName.KeyDown += new KeyEventHandler(txtbxEnterHabitName_KeyDown);
            txtbxEnterHabitName.LostFocus += new EventHandler(txtbxEnterHabitName_ExitFocus);
            pagePanel.Controls.Add(txtbxEnterHabitName);

            // Create the label that labels the text box
            lblEnterHabitName = new Label();
            lblEnterHabitName.Text = "Enter habit name";
            lblEnterHabitName.ForeColor = Color.White;
            lblEnterHabitName.AutoSize = true;
            lblEnterHabitName.Location = new Point(txtbxEnterHabitName.Location.X + 130, txtbxEnterHabitName.Location.Y + 30);
            lblEnterHabitName.Visible = false;
            pagePanel.Controls.Add(lblEnterHabitName);
        }

        private void AddNewDay()
        {
            habitsGrid.RowCount++;

            /*for (int x = 0; x < habitsGrid.ColumnCount; x++)
            {
                CreateCheckbox(x, 1);
            } */
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
                CreateCheckbox(habitsGrid.ColumnCount - 1, y, false);
            }
        }

        /// <summary>
        /// Writes to the habit data file with the names of the habits and the progress (which checkboxes are ticked)
        /// </summary>
        private void ShowHabitNamingBox()
        {
            // Show the new habit box - for naming, icon, etc.
            txtbxEnterHabitName.Location = new Point(btnAddHabit.Location.X + 10, btnAddHabit.Location.Y + 60);
            txtbxEnterHabitName.Visible = true;
            txtbxEnterHabitName.Focus();

            lblEnterHabitName.Visible = true;
            lblEnterHabitName.Location = new Point(txtbxEnterHabitName.Location.X, txtbxEnterHabitName.Location.Y - 30);

            // Show the textbox until the keydown event is called (enter key is pressed) and direct to habit creation method
        }

        private void CreateCheckbox (int x, int y, bool _checked)
        {
            // Create the checkbox
            CheckBox habitCheckbox = new CheckBox();
            habitCheckbox.AutoSize = false;
            habitCheckbox.Size = new Size(40, 40);
            habitCheckbox.Anchor = AnchorStyles.Top;
            habitCheckbox.CheckAlign = ContentAlignment.TopCenter;
            habitCheckbox.Padding = new Padding(0, 0, 0, 0);
            habitCheckbox.Checked = _checked;
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

            new ToolTip().SetToolTip(btnDeleteHabit, "Delete Habit");
        }


        private void SaveHabitData()
        {
            // Save habits grid checkbox layout in a 2D array of bools representing: true = checkbox ticked, false = not ticked
            bool[,] checkboxGridState = new bool[habitsGrid.RowCount - 2, habitsGrid.ColumnCount - 2];

            for (int y = 0; y < checkboxGridState.GetLength(0); y++)
            {
                for (int x = 0; x < checkboxGridState.GetLength(1); x++)
                {
                    Control control = habitsGrid.GetControlFromPosition(x + 1, y + 2);

                    if (control != null)
                    {
                        checkboxGridState[y, x] = (control as CheckBox).Checked;
                    }
                }
            }

            string habitNameLine = "";

            // Loop through the columns in the top row
            for (int column = 0; column < habitsGrid.ColumnCount - 1; column++)
            {
                // Get the string name from the grid
                Label habitName = habitsGrid.GetControlFromPosition(column + 1, 0) as Label;

                if (habitName != null)
                {
                    if (column != habitsGrid.ColumnCount - 2)
                        habitNameLine += habitName.Text + "/";
                    else
                        habitNameLine += habitName.Text;
                }
            }
            // Write to the file, a 2D grid of bools as the habits states
            // e.g    true true false
            //        false true true
            // Open file to write to it
            using (StreamWriter writer = new StreamWriter(habitStatesFilePath, false))
            {
                writer.WriteLine(habitNameLine);

                // Loop through checkboxGridState 2D bool array
                for (int y = 0; y < checkboxGridState.GetLength(0); y++)
                {
                    for (int x = 0; x < checkboxGridState.GetLength(1); x++)
                    {
                        // Separate the states by a space, unless it is the last column (don't add a space after)
                        if (x == checkboxGridState.GetLength(1) - 1)
                        {
                            writer.Write(checkboxGridState[y, x]);
                        }
                        else
                        {
                            writer.Write(checkboxGridState[y, x] + " ");
                        }
                    }

                    if (y != checkboxGridState.GetLength(0) - 1 && checkboxGridState.GetLength(1) != 0)
                    {
                        // Add a new line
                        writer.Write("\n");
                    }
                }
            }
        }


        // -------------- EVENTS -------------------------
        #region Events
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
            habitsGrid.ColumnCount--;
        }
    
        // Runs when a key is pressed and the text box is in focus
        private void txtbxEnterHabitName_KeyDown (object sender, KeyEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            // If the key is 'enter' and the textbox has something in it
            if (e.KeyCode == Keys.Return && textBox.Text != "")
            {
                // Create a new habit, using the name entered in the text box
                AddNewHabit(textBox.Text);
                // Reset the text box and make it invisible
                textBox.Text = "";
                textBox.Visible = false;
                lblEnterHabitName.Visible = false;
            }
        }

        // Runs when the text box is clicked off of
        private void txtbxEnterHabitName_ExitFocus (object sender, EventArgs e)
        {
            // Disable and make the text box disappear
            TextBox textBox = (TextBox)sender;
            textBox.Text = "";
            textBox.Visible = false;
            lblEnterHabitName.Visible = false;
        }

        /// <summary>
        /// This event method runs just before the form closes. Initiates saving/writing to file
        /// </summary>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveHabitData();
        }
        #endregion
    }
}
