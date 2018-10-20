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
        private const string XmlElementSenkaPicStorePath = @"SenkaPicPath";
        private readonly XmlConfigTool config = new XmlConfigTool(Path.Combine(Application.StartupPath, @"config.xml"));

        [Browsable(false)]
        public string SenkaPicPath => config.ReadConfig(XmlElementSenkaPicStorePath, Application.UserAppDataPath);

        [EditorBrowsable(EditorBrowsableState.Never), Category("路径"), Description("战果人事表的存储路径"), Editor(typeof(PropertyGridDirectoryItem), typeof(UITypeEditor))]
        public string 战果人事表存储路径
        {
            get => SenkaPicPath;
            set => config.SaveConfig(XmlElementSenkaPicStorePath, value);
        }
    }
}