using System;

namespace Major_Project___Productivity_App___Hector_F
{
    class HabitsPage : Page
    {
        public HabitsPage(App mainForm) : base(mainForm) { }

        public override void Create()
        {
            base.Create();

            pagePanel.BackColor = Color.Orange;

            Label txtbxWelcome = new Label();
            txtbxWelcome.Text = "This is the habits page.";
            txtbxWelcome.AutoSize = true;
            txtbxWelcome.Location = new Point((mainForm.Width - mainForm.menuWidth) / 2 - txtbxWelcome.Width / 2, mainForm.Height / 2 - txtbxWelcome.Height / 2);
            mainForm.Controls.Add(txtbxWelcome);
            txtbxWelcome.Parent = this.pagePanel;
        }
    }
}
