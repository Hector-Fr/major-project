
namespace Major_Project___Productivity_App___Hector_F
{
    class HomePage : Page
    {
        public HomePage(App mainForm, string pageName, Button menuButton) : base(mainForm, pageName, menuButton) { }

        public override void Create(string pageName)
        {
            base.Create(pageName);

            CreateHomePage();
        }

        private void CreateHomePage()
        {
            Label lblTitle = new Label();
            lblTitle.Text = "PRODUCTIV";
            lblTitle.Size = new Size(400, 200);
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(400, 50);
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            lblTitle.Font = new Font(mainForm.Font.FontFamily, 30, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            pagePanel.Controls.Add(lblTitle);

            Label lblWelcome = new Label();
            lblWelcome.Text = "Welcome. Time to crush your productivity goals!";
            lblWelcome.Size = new Size(400, 200);
            lblWelcome.Location = new Point(400, 500);
            lblWelcome.TextAlign = ContentAlignment.MiddleCenter;
            lblWelcome.Font = new Font(mainForm.Font.FontFamily, 14, FontStyle.Italic);
            lblWelcome.ForeColor = Color.White;
            pagePanel.Controls.Add(lblWelcome);
        }
    }
}
