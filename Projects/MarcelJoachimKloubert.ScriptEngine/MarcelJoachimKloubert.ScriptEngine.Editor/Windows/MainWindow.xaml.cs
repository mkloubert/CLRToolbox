using System.Windows.Controls.Ribbon;
using MarcelJoachimKloubert.ScriptEngine.Editor.ViewModels;

namespace MarcelJoachimKloubert.ScriptEngine.Editor.Windows
{
    /// <summary>
    /// Code behind for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow
    {
        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow" /> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();

            this.DataContext = new MainViewModel();
        }

        #endregion Constructors
    }
}
