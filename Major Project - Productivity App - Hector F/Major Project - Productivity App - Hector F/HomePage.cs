using System;
using System.Windows.Forms;

namespace Major_Project___Productivity_App___Hector_F
{
    class HomePage : Page
    {
        public HomePage(App mainForm) : base(mainForm) { }

        public override void Create()
        {
            base.Create();

            pagePanel.BackColor = Color.Red;

            Label txtbxWelcome = new Label();
            txtbxWelcome.Text = "Welcome. Time to crush your productivity goals.";
            txtbxWelcome.AutoSize = true;
            txtbxWelcome.Location = new Point((mainForm.Width - mainForm.menuWidth) / 2 - txtbxWelcome.Width / 2, mainForm.Height / 2 - txtbxWelcome.Height / 2);
            mainForm.Controls.Add(txtbxWelcome);
            txtbxWelcome.Parent = this.pagePanel;
        }
    }
}
