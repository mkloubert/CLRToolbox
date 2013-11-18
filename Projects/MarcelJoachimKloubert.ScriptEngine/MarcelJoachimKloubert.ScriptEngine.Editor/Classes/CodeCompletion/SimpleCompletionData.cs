using System;
using System.Windows.Media;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;

namespace MarcelJoachimKloubert.ScriptEngine.Editor.Classes.CodeCompletion
{
    /// <summary>
    /// Simple implementation of <see cref="ICompletionData" /> interface.
    /// </summary>
    public sealed class SimpleCompletionData : ICompletionData
    {
        #region Properties (6)

        /// <summary>
        /// Gets or sets the action for <see cref="SimpleCompletionData.Complete(TextArea, ISegment, EventArgs)" /> method.
        /// </summary>
        public Action<ICompletionData, TextArea, ISegment, EventArgs> CompleteAction
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ICompletionData.Content" />
        public object Content
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ICompletionData.Description" />
        public object Description
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ICompletionData.Image" />
        public ImageSource Image
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ICompletionData.Priority" />
        public double Priority
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ICompletionData.Text" />
        public string Text
        {
            get;
            set;
        }

        #endregion Properties

        #region Methods (1)

        // Public Methods (1) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ICompletionData.Complete(TextArea, ISegment, EventArgs)" />
        public void Complete(TextArea textArea, ISegment completionSegment, EventArgs insertionRequestEventArgs)
        {
            var action = this.CompleteAction;
            if (action != null)
            {
                action(this, textArea, completionSegment, insertionRequestEventArgs);
            }
        }

        #endregion Methods
    }
}
