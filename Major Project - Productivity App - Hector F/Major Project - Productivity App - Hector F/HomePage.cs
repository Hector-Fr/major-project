using System;

namespace Major_Project___Productivity_App___Hector_F
{
    class HomePage : Page
    {
        public HomePage() : base("HOME") { }

        public void Initialise()
        {
            TextBox txtbxWelcome = new TextBox();
            txtbxWelcome.Text = "Welcome. Time to crush your productivity goals.";
            txtbxWelcome.Location = new Point((App.width - App.menuWidth) / 2 - txtbxWelcome.Width / 2, App.height / 2 - txtbxWelcome.Height / 2);
            App.controls.add();
        }
    }
}
