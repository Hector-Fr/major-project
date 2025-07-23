using System.Windows.Forms;

namespace Major_Project___Productivity_App___Hector_F
{
    public partial class App : Form
    {
        public static int width, height;
        Page[] pages;
        FlowLayoutPanel menu;
        public static int menuWidth = 240;
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
            width = this.Width;
            height = this.Height;

            CreateMenu();
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

            pages = new Page[] { CreatePage("HOME", Color.Red), 
                                 CreatePage("HABITS", Color.Orange), 
                                 CreatePage("TASKS", Color.Yellow), 
                                 CreatePage("FOCUS TIMER", Color.Green), 
                                 CreatePage("GOALS", Color.Blue) };

            menu.Visible = true;
        }

        private Page CreatePage(string pageName, Color color)
        {
            Page page = new Page(pageName);

            page.panel.Size = new Size(this.Width - menuWidth, this.Height);
            page.panel.BackColor = color;//Color.FromArgb(255, 37, 37, 50);
            page.panel.Location = new Point(menuWidth, 0);
            Controls.Add(page.panel);

            page.btnMenuButton = CreateMenuButton(pageName);
            page.btnMenuButton.Click += (sender, e) => menuButton_OnMouseClick(sender, e, page);

            return page;
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
            btnMenuButton.MouseEnter += new EventHandler(menuButton_OnMouseEnterButton);
            btnMenuButton.MouseLeave += new EventHandler(menuButton_OnMouseExitButton);
         
            menu.Controls.Add(btnMenuButton);

            return btnMenuButton;
        }

        private void menuButton_OnMouseClick(object sender, EventArgs e, Page page)
        {
            foreach (Page _page in pages)
            {
                _page.Show(false);
            }

            page.Show(true);
        }

        private void menuButton_OnMouseEnterButton(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.BackColor = Color.FromArgb(50, 189, 189, 208);
        }
        private void menuButton_OnMouseExitButton(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.BackColor = Color.FromArgb(255, 61, 63, 82);
        }
    }
}
