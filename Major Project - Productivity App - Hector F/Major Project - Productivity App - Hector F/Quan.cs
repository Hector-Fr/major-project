using System;
using System.Xml;

namespace Major_Project___Productivity_App___Hector_F
{
    class Quan : Page
    {
        public Quan(App mainForm, string pageName, Button menuButton) : base(mainForm, pageName, menuButton) { }

        public void ShowQuan()
        {
            PictureBox quan = new PictureBox();
            quan.Image = Image.FromFile("C:\\Progamming Projects\\School\\Major Project\\major-project\\Major Project - Productivity App - Hector F\\Major Project - Productivity App - Hector F\\Resources\\Quan.png");
            quan.Location = new Point(200, 200);
            quan.SizeMode = PictureBoxSizeMode.AutoSize;
            quan.Visible = true;
            pagePanel.Controls.Add(quan);
        }
    }
}
