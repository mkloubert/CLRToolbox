// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System.Windows;
using System.Windows.Input;

namespace MarcelJoachimKloubert.CLRToolbox.Windows.Controls
{
    /// <summary>
    /// A read-to-use draggable and borderless <see cref="Window" />.
    /// </summary>
    public class BorderlessMoveableWindow : Window
    {
        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="BorderlessMoveableWindow" /> class.
        /// </summary>
        public BorderlessMoveableWindow()
        {
            this.MouseDown += this.BorderlessMoveableWindow_MouseDown;
            this.ResizeMode = ResizeMode.NoResize;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.WindowStyle = WindowStyle.None;
        }

        #endregion Constructors

        #region Methods (2)

        // Protected Methods (1) 

        /// <summary>
        /// That method is invoked when that window is dragged.
        /// </summary>
        /// <param name="e">The event arguments of the underlying <see cref="UIElement.MouseDown" /> event.</param>
        protected virtual void OnDragWindow(MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        // Private Methods (1) 

        private void BorderlessMoveableWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.OnDragWindow(e);
        }

        #endregion Methods
    }
}