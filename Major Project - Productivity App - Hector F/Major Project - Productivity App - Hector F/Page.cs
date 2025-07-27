
namespace Major_Project___Productivity_App___Hector_F
{
    public class Page
    {
        public App mainForm;
        public Panel pagePanel;
        public Button menuButton;

        // Constructor for the class - pass in a reference to the main app script, the page name, and menu button associated with the page
        public Page(App mainForm, string pageName, Button menuButton)
        {
            // Initialise the page
            this.mainForm = mainForm;
            pagePanel = new Panel();
            this.menuButton = menuButton;
            // Create is automatically called when a new page instance is created
            Create(pageName);
        }

        public virtual void Create(string pageName)
        {
            // Each page is a panel that takes up the space that isn't taken up by the menu sidebar
            pagePanel.BackColor = mainForm.pageColour;
            pagePanel.Size = new Size(mainForm.Width - mainForm.menuWidth, mainForm.Height);
            pagePanel.Location = new Point(mainForm.menuWidth, 0);
            mainForm.Controls.Add(pagePanel);

            // FOR TESTING - show which page is active using a simple textbox
            Label txtbx = new Label();
            txtbx.Text = "This is the " + pageName + " page.";
            txtbx.AutoSize = true;
            txtbx.ForeColor = Color.White;
            txtbx.Location = new Point((mainForm.Width - mainForm.menuWidth) / 2 - txtbx.Width / 2, mainForm.Height / 2 - txtbx.Height / 2);
            mainForm.Controls.Add(txtbx);
            // IMPORTANT - every control in the page must be a child of the page panel, so that when the page panel is disabled, so will the controls
            txtbx.Parent = pagePanel;
        }
    }
}
