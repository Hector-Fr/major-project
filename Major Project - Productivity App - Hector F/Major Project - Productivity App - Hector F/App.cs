
namespace Major_Project___Productivity_App___Hector_F
{
    public partial class App : Form
    {
        // Variables
        Page activePage;
        Page[] pages;

        // Menu
        FlowLayoutPanel menu;
        public int menuWidth = 240;
        int buttonHeight = 80;

        // Colours
        public Color pageColour = Color.FromArgb(255, 40, 47, 62);
        public Color menuColour = Color.FromArgb(255, 34, 40, 53);
        public Color buttonColour = Color.FromArgb(255, 60, 70, 91);        

        public App()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Load and create everything for the form
            Initialise();
        }

        /// <summary>
        /// Initialises, creates and shows everything that is the interface of the app. Runs on app load.
        /// </summary>
        private void Initialise()
        {
            this.BackColor = pageColour;
            this.Size = new Size(1700, 1000);

            CreateMenu();

            CreatePages();
        }

        /// <summary>
        /// Creates all the pages of the app by initialising all instances of the page class in the separate scripts for each page
        /// </summary>
        private void CreatePages()
        {
            // Create the button in the menu associated with the page
            Button btnMenuHome = CreateMenuButton("HOME");
            // Create the page - initialise its dedicated class, in this case 'HomePage'
            HomePage homePage = new HomePage(this, "HOME", btnMenuHome);
            // When the button is clicked, it goes to that page
            btnMenuHome.Click += new EventHandler((sender, e) => btnMenuButton_OnMouseClick(sender, e, homePage));

            // And so on for the other pages...
            Button btnMenuHabits = CreateMenuButton("HABITS");
            HabitsPage habitsPage = new HabitsPage(this, "HABITS", btnMenuHabits);
            btnMenuHabits.Click += new EventHandler((sender, e) => btnMenuButton_OnMouseClick(sender, e, habitsPage));

            Button btnMenuTasks = CreateMenuButton("TASKS");
            TasksPage tasksPage = new TasksPage(this, "TASKS", btnMenuTasks);
            btnMenuTasks.Click += new EventHandler((sender, e) => btnMenuButton_OnMouseClick(sender, e, tasksPage));

            Button btnMenuFocus = CreateMenuButton("FOCUS");
            FocusPage focusPage = new FocusPage(this, "FOCUS", btnMenuFocus);
            btnMenuFocus.Click += new EventHandler((sender, e) => btnMenuButton_OnMouseClick(sender, e, focusPage));

            Button btnMenuGoals = CreateMenuButton("GOALS");
            GoalsPage goalsPage = new GoalsPage(this, "GOALS", btnMenuGoals);
            btnMenuGoals.Click += new EventHandler((sender, e) => btnMenuButton_OnMouseClick(sender, e, goalsPage));

            // Store all the pages in an array
            pages = new Page[] { homePage, habitsPage, tasksPage, focusPage, goalsPage };

            // When the app opens, the default page is the home page
            SwitchToPage(pages[0]);
        }
        
        /// <summary>
        /// When called, this method ensures all other pages are disabled, then the desired page is enabled
        /// </summary>
        /// <param name="page"></param>
        private void SwitchToPage (Page page)
        {
            // Loop through all the pages in the array and disable them
            foreach (Page _page in pages)
            {
                _page.pagePanel.Visible = false;
            }
            // Enable the desired page, making it active
            page.pagePanel.Visible = true;
            activePage = page;
        }

        /// <summary>
        /// Create the menu (flow layout panel) sidebar with all the buttons directing to the different pages
        /// </summary>
        private void CreateMenu()
        {
            // Create the menu as a list of buttons, make it visible
            menu = new FlowLayoutPanel();
            menu.Size = new Size(menuWidth, this.Height);
            menu.BackColor = menuColour;
            menu.FlowDirection = FlowDirection.TopDown;
            menu.BorderStyle = BorderStyle.None;
            menu.Margin = new Padding(0, 0, 0, 0);
            menu.Location = new Point(0, 0);
            Controls.Add(menu);

            menu.Visible = true;
        }

        /// <summary>
        /// Creates the button that is associated with a page. This button will be in the menu sidebar
        /// </summary>
        /// <param name="buttonName"></param>
        /// <returns></returns>
        private Button CreateMenuButton(string buttonName)
        {
            // Create the button with the name of the page as the text
            Button btnMenuButton = new Button();
            btnMenuButton.Size = new Size(menuWidth, buttonHeight);
            btnMenuButton.BackColor = buttonColour;
            btnMenuButton.FlatStyle = FlatStyle.Flat;
            btnMenuButton.FlatAppearance.BorderColor = Color.FromArgb(255, 43, 45, 60);
            btnMenuButton.FlatAppearance.BorderSize = 3;
            btnMenuButton.Margin = new Padding(0, 0, 0, 0);
            btnMenuButton.ForeColor = Color.White;
            btnMenuButton.Text = buttonName;
            // Change the colour of the button if the mouse hovers over it
            btnMenuButton.MouseEnter += new EventHandler(btnMenuButton_OnMouseEnterButton);
            btnMenuButton.MouseLeave += new EventHandler(btnMenuButton_OnMouseExitButton);
         
            // Add the button to the menu sidebar (append onto the flow layout panel)
            menu.Controls.Add(btnMenuButton);

            return btnMenuButton;
        }

        private void btnMenuButton_OnMouseClick(object sender, EventArgs e, Page page)
        {
            // When the button is clicked, switch to the page associated with the button
            Button btnMenuButton = (Button)sender;
            SwitchToPage(page);
        }

        private void btnMenuButton_OnMouseEnterButton(object sender, EventArgs e)
        {
            // When the mouse hovers over the button, change its colour
            Button button = (Button)sender;
            button.BackColor = Color.FromArgb(50, 189, 189, 208);
        }
        private void btnMenuButton_OnMouseExitButton(object sender, EventArgs e)
        {
            // When the mouse leaves hovering over the button, change back to its normal colour
            Button button = (Button)sender;
            button.BackColor = buttonColour;
        }

    }
}
