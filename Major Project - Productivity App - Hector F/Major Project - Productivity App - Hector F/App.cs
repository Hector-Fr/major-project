using System.Windows.Forms;

namespace Major_Project___Productivity_App___Hector_F
{
    public partial class App : Form
    {
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

            pages = new Page[] { CreatePage("HOME"), 
                                 CreatePage("HABITS"), 
                                 CreatePage("TASKS"), 
                                 CreatePage("FOCUS TIMER"), 
                                 CreatePage("GOALS") };

            menu.Visible = true;
        }

        private Page CreatePage(string pageName)
        {
            Page page = new Page(pageName);

            page.panel.Size = new Size(this.Width - menuWidth, this.Height);
            page.panel.BackColor = Color.FromArgb(255, 37, 37, 50);
            page.panel.Location = new Point(menuWidth, 0);
            Controls.Add(page.panel);

            page.menuButton = CreateMenuButton(pageName);
            page.menuButton.Click += (sender, e) => menuButton_OnMouseClick(sender, e, page);

            return page;
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
            menuButton.MouseEnter += new EventHandler(menuButton_OnMouseEnterButton);
            menuButton.MouseLeave += new EventHandler(menuButton_OnMouseExitButton);
         
            menu.Controls.Add(menuButton);

            return menuButton;
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
