using System.Windows.Forms;

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

            CreateMenu();

            Page homePage = CreateHomePage();
            Button btnMenuButton = CreateMenuButton("HOME");
            btnMenuButton.Click += new EventHandler(btnMenuButton_OnMouseClick, homePage);
            homePage.menuButton = btnMenuButton;
        }

        private void CreateMenu()
        {
            menu = new FlowLayoutPanel();
            menu.Size = new Size(menuWidth, this.Height);
            menu.BackColor = Color.FromArgb(255, 61, 63, 82);
            menu.FlowDirection = FlowDirection.TopDown;
            menu.BorderStyle = BorderStyle.None;
            menu.ClientSize = menu.Size;
            menu.Margin = new Padding(0, 0, 0, 0);
            menu.Location = new Point(0, 0);
            Controls.Add(menu);

            menu.Visible = true;
        }


        private Button CreateMenuButton(string buttonName)
        {
            Button btnMenuButton = new Button();
            btnMenuButton.Size = new Size(menuWidth, buttonHeight);
            btnMenuButton.BackColor = Color.FromArgb(255, 61, 63, 82);
            btnMenuButton.FlatStyle = FlatStyle.Flat;
            btnMenuButton.FlatAppearance.BorderColor = Color.FromArgb(255, 43, 45, 60);
            btnMenuButton.FlatAppearance.BorderSize = 3;
            btnMenuButton.Margin = new Padding(0, 0, 0, 0);
            btnMenuButton.ForeColor = Color.White;
            btnMenuButton.Text = buttonName;
            btnMenuButton.MouseEnter += new EventHandler(btnMenuButton_OnMouseEnterButton);
            btnMenuButton.MouseLeave += new EventHandler(btnMenuButton_OnMouseExitButton);
            
         
            menu.Controls.Add(btnMenuButton);

            return btnMenuButton;
        }

        private void btnMenuButton_OnMouseClick(object sender, EventArgs e, Page page)
        {
            Button btnMenuButton = (Button)sender;

            btnMenuButton.Visible = true;
        }

        private void btnMenuButton_OnMouseEnterButton(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.BackColor = Color.FromArgb(50, 189, 189, 208);
        }
        private void btnMenuButton_OnMouseExitButton(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.BackColor = Color.FromArgb(255, 61, 63, 82);
        }


        private Page CreateHomePage()
        {
            Page homePage = new Page();
            homePage.pagePanel.BackColor = Color.FromArgb(255, 37, 37, 255);
            homePage.pagePanel.Size = new Size(this.Width - menuWidth, this.Height);
            homePage.pagePanel.Location = new Point(menuWidth, 0);
            Controls.Add(homePage.pagePanel);

            Label txtbxWelcome = new Label();
            txtbxWelcome.Text = "Welcome. Time to crush your productivity goals.";
            txtbxWelcome.AutoSize = true;
            txtbxWelcome.Location = new Point((this.Width - menuWidth) / 2 - txtbxWelcome.Width / 2, this.Height / 2 - txtbxWelcome.Height / 2);
            Controls.Add(txtbxWelcome);
            txtbxWelcome.Parent = homePage.pagePanel;

            homePage.pagePanel.Visible = false;

            return homePage;
        }

        private Page CreateHabitsPage()
        {
            Page habitsPage = new Page();
            habitsPage.pagePanel.BackColor = Color.FromArgb(255, 37, 37, 50);
            habitsPage.pagePanel.Size = new Size(this.Width - menuWidth, this.Height);
            habitsPage.pagePanel.Location = new Point(menuWidth, 0);
            Controls.Add(habitsPage.pagePanel);

            habitsPage.pagePanel.Visible = false;

            return habitsPage;
        }
    }
}
