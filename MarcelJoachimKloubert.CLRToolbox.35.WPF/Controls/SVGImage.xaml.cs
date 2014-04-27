// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Data.Documents.Svg;
using System.Windows;
using System.Windows.Controls;

namespace MarcelJoachimKloubert.CLRToolbox.Windows.Controls
{
    /// <summary>
    /// Code behind for SVGImage.xaml
    /// </summary>
    public partial class SVGImage : UserControl
    {
        #region Fields (1) 

        /// <summary>
        /// The dependancy property for <see cref="SVGImage.Document" />.
        /// </summary>
        public static readonly DependencyProperty DocumentProperty =
            DependencyProperty.Register("Document",
                                        typeof(SvgDocument),
                                        typeof(SVGImage),
                                        new FrameworkPropertyMetadata((object)null));

        #endregion Fields 

        #region Constructors (1) 

        /// <summary>
        /// Initializes a new instance of the <see cref="SVGImage" /> class.
        /// </summary>
        public SVGImage()
        {
            this.InitializeComponent();
        }

        #endregion Constructors 

        #region Properties (1) 

        /// <summary>
        /// Gets or sets the 
        /// </summary>
        public SvgDocument Document
        {
            get { return (SvgDocument)this.GetValue(DocumentProperty); }

            set
            {
                this.SetValue(DocumentProperty, value);
                this.UpdateImageFromDocument(value);
            }
        }

        #endregion Properties 

        #region Methods (1)
        
        private void UpdateImageFromDocument(SvgDocument document)
        {
            this.Content = null;
            if (document == null)
            {
                return;
            }

            var newCanvas = new Canvas();

            this.Content = newCanvas;
        }

        #endregion Methods
    }
}