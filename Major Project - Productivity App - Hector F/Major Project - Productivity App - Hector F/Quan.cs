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
            quan.Image = Image.FromFile("C:\\Progamming Projects\\School\\Major Project\\major-project\\Major Project - Productivity App - Hector F\\Major Project - Productivity App - Hector F\\Resources\\Van wering.png");
            quan.Location = new Point(0, 0);
            quan.Size = new Size(900, 700);
            quan.SizeMode = PictureBoxSizeMode.StretchImage;
            quan.Visible = true;
            pagePanel.Controls.Add(quan);
        }
    }
}
