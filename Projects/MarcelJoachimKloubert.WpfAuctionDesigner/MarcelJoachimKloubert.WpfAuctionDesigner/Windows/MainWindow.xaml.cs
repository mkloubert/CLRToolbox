// 
// WPF based tool to create product pages for auctions on eBay, e.g.
// Copyright (C) 2013  Marcel Joachim Kloubert
//     
// This library is free software; you can redistribute it and/or modify it
// under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 3 of the License, or (at
// your option) any later version.
//     
// This library is distributed in the hope that it will be useful, but
// WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// General Public License for more details.
//     
// You should have received a copy of the GNU General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301,
// USA.
// 


using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls.Ribbon;
using ICSharpCode.AvalonEdit;
using MarcelJoachimKloubert.WpfAuctionDesigner.Classes.Text;

namespace MarcelJoachimKloubert.WpfAuctionDesigner.Windows
{
    /// <summary>
    /// Code behind of "MainWindow.xaml".
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
        }

        #endregion Constructors

        #region Methods (8)

        // Private Methods (8) 

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            this.DataContext = null;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var vm = new ViewModel();
            vm.SurroundText += this.ViewModel_SurroundText;
            vm.InsertText += this.ViewModel_InsertText;
            vm.DoRedo += this.ViewModel_DoRedo;
            vm.DoUndo += this.ViewModel_DoUndo;

            //this.TextEditor_ArticleDescription.TextArea.TextView.LineTransformers.Add(new SpellingErrorColorizer());
            //this.TextEditor_BuyInfo.TextArea.TextView.LineTransformers.Add(new SpellingErrorColorizer());
            //this.TextEditor_DeliveryInfo.TextArea.TextView.LineTransformers.Add(new SpellingErrorColorizer());
            //this.TextEditor_Remarks.TextArea.TextView.LineTransformers.Add(new SpellingErrorColorizer());
            //this.TextEditor_ScopeOfSupply.TextArea.TextView.LineTransformers.Add(new SpellingErrorColorizer());

            this.DataContext = vm;
        }

        private TextEditor TryGetSelectedTextEditor()
        {
            return this.TryGetSelectedTextEditorFromVM(this.DataContext as ViewModel);
        }

        private TextEditor TryGetSelectedTextEditorFromVM(ViewModel vm)
        {
            if (vm != null)
            {
                switch (vm.SelectedTabIndex)
                {
                    case ViewModel.TABINDEX_DESCRIPTION:
                        return this.TextEditor_ArticleDescription;

                    case ViewModel.TABINDEX_SCOPEOFSUPPLY:
                        return this.TextEditor_ScopeOfSupply;

                    case ViewModel.TABINDEX_DELIVERYINFO:
                        return this.TextEditor_DeliveryInfo;

                    case ViewModel.TABINDEX_BUYINFO:
                        return this.TextEditor_BuyInfo;

                    case ViewModel.TABINDEX_REMARKS:
                        return this.TextEditor_Remarks;
                }
            }

            return null;
        }

        private void ViewModel_DoRedo(object sender, EventArgs e)
        {
            var textEditor = this.TryGetSelectedTextEditor();
            if (textEditor == null)
            {
                return;
            }

            textEditor.Redo();
        }

        private void ViewModel_DoUndo(object sender, EventArgs e)
        {
            var textEditor = this.TryGetSelectedTextEditor();
            if (textEditor == null)
            {
                return;
            }

            textEditor.Undo();
        }

        private void ViewModel_InsertText(object sender, TMInsertTextEventArgs e)
        {
            var vm = sender as ViewModel;

            var editor = this.TryGetSelectedTextEditorFromVM(vm);
            if (editor == null)
            {
                return;
            }

            if (e.Text != null)
            {
                editor.SelectedText = e.Text ?? string.Empty;
            }
        }

        private void ViewModel_SurroundText(object sender, TMSurroundTextEventArgs e)
        {
            var vm = sender as ViewModel;

            var editor = this.TryGetSelectedTextEditorFromVM(vm);
            if (editor == null)
            {
                return;
            }

            var selStart = editor.SelectionStart;
            var selLength = editor.SelectionLength;

            var prefix = e.Prefix ?? string.Empty;
            var text = editor.SelectedText ?? string.Empty;
            var suffix = e.Suffix ?? string.Empty;

            var newSelectedText = string.Format("{0}{1}{2}",
                                                prefix,
                                                text,
                                                suffix);

            editor.SelectedText = newSelectedText;
            editor.Select(start: selStart + prefix.Length,
                          length: selLength);
        }

        #endregion Methods
    }
}
