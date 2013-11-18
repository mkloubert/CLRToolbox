using System;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;
using MarcelJoachimKloubert.CLRToolbox.ComponentModel;
using MarcelJoachimKloubert.CLRToolbox.Windows.Input;
using MarcelJoachimKloubert.ScriptEngine.Editor.Classes.CodeCompletion;

namespace MarcelJoachimKloubert.ScriptEngine.Editor.ViewModels
{
    /// <summary>
    /// The model for the main view.
    /// </summary>
    public sealed class MainViewModel : NotificationObjectBase
    {
        #region Properties (1)

        /// <summary>
        /// Gets the command that opens the IntelliSense window.
        /// </summary>
        public SimpleCommand<TextArea> OpenIntellisenseCommand
        {
            get;
            private set;
        }

        #endregion Properties

        #region Methods (3)

        // Protected Methods (1) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="NotificationObjectBase.OnConstructor()" />
        protected override void OnConstructor()
        {
            base.OnConstructor();

            this.OpenIntellisenseCommand = new SimpleCommand<TextArea>(this.OpenIntellisense);
        }
        // Private Methods (2) 

        private static void ICompletionData_CompleteAction(ICompletionData data, TextArea ta, ISegment segment, EventArgs e)
        {
            ta.Document
              .Replace(segment, data.Text);
        }

        private void OpenIntellisense(TextArea textArea)
        {
            var win = new CompletionWindow(textArea);

            win.CompletionList.CompletionData.Add(new SimpleCompletionData()
                {
                    CompleteAction = ICompletionData_CompleteAction,
                    Content = "jupp1",
                    Description = "jupp1",
                    Priority = 1,
                    Text = "blä1",
                });

            win.CompletionList.CompletionData.Add(new SimpleCompletionData()
                {
                    CompleteAction = ICompletionData_CompleteAction,
                    Content = "jupp2",
                    Description = "jupp2",
                    Priority = 3,
                    Text = "blä2",
                });

            win.CompletionList.CompletionData.Add(new SimpleCompletionData()
                {
                    CompleteAction = ICompletionData_CompleteAction,
                    Content = "jupp3",
                    Description = "jupp3",
                    Priority = 0,
                    Text = "blä3",
                });

            win.Show();
        }

        #endregion Methods
    }
}
