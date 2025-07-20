namespace Major_Project___Productivity_App___Hector_F
{
    public partial class App : Form
    {
        FlowLayoutPanel menu;
        int menuWidth = 240;
        int buttonHeight = 80;

        public App()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Initialise();
        }

        /// <summary>
        /// Initialises, creates and shows everything that is the interface of the app. Runs on app load.
        /// </summary>
        private void Initialise()
        {
            this.BackColor = Color.FromArgb(255, 37, 37, 50);

            menu = new FlowLayoutPanel();
            menu.Size = new Size(menuWidth, this.Height);
            menu.BackColor = Color.FromArgb(255, 61, 63, 82);
            menu.FlowDirection = FlowDirection.TopDown;
            menu.BorderStyle = BorderStyle.None;
            menu.ClientSize = menu.Size;
            menu.Margin = new Padding(0, 0, 0, 0);
            menu.Location = new Point(0, 0);
            menu.Visible = false;
            Controls.Add(menu);

            Button homeButton = CreateMenuButton("HOME");
            Button habitsButton = CreateMenuButton("HABITS");
            Button tasksGoalsButton = CreateMenuButton("TASKS AND GOALS");

            menu.Visible = true;
        }

        private Button CreateMenuButton(string buttonName)
        {
            Button menuButton = new Button();
            menuButton.Size = new Size(menuWidth, buttonHeight);
            menuButton.BackColor = Color.FromArgb(255, 61, 63, 82);
            menuButton.FlatStyle = FlatStyle.Flat;
            menuButton.FlatAppearance.BorderColor = Color.FromArgb(255, 43, 45, 60);
            menuButton.FlatAppearance.BorderSize = 3;
            menuButton.Margin = new Padding(0, 0, 0, 0);
            menuButton.ForeColor = Color.White;
            menuButton.Text = buttonName;
            menuButton.MouseEnter += new EventHandler(OnMouseEnterButton);
            menuButton.MouseLeave += new EventHandler(OnMouseExitButton);

            menu.Controls.Add(menuButton);

            return menuButton;
        }

        private void OnMouseEnterButton(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.BackColor = Color.FromArgb(50, 189, 189, 208);
        }
        private void OnMouseExitButton(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.BackColor = Color.FromArgb(255, 61, 63, 82);
        }
    }
}
