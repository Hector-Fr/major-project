using System.IO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Major_Project___Productivity_App___Hector_F
{
    class HabitsPage : Page
    {
        string[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

        // The max number of days of habits that the habit grid (rows) should show at one time
        int numOfDaysToShow = 5;
        // The number of habits there are (columns). The user can add more
        int numOfHabits = 0;

        TableLayoutPanel habitsGrid;
        int yAdjust = 20;
        Size habitsGridSize = new Size(1000, 600);
        Button btnAddHabit;
        TextBox txtbxEnterHabitName;
        Label lblEnterHabitName;
        string habitStatesFilePath = "C:\\Progamming Projects\\School\\Major Project\\major-project\\Major Project - Productivity App - Hector F\\Major Project - Productivity App - Hector F\\bin\\Debug\\HabitStates.txt";

        public HabitsPage(App mainForm, string pageName, Button menuButton) : base(mainForm, pageName, menuButton) { }

        public override void Create(string pageName)
        {
            base.Create(pageName);

            mainForm.FormClosing += new FormClosingEventHandler(MainForm_FormClosing);

            bool[,] habitData = LoadHabitDataFromFile();
            CreateHabitsGrid(habitData);
        }

        private bool[,] LoadHabitDataFromFile()
        {
            int rows = 0, columns = 0;

            if (string.IsNullOrWhiteSpace(File.ReadAllText(habitStatesFilePath)))
            {
                return null;
            }
            using (StreamReader reader = new StreamReader(habitStatesFilePath))
            {
                while (!reader.EndOfStream)
                {
                    string[] items = reader.ReadLine().Split(' ');
                    columns = items.Length;
                    rows++;
                }
            }

            bool[,] habitData = new bool[rows, columns];

            using (StreamReader reader = new StreamReader(habitStatesFilePath))
            {
                int rowIndex = 0;

                while (!reader.EndOfStream)
                {
                    string[] rawRow = reader.ReadLine().Split(' ');

                    for (int x = 0; x < rawRow.Length; x++)
                    {
                        habitData[rowIndex, x] = bool.Parse(rawRow[x]);
                    }

                    rowIndex++;
                }
            }

            return habitData;
        }

        private void CreateHabitsGrid(bool[,] habitData)
        {
            // The habits grid is a table including the rows for the days, the columns for the habits, including checkboxes, buttons, text, etc. in the grid as well
            habitsGrid = new TableLayoutPanel();
            habitsGrid.RowCount = numOfDaysToShow + 2;

            string text = "";
            for (int y = 0; y < habitData.GetLength(0); y++)
            {
                for (int x = 0; x < habitData.GetLength(1); x++)
                {
                    text += habitData[y, x].ToString();
                }

                text += "\n";
            }
           
            MessageBox.Show(text);

            if (habitData != null)
            {
                try
                {
                    habitsGrid.ColumnCount = habitData.GetLength(1) + 2;
                }
                catch
                {
                    habitsGrid.ColumnCount = 3;
                }
            }
            else
            {
                habitsGrid.ColumnCount = 2;
            }

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
                            // If there is no data in the file
                            if (habitData == null)
                            {
                                // Just create the checkboxes
                                CreateCheckbox(x, y, false);
                            }
                            // If there was data in the file
                            else
                            {
                                if (habitData.GetLength(0) != 0 && habitData.GetLength(1) != 0)
                                {
                                    // Load data from file into this checkbox
                                    CreateCheckbox(x, y, habitData[y - 2, x - 1]);
                                }
                                
                            }
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
            new ToolTip().SetToolTip(btnAddHabit, "Add a new habit");

            txtbxEnterHabitName = new TextBox();
            txtbxEnterHabitName.Size = new Size(180, txtbxEnterHabitName.Size.Height);
            txtbxEnterHabitName.Visible = false;
            txtbxEnterHabitName.KeyDown += new KeyEventHandler(txtbxEnterHabitName_KeyDown);
            txtbxEnterHabitName.LostFocus += new EventHandler(txtbxEnterHabitName_ExitFocus);
            pagePanel.Controls.Add(txtbxEnterHabitName);

            lblEnterHabitName = new Label();
            lblEnterHabitName.Text = "Enter habit name";
            lblEnterHabitName.ForeColor = Color.White;
            lblEnterHabitName.AutoSize = true;
            lblEnterHabitName.Location = new Point(txtbxEnterHabitName.Location.X + 130, txtbxEnterHabitName.Location.Y + 30);
            lblEnterHabitName.Visible = false;
            pagePanel.Controls.Add(lblEnterHabitName);

            DateTime midnight = DateTime.Today.AddDays(1).AddSeconds(-1);
        }

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

        private void AddNewDay()
        {
            habitsGrid.RowCount++;

            /*for (int x = 0; x < habitsGrid.ColumnCount; x++)
            {
                CreateCheckbox(x, 1);
            } */
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
    
        private void txtbxEnterHabitName_KeyDown (object sender, KeyEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (e.KeyCode == Keys.Return && textBox.Text != "")
            {
                AddNewHabit(textBox.Text);
                textBox.Text = "";
                textBox.Visible = false;
                lblEnterHabitName.Visible = false;
            }
        }

        private void txtbxEnterHabitName_ExitFocus (object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Text = "";
            textBox.Visible = false;
            lblEnterHabitName.Visible = false;
        }

        /// <summary>
        /// This event method runs just before the form closes. Includes saving/writing to file functions
        /// </summary>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
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


            // Write to the file, a 2D grid of bools as the habits states
            // e.g    true true false
            //        false true true
            // Open file to write to it
            using (StreamWriter writer = new StreamWriter(habitStatesFilePath, false))
            {
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
    }
}
