using System;

namespace Major_Project___Productivity_App___Hector_F
{
    public class Page
    {
        public Form app;

        public string name;
        public Panel panel;
        public Button btnMenuButton;
        public bool visible;

        public Page(string _name)
        {
            app = Form.
            name = _name;
            panel = new Panel();
            btnMenuButton = new Button();
            visible = false;
        }

        public void Show(bool enable)
        {
            visible = enable;
            panel.Visible = visible;
        }
    }
}
