using System.Windows.Forms;

namespace KCSpy.View
{
    public partial class FrmConfig : Form
    {
        public FrmConfig() => InitializeComponent();

        private void FrmConfig_Load(object sender, System.EventArgs e) => prop.SelectedObject = Global.Config;
    }
}