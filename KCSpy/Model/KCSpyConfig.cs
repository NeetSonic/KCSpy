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
        private const string XmlElementExcelStorePath = @"ExcelPath";
        private const string XmlElementExcelTemplatePath = @"ExcelTemplatePath";
        private readonly XmlConfigTool config = new XmlConfigTool(Path.Combine(Application.StartupPath, @"config.xml"));

        [Browsable(false)]
        public string SenkaPicPath => config.ReadConfig(XmlElementSenkaPicStorePath, Application.UserAppDataPath);

        [Browsable(false)]
        public string ExcelPath => config.ReadConfig(XmlElementExcelStorePath, Application.UserAppDataPath);

        [Browsable(false)]
        public string ExcelTemplatePath => config.ReadConfig(XmlElementExcelTemplatePath, Application.UserAppDataPath);

        [EditorBrowsable(EditorBrowsableState.Never), Category("路径"), Description("战果人事表的存储路径"), Editor(typeof(PropertyGridDirectoryItem), typeof(UITypeEditor))]
        public string 战果人事表存储路径
        {
            get => SenkaPicPath;
            set => config.SaveConfig(XmlElementSenkaPicStorePath, value);
        }
        [EditorBrowsable(EditorBrowsableState.Never), Category("路径"), Description("Excel文件存储路径"), Editor(typeof(PropertyGridDirectoryItem), typeof(UITypeEditor))]
        public string Excel文件存储路径
        {
            get => ExcelPath;
            set => config.SaveConfig(XmlElementExcelStorePath, value);
        }
        [EditorBrowsable(EditorBrowsableState.Never), Category("路径"), Description("Excel文件模版存储路径"), Editor(typeof(PropertyGridFileItem), typeof(UITypeEditor))]
        public string Excel文件模版存储路径
        {
            get => ExcelTemplatePath;
            set => config.SaveConfig(XmlElementExcelTemplatePath, value);
        }
    }
}