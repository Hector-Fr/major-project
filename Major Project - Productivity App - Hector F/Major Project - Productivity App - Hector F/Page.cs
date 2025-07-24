using System;
using System.Windows.Forms;

namespace Major_Project___Productivity_App___Hector_F
{
    public class Page
    {
        public App mainForm;
        public Panel pagePanel;
        public Button menuButton;

        public Page(App mainForm)
        {
            this.mainForm = mainForm;
            pagePanel = new Panel();
        }

        public virtual void Create()
        {
            //pagePanel.BackColor = Color.FromArgb(255, 37, 37, 50);
            pagePanel.Size = new Size(mainForm.Width - mainForm.menuWidth, mainForm.Height);
            pagePanel.Location = new Point(mainForm.menuWidth, 0);
            mainForm.Controls.Add(pagePanel);
        }
    }
}
