
using System.Transactions;
using System.Windows.Forms;

namespace Major_Project___Productivity_App___Hector_F
{
    internal class Page
    {
        public string name;
        public Button menuButton;
        public Panel panel;
        bool isVisible = false;

        public Page(string _name)
        {
            name = _name;
            menuButton = new Button();
            panel = new Panel();
            isVisible = false;
            panel.Visible = false;
        }

        public void Show(bool enable)
        {
            isVisible = enable;
            panel.Visible = isVisible;
        }
    }
}
