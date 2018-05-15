using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Windows.Forms;
using Neetsonic.DataStructure;
using Neetsonic.Tool;

namespace KCSpy.Model
{
    public sealed class KCSpyConfig
    {
        private const string XmlElementStorePath = @"StorePath";
        private readonly XmlConfigTool config = new XmlConfigTool(Path.Combine(Application.StartupPath, @"config.xml"));

        [Browsable(false)]
        public string DBPath => config.ReadConfig(XmlElementStorePath, Application.UserAppDataPath);

        [EditorBrowsable(EditorBrowsableState.Never), Category("路径"), Description("数据库文件的存储路径"), Editor(typeof(PropertyGridFileItem), typeof(UITypeEditor))]
        public string 数据库文件路径
        {
            get => DBPath;
            set => config.SaveConfig(XmlElementStorePath, value);
        }
    }
}