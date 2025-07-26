using System;
using System.Windows.Forms;

namespace Major_Project___Productivity_App___Hector_F
{
    public class Page
    {
        public App mainForm;
        public Panel pagePanel;
        public Button menuButton;

        public Page(App mainForm, string pageName)
        {
            this.mainForm = mainForm;
            pagePanel = new Panel();

            Create(pageName);
        }

        public virtual void Create(string pageName)
        {
            pagePanel.BackColor = mainForm.pageColour;
            pagePanel.Size = new Size(mainForm.Width - mainForm.menuWidth, mainForm.Height);
            pagePanel.Location = new Point(mainForm.menuWidth, 0);
            mainForm.Controls.Add(pagePanel);

            Label txtbx = new Label();
            txtbx.Text = "This is the " + pageName + " page.";
            txtbx.AutoSize = true;
            txtbx.ForeColor = Color.White;
            txtbx.Location = new Point((mainForm.Width - mainForm.menuWidth) / 2 - txtbx.Width / 2, mainForm.Height / 2 - txtbx.Height / 2);
            mainForm.Controls.Add(txtbx);
            txtbx.Parent = pagePanel;
        }
    }
}
