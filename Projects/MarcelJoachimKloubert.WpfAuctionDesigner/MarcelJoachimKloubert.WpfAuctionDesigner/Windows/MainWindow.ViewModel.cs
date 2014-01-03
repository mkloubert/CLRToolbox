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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using MarcelJoachimKloubert.CLRToolbox.ComponentModel;
using MarcelJoachimKloubert.CLRToolbox.Extensions;
using MarcelJoachimKloubert.CLRToolbox.Windows.Input;
using MarcelJoachimKloubert.WpfAuctionDesigner.Classes;
using MarcelJoachimKloubert.WpfAuctionDesigner.Classes.Collections;
using MarcelJoachimKloubert.WpfAuctionDesigner.Classes.Controls;
using MarcelJoachimKloubert.WpfAuctionDesigner.Classes.Controls.Items;
using MarcelJoachimKloubert.WpfAuctionDesigner.Classes.Documents;
using MarcelJoachimKloubert.WpfAuctionDesigner.Classes.Drawing;
using MarcelJoachimKloubert.WpfAuctionDesigner.Classes.IO;
using MarcelJoachimKloubert.WpfAuctionDesigner.Classes.Text;
using Microsoft.Win32;
using Xceed.Wpf.Toolkit;
using WpfToolKit = Xceed.Wpf.Toolkit;

namespace MarcelJoachimKloubert.WpfAuctionDesigner.Windows
{
    partial class MainWindow
    {
        #region Nested Classes (1)

        /// <summary>
        /// View model for <see cref="MainWindow" />.
        /// </summary>
        public sealed class ViewModel : NotificationObjectBase
        {
            #region Fields (48)

            private ObservableCollection<ColorItem> _availableFontColors;
            private ObservableCollection<TMValueComboBoxItem> _availableFontSizes;
            private SimpleCommand<object> _copyToClipboardCommand;
            private const string _DEFAULT_DOC_FILENAME = "default.xml";
            private const string _DIALOG_FILTER = "Auction Designer Dateien (*.ad)|*.ad|Alle Dateien (*.*)|*.*";
            private TMAuctionDesignerDocument _document;
            private FileInfo _documentFile;
            private IHighlightingDefinition _editorSyntax;
            private SimpleCommand<object> _exitAppCommand;
            private SimpleCommand<object> _insertAlignCenterCommand;
            private SimpleCommand<object> _insertAlignJustifyCommand;
            private SimpleCommand<object> _insertAlignLeftCommand;
            private SimpleCommand<object> _insertAlignRightCommand;
            private SimpleCommand<object> _insertBoldCommand;
            private SimpleCommand<Color> _insertFontColorCommand;
            private SimpleCommand<TMValueComboBoxItem> _insertFontSizeCommand;
            private SimpleCommand<object> _insertItalicCommand;
            private SimpleCommand<object> _insertListItemCommand;
            private SimpleCommand<object> _insertMoveRightCommand;
            private SimpleCommand<object> _insertPictureFromClipboardCommand;
            private SimpleCommand<object> _insertPicturesCommand;
            private SimpleCommand<object> _insertStrikeThroughCommand;
            private SimpleCommand<object> _insertUnderlineCommand;
            private SimpleCommand<object> _newDocumentCommand;
            private SimpleCommand<object> _openDocumentCommand;
            private PreviewWindow _previewWindow;
            /// <summary>
            /// Gets the list of recent files.
            /// </summary>
            private ObservableCollection<TMRecentFile> _recentFiles;
            private SimpleCommand<object> _redoCommand;
            private SimpleCommand<object> _saveAsDefaultCommand;
            private SimpleCommand<object> _saveDocumentCommand;
            private Color _selectedFontColor;
            private TMValueComboBoxItem _selectedFontSize;
            private int _selectedTabIndex;
            private SimpleCommand<object> _undoCommand;
            private const string _WINDOW_TITLE_PREFIX = "Auction Designer by Marcel J. Kloubert";
            private string _windowTitle;
            /// <summary>
            /// Stores the list of available font colors.
            /// </summary>
            public static readonly ColorItem[] FONT_COLORS =
            {
                new ColorItem(Color.FromRgb(0, 0, 0), "Schwarz"),
                new ColorItem(Color.FromRgb(0, 0, 128), "Dunkelblau"),
                new ColorItem(Color.FromRgb(0, 128, 0), "Dunkelgrün"),
                new ColorItem(Color.FromRgb(0, 128, 128), "Blaugrün"),
                new ColorItem(Color.FromRgb(128, 0, 0), "Dunkelrot"),
                new ColorItem(Color.FromRgb(128, 0, 128), "Violett"),
                new ColorItem(Color.FromRgb(128, 128, 0), "Dunkelgelb"),
                new ColorItem(Color.FromRgb(192, 192, 192), "Grau"),
                new ColorItem(Color.FromRgb(128, 128, 128), "Dunkelgrau"),
                new ColorItem(Color.FromRgb(0, 0, 255), "Blau"),
                new ColorItem(Color.FromRgb(0, 255, 0), "Grün"),
                new ColorItem(Color.FromRgb(0, 255, 255), "Aquablau"),
                new ColorItem(Color.FromRgb(255, 0, 0), "Rot"),
                new ColorItem(Color.FromRgb(255, 0, 255), "Pink"),
                new ColorItem(Color.FromRgb(255, 255, 0), "Gelb"),
                new ColorItem(Color.FromRgb(255, 255, 255), "Weiß"),
            };
            /// <summary>
            /// Internal / CSS class name for big font size.
            /// </summary>
            public const string FONT_SIZE_BIG = "grosseSchrift";
            /// <summary>
            /// Internal / CSS class name for formal font size.
            /// </summary>
            public const string FONT_SIZE_NORMAL = "normaleSchrift";
            /// <summary>
            /// Internal / CSS class name for small font size.
            /// </summary>
            public const string FONT_SIZE_SMALL = "kleineSchrift";
            /// <summary>
            /// Internal / CSS class name for very big font size.
            /// </summary>
            public const string FONT_SIZE_VERYBIG = "sehrGrosseSchrift";
            /// <summary>
            /// Internal / CSS class name for very small font size.
            /// </summary>
            public const string FONT_SIZE_VERYSMALL = "sehrKleineSchrift";
            /// <summary>
            /// Stores the list of font sizes.
            /// </summary>
            public static readonly TMValueComboBoxItem[] FONT_SIZES = 
            {
                new TMValueComboBoxItem("sehr_grosse_schrift", i => "Sehr große Schrift")
                {
                    Tag = FONT_SIZE_VERYBIG,
                },
                new TMValueComboBoxItem("grosse_schrift", i => "Große Schrift")
                {
                    Tag = FONT_SIZE_BIG,
                },
                new TMValueComboBoxItem("normale_schrift", i => "Normale Schrift")
                {
                    Tag = FONT_SIZE_NORMAL,
                },
                new TMValueComboBoxItem("kleine_schrift", i => "Kleine Schrift")
                {
                    Tag = FONT_SIZE_SMALL,
                },
                new TMValueComboBoxItem("sehr_kleine_schrift", i => "Sehr kleine Schrift")
                {
                    Tag = FONT_SIZE_VERYSMALL,
                },
            };
            /// <summary>
            /// Stores the zero-based tab index that contains the text box
            /// with the buy information.
            /// </summary>
            public const int TABINDEX_BUYINFO = 4;
            /// <summary>
            /// Stores the zero-based tab index that contains the text box
            /// with the delivery information.
            /// </summary>
            public const int TABINDEX_DELIVERYINFO = 3;
            /// <summary>
            /// Stores the zero-based tab index that contains the text box
            /// with the article decription.
            /// </summary>
            public const int TABINDEX_DESCRIPTION = 1;
            /// <summary>
            /// Stores the zero-based tab index that contains the text box
            /// with the remarks.
            /// </summary>
            public const int TABINDEX_REMARKS = 5;
            /// <summary>
            /// Stores the zero-based tab index that contains the text box
            /// with the scope of supply.
            /// </summary>
            public const int TABINDEX_SCOPEOFSUPPLY = 2;

            #endregion Fields

            #region Constructors (1)

            ~ViewModel()
            {
                var previewWindow = this.PreviewWindow;
                if (previewWindow != null)
                {
                    previewWindow.Close(true);
                    this.PreviewWindow = null;
                }
            }

            #endregion Constructors

            #region Properties (35)

            /// <summary>
            /// Gets the list of available font colors.
            /// </summary>
            public ObservableCollection<ColorItem> AvailableFontColors
            {
                get { return this._availableFontColors; }

                private set { this.SetProperty(ref this._availableFontColors, value); }
            }

            /// <summary>
            /// Gets the list of available font sizes.
            /// </summary>
            public ObservableCollection<TMValueComboBoxItem> AvailableFontSizes
            {
                get { return this._availableFontSizes; }

                private set { this.SetProperty(ref this._availableFontSizes, value); }
            }

            /// <summary>
            /// Gets if text editor functions can be used or not.
            /// </summary>
            public bool CanUseTextEditorFunctions
            {
                get
                {
                    switch (this._selectedTabIndex)
                    {
                        case TABINDEX_DESCRIPTION:
                        case TABINDEX_SCOPEOFSUPPLY:
                        case TABINDEX_DELIVERYINFO:
                        case TABINDEX_BUYINFO:
                        case TABINDEX_REMARKS:
                            return true;
                    }

                    return false;
                }
            }

            /// <summary>
            /// Gets the command that copies current HTML source to clipboard.
            /// </summary>
            public SimpleCommand<object> CopyToClipboardCommand
            {
                get { return this._copyToClipboardCommand; }

                private set { this.SetProperty(ref this._copyToClipboardCommand, value); }
            }

            /// <summary>
            /// Gets the current document.
            /// </summary>
            public TMAuctionDesignerDocument Document
            {
                get { return this._document; }

                private set { this.SetProperty(ref this._document, value); }
            }

            /// <summary>
            /// Gets the current document file.
            /// </summary>
            public FileInfo DocumentFile
            {
                get { return this._documentFile; }

                private set
                {
                    if (this.SetProperty(ref this._documentFile, value))
                    {
                        if (value != null)
                        {
                            this.WindowTitle = string.Format("{0} <{1}>",
                                                             _WINDOW_TITLE_PREFIX,
                                                             value.Name);
                        }
                        else
                        {
                            this.WindowTitle = _WINDOW_TITLE_PREFIX;
                        }
                    }
                }
            }

            /// <summary>
            /// Gets the syntax definition for the text editors.
            /// </summary>
            public IHighlightingDefinition EditorSyntax
            {
                get { return this._editorSyntax; }

                private set { this.SetProperty(ref this._editorSyntax, value); }
            }

            /// <summary>
            /// Gets the command that exits the application.
            /// </summary>
            public SimpleCommand<object> ExitAppCommand
            {
                get { return this._exitAppCommand; }

                private set { this.SetProperty(ref this._exitAppCommand, value); }
            }

            /// <summary>
            /// Gets the command that inserts an align center tag.
            /// </summary>
            public SimpleCommand<object> InsertAlignCenterCommand
            {
                get { return this._insertAlignCenterCommand; }

                private set { this.SetProperty(ref this._insertAlignCenterCommand, value); }
            }

            /// <summary>
            /// Gets the command that inserts a justify tag.
            /// </summary>
            public SimpleCommand<object> InsertAlignJustifyCommand
            {
                get { return this._insertAlignJustifyCommand; }

                private set { this.SetProperty(ref this._insertAlignJustifyCommand, value); }
            }

            /// <summary>
            /// Gets the command that inserts an align left tag.
            /// </summary>
            public SimpleCommand<object> InsertAlignLeftCommand
            {
                get { return this._insertAlignLeftCommand; }

                private set { this.SetProperty(ref this._insertAlignLeftCommand, value); }
            }

            /// <summary>
            /// Gets the command that inserts an align right tag.
            /// </summary>
            public SimpleCommand<object> InsertAlignRightCommand
            {
                get { return this._insertAlignRightCommand; }

                private set { this.SetProperty(ref this._insertAlignRightCommand, value); }
            }

            /// <summary>
            /// Gets the command to insert a bold tag.
            /// </summary>
            public SimpleCommand<object> InsertBoldCommand
            {
                get { return this._insertBoldCommand; }

                private set { this.SetProperty(ref this._insertBoldCommand, value); }
            }

            /// <summary>
            /// Gets the command that inserts a font color tag.
            /// </summary>
            public SimpleCommand<Color> InsertFontColorCommand
            {
                get { return this._insertFontColorCommand; }

                private set { this.SetProperty(ref this._insertFontColorCommand, value); }
            }

            /// <summary>
            /// Gets the command that inserts the font size.
            /// </summary>
            public SimpleCommand<TMValueComboBoxItem> InsertFontSizeCommand
            {
                get { return this._insertFontSizeCommand; }

                set { this.SetProperty(ref this._insertFontSizeCommand, value); }
            }

            /// <summary>
            /// Gets the command to insert an italic tag.
            /// </summary>
            public SimpleCommand<object> InsertItalicCommand
            {
                get { return this._insertItalicCommand; }

                private set { this.SetProperty(ref this._insertItalicCommand, value); }
            }

            /// <summary>
            /// Gets the command for inserting a list item.
            /// </summary>
            public SimpleCommand<object> InsertListItemCommand
            {
                get { return this._insertListItemCommand; }

                private set { this.SetProperty(ref this._insertListItemCommand, value); }
            }

            /// <summary>
            /// Gets the command for moving a block right.
            /// </summary>
            public SimpleCommand<object> InsertMoveRightCommand
            {
                get { return this._insertMoveRightCommand; }

                private set { this.SetProperty(ref this._insertMoveRightCommand, value); }
            }

            /// <summary>
            /// Gets the command that inserts a picture from clipboard.
            /// </summary>
            public SimpleCommand<object> InsertPictureFromClipboardCommand
            {
                get { return this._insertPictureFromClipboardCommand; }

                private set { this.SetProperty(ref this._insertPictureFromClipboardCommand, value); }
            }

            /// <summary>
            /// Gets the command for inserting pictures.
            /// </summary>
            public SimpleCommand<object> InsertPicturesCommand
            {
                get { return this._insertPicturesCommand; }

                private set { this.SetProperty(ref this._insertPicturesCommand, value); }
            }

            /// <summary>
            /// Gets the command to insert a strike through tag.
            /// </summary>
            public SimpleCommand<object> InsertStrikeThroughCommand
            {
                get { return this._insertStrikeThroughCommand; }

                private set { this.SetProperty(ref this._insertStrikeThroughCommand, value); }
            }

            /// <summary>
            /// Gets the command to insert an underline tag.
            /// </summary>
            public SimpleCommand<object> InsertUnderlineCommand
            {
                get { return this._insertUnderlineCommand; }

                private set { this.SetProperty(ref this._insertUnderlineCommand, value); }
            }

            /// <summary>
            /// Gets the command that creates a new document.
            /// </summary>
            public SimpleCommand<object> NewDocumentCommand
            {
                get { return this._newDocumentCommand; }

                private set { this.SetProperty(ref this._newDocumentCommand, value); }
            }

            /// <summary>
            /// Gets the command that opens an existing document.
            /// </summary>
            public SimpleCommand<object> OpenDocumentCommand
            {
                get { return this._openDocumentCommand; }

                private set { this.SetProperty(ref this._openDocumentCommand, value); }
            }

            /// <summary>
            /// Gets the current preview window instance.
            /// </summary>
            public PreviewWindow PreviewWindow
            {
                get { return this._previewWindow; }

                private set { this.SetProperty(ref this._previewWindow, value); }
            }

            /// <summary>
            /// Gets the list of recent files.
            /// </summary>
            public ObservableCollection<TMRecentFile> RecentFiles
            {
                get { return this._recentFiles; }

                private set { this.SetProperty(ref this._recentFiles, value); }
            }

            /// <summary>
            /// Gets the command that does a REDO in a text editor.
            /// </summary>
            public SimpleCommand<object> RedoCommand
            {
                get { return this._redoCommand; }

                private set { this.SetProperty(ref this._redoCommand, value); }
            }

            /// <summary>
            /// Gets the command that saves the current document as default one.
            /// </summary>
            public SimpleCommand<object> SaveAsDefaultCommand
            {
                get { return this._saveAsDefaultCommand; }

                private set { this.SetProperty(ref this._saveAsDefaultCommand, value); }
            }

            /// <summary>
            /// Gets the command that the current data to document.
            /// </summary>
            public SimpleCommand<object> SaveDocumentCommand
            {
                get { return this._saveDocumentCommand; }

                private set { this.SetProperty(ref this._saveDocumentCommand, value); }
            }

            /// <summary>
            /// Gets or sets the selected font color.
            /// </summary>
            public Color SelectedFontColor
            {
                get { return this._selectedFontColor; }

                set { this.SetProperty(ref this._selectedFontColor, value); }
            }

            /// <summary>
            /// Gets the currently selected font size.
            /// </summary>
            public TMValueComboBoxItem SelectedFontSize
            {
                get { return this._selectedFontSize; }

                set { this.SetProperty(ref this._selectedFontSize, value); }
            }

            /// <summary>
            /// Sets the selected recent file to open it.
            /// </summary>
            public TMRecentFile SelectedRecentFile
            {
                set
                {
                    if (value == null)
                    {
                        return;
                    }

                    try
                    {
                        var file = value.GetInfo();
                        if (file.Exists)
                        {
                            this.Document = TMAuctionDesignerDocument.FromFile(file);
                            this.DocumentFile = file;

                            this.InsertRecentFile(value);
                        }
                        else
                        {
                            //TODO
                        }
                    }
                    catch
                    {
                        //TODO
                    }
                    finally
                    {
                        this.TrySaveRecentFileList();
                    }
                }
            }

            /// <summary>
            /// Gets or sets the index of the selected tab of the main tab control.
            /// </summary>
            public int SelectedTabIndex
            {
                get { return this._selectedTabIndex; }

                set
                {
                    if (this.SetProperty(ref this._selectedTabIndex, value))
                    {
                        TextDocument toolBarTextDocument = null;

                        // CurrentToolBarTextDocument
                        var doc = this.Document;
                        if (doc != null)
                        {
                            switch (value)
                            {
                                case TABINDEX_BUYINFO:
                                    toolBarTextDocument = doc.BuyInfo;
                                    break;

                                case TABINDEX_DELIVERYINFO:
                                    toolBarTextDocument = doc.DeliveryInfo;
                                    break;

                                case TABINDEX_DESCRIPTION:
                                    toolBarTextDocument = doc.ArticleDescription;
                                    break;

                                case TABINDEX_REMARKS:
                                    toolBarTextDocument = doc.Remarks;
                                    break;

                                case TABINDEX_SCOPEOFSUPPLY:
                                    toolBarTextDocument = doc.ScopeOfSupply;
                                    break;
                            }
                        }

                        this.OnPropertyChanged(() => this.CanUseTextEditorFunctions);
                    }
                }
            }

            /// <summary>
            /// Gets the command that does an UNDO in a text editor.
            /// </summary>
            public SimpleCommand<object> UndoCommand
            {
                get { return this._undoCommand; }

                private set { this.SetProperty(ref this._undoCommand, value); }
            }

            /// <summary>
            /// Gets the title of the window.
            /// </summary>
            public string WindowTitle
            {
                get { return this._windowTitle; }

                private set { this.SetProperty(ref this._windowTitle, value); }
            }

            #endregion Properties

            #region Delegates and Events (4)

            // Events (4) 

            public event EventHandler DoRedo;

            public event EventHandler DoUndo;

            /// <summary>
            /// Is invoked if a selected text in a textbox should be replaced
            /// by a text.
            /// </summary>
            public event TMInsertTextHandler InsertText;

            /// <summary>
            /// Is invoked if a selected text in a textbox should be surrounded
            /// by a prefix and a suffix.
            /// </summary>
            public event TMSurroundTextEventHandler SurroundText;

            #endregion Delegates and Events

            #region Methods (40)

            // Protected Methods (1) 

            protected override void OnConstructor()
            {
                this.InitializeFontData();
                this.InitializeEditorSyntaxDefinition();
                this.InitializeRecentFiles();

                this.DocumentFile = null;
                this.WindowTitle = _WINDOW_TITLE_PREFIX;

                if (!this.InitializeDefault())
                {
                    this.ResetDocument();
                }

                this.InitializePreviewWindow();

                this.InitializeCommands();
            }
            // Private Methods (39) 

            private void CopyToClipboard(object param)
            {
                try
                {
                    string html = null;

                    var doc = this.Document;
                    if (doc != null)
                    {
                        html = doc.HtmlOutputSource;
                    }

                    Clipboard.SetText(html ?? string.Empty);

                    WpfToolKit.MessageBox
                          .Show("Der HTML-Quelltext wurde erfolgreich in die Zwischenablage kopiert.",
                                "HTML in Zwischenablage kopieren",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
                }
                catch
                {
                    //TODO
                }
            }

            private void ExitApp(object param)
            {
                try
                {
                    Application.Current.Shutdown();
                }
                catch
                {
                    //TODO
                }
            }

            private FileInfo GetDefaultFile()
            {
                var appDataDir = TMAppHelper.GetAppDataDirectory(true);

                return new FileInfo(Path.Combine(appDataDir.FullName, "default.xml"));
            }

            private FileInfo GetRecentFile()
            {
                var appDataDir = TMAppHelper.GetAppDataDirectory(true);

                return new FileInfo(Path.Combine(appDataDir.FullName, "recent.txt"));
            }

            private void InitializeCommands()
            {
                this.NewDocumentCommand = new SimpleCommand<object>(this.NewDocument);
                this.OpenDocumentCommand = new SimpleCommand<object>(this.OpenDocument);
                this.SaveDocumentCommand = new SimpleCommand<object>(this.SaveDocument);
                this.CopyToClipboardCommand = new SimpleCommand<object>(this.CopyToClipboard);
                this.SaveAsDefaultCommand = new SimpleCommand<object>(this.SaveAsDefault);
                this.ExitAppCommand = new SimpleCommand<object>(this.ExitApp);

                this.InsertBoldCommand = new SimpleCommand<object>(this.InsertBold);
                this.InsertItalicCommand = new SimpleCommand<object>(this.InsertItalic);
                this.InsertUnderlineCommand = new SimpleCommand<object>(this.InsertUnderline);
                this.InsertStrikeThroughCommand = new SimpleCommand<object>(this.InsertStrikeThrough);

                this.InsertListItemCommand = new SimpleCommand<object>(this.InsertListItem);

                this.InsertAlignLeftCommand = new SimpleCommand<object>(this.InsertAlignLeft);
                this.InsertAlignCenterCommand = new SimpleCommand<object>(this.InsertAlignCenter);
                this.InsertAlignRightCommand = new SimpleCommand<object>(this.InsertAlignRight);
                this.InsertAlignJustifyCommand = new SimpleCommand<object>(this.InsertAlignJustify);

                this.InsertMoveRightCommand = new SimpleCommand<object>(this.InsertMoveRight);

                this.InsertPicturesCommand = new SimpleCommand<object>(this.InsertPictures);
                this.InsertPictureFromClipboardCommand = new SimpleCommand<object>(this.InsertPictureFromClipboard);

                this.InsertFontColorCommand = new SimpleCommand<Color>(this.InsertFontColor);
                this.InsertFontSizeCommand = new SimpleCommand<TMValueComboBoxItem>(this.InsertFontSize);

                this.RedoCommand = new SimpleCommand<object>(this.Redo);
                this.UndoCommand = new SimpleCommand<object>(this.Undo);
            }

            private bool InitializeDefault()
            {
                var result = false;

                var defaultFile = this.GetDefaultFile();
                if (defaultFile.Exists)
                {
                    try
                    {
                        this.Document = TMAuctionDesignerDocument.FromFile(defaultFile);
                        result = true;
                    }
                    catch
                    {
                        // ignore errors here
                    }
                }

                return result;
            }

            private void InitializeEditorSyntaxDefinition()
            {
                // editor syntax
                using (var stream = this.GetType().Assembly.GetManifestResourceStream("MarcelJoachimKloubert.WpfAuctionDesigner._Res.AvalonEdit.Editor.xml"))
                {
                    var xmlDoc = XDocument.Load(stream);

                    // extend syntax definition dynamically
                    {
                        XElement fontXmlRootTag;
                        using (var reader = xmlDoc.CreateReader())
                        {
                            var nsManager = new XmlNamespaceManager(reader.NameTable);
                            nsManager.AddNamespace("tm", "http://icsharpcode.net/sharpdevelop/syntaxdefinition/2008");

                            var fontColorXmlPlaceholderTag = xmlDoc.XPathSelectElements(@"//tm:SyntaxDefinition/tm:RuleSet/tm:Keywords/tm:Word[text() = 'UPcuJuU6t7ybW50VpgTgScQGBKQL2Omz']",
                                                                                        nsManager)
                                                                   .First();

                            fontXmlRootTag = fontColorXmlPlaceholderTag.Parent;

                            fontColorXmlPlaceholderTag.Remove();
                        }

                        // font colors
                        foreach (var color in FONT_COLORS)
                        {
                            var tagName = TMEditorHelper.ToFontColorTagName(color);

                            var wordTagName = XName.Get("Word", "http://icsharpcode.net/sharpdevelop/syntaxdefinition/2008");

                            var wordElementOpen = new XElement(wordTagName)
                            {
                                Value = string.Format("{{{0}}}", tagName),
                            };
                            fontXmlRootTag.Add(wordElementOpen);

                            var wordElementClose = new XElement(wordTagName)
                            {
                                Value = string.Format("{{/{0}}}", tagName),
                            };
                            fontXmlRootTag.Add(wordElementClose);
                        }

                        // font sizes
                        foreach (var size in FONT_SIZES)
                        {
                            var tagName = size.Value.AsString(true);

                            var wordTagName = XName.Get("Word", "http://icsharpcode.net/sharpdevelop/syntaxdefinition/2008");

                            var wordElementOpen = new XElement(wordTagName)
                            {
                                Value = string.Format("{{{0}}}", tagName),
                            };
                            fontXmlRootTag.Add(wordElementOpen);

                            var wordElementClose = new XElement(wordTagName)
                            {
                                Value = string.Format("{{/{0}}}", tagName),
                            };
                            fontXmlRootTag.Add(wordElementClose);
                        }
                    }

                    using (var reader = xmlDoc.CreateReader())
                    {
                        this.EditorSyntax = HighlightingLoader.Load(reader, HighlightingManager.Instance);
                    }
                }
            }

            private void InitializeFontData()
            {
                // font colors
                this.AvailableFontColors = new TMSynchronizedObservableCollection<ColorItem>(syncRoot: this._SYNC,
                                                                                             collection: FONT_COLORS);
                this.SelectedFontColor = this.AvailableFontColors[0].Color;

                // font sizes
                this.AvailableFontSizes = new TMSynchronizedObservableCollection<TMValueComboBoxItem>(syncRoot: this._SYNC,
                                                                                                      collection: FONT_SIZES);
                this.SelectedFontSize = this.AvailableFontSizes
                                            .Single(fs => object.Equals(fs.Tag, FONT_SIZE_NORMAL));
            }

            private void InitializePreviewWindow()
            {
                var win = new PreviewWindow();
                win.DataContext = new PreviewWindow.ViewModel(this);
                win.Closed += (sender, e) =>
                {
                    this.PreviewWindow = null;
                };

                this.PreviewWindow = win;
            }

            private void InitializeRecentFiles()
            {
                this.RecentFiles = new TMSynchronizedObservableCollection<TMRecentFile>(syncRoot: this._SYNC);

                try
                {
                    var recentFile = this.GetRecentFile();
                    if (recentFile.Exists)
                    {
                        using (var stream = recentFile.OpenRead())
                        {
                            using (var reader = new StreamReader(stream, Encoding.UTF8))
                            {
                                string line;
                                while ((line = reader.ReadLine()) != null)
                                {
                                    if (!string.IsNullOrWhiteSpace(line))
                                    {
                                        // string => TMRecentFile
                                        this.RecentFiles.Add(line);
                                    }
                                }
                            }
                        }
                    }
                }
                catch
                {
                    //TODO
                }
            }

            private void InsertAlignCenter(object param)
            {
                this.OnSurroundTag("mitte");
            }

            private void InsertAlignJustify(object param)
            {
                this.OnSurroundTag("blocksatz");
            }

            private void InsertAlignLeft(object param)
            {
                this.OnSurroundTag("links");
            }

            private void InsertAlignRight(object param)
            {
                this.OnSurroundTag("rechts");
            }

            private void InsertBold(object param)
            {
                this.OnSurroundTag("fett");
            }

            private void InsertFontColor(Color fontColor)
            {
                var matchingColor = FONT_COLORS.LastOrDefault(c => c.Color.Equals(fontColor));
                if (matchingColor != null)
                {
                    this.OnSurroundTag(TMEditorHelper.ToFontColorTagName(matchingColor));
                }
            }

            private void InsertFontSize(TMValueComboBoxItem fontSize)
            {
                if (fontSize == null)
                {
                    return;
                }

                this.OnSurroundTag(fontSize.Value.AsString(true));
            }

            private void InsertItalic(object param)
            {
                this.OnSurroundTag("kursiv");
            }

            private void InsertListItem(object param)
            {
                this.OnSurroundTag("*");
            }

            private void InsertMoveRight(object param)
            {
                this.OnSurroundTag("nach_rechts");
            }

            private void InsertPictureFromClipboard(object param)
            {
                try
                {
                    if (Clipboard.ContainsImage())
                    {
                        var imageTags = new StringBuilder();

                        var bmp = Clipboard.GetImage();
                        using (var img = TMImageHelper.ToBitmap(bmp))
                        {
                            using (var newImg = TMImageHelper.ResizeImage(img, 512))
                            {
                                using (var temp = new MemoryStream())
                                {
                                    newImg.Save(temp, ImageFormat.Jpeg);

                                    imageTags.AppendFormat("{{bild}}data:image/jpeg;base64,{0}{{/bild}}",
                                                           Convert.ToBase64String(temp.ToArray()));
                                }
                            }
                        }

                        this.OnInsertText(imageTags.ToString());
                    }
                    else
                    {
                        //TODO
                    }
                }
                catch
                {
                    //TODO
                }
            }

            private void InsertPictures(object param)
            {
                try
                {
                    var dialog = new OpenFileDialog();
                    dialog.Filter = "Alle Bilder|*.png;*.gif;*.bmp;*.jpg;*.jpeg;*.psd|Alle Dateien (*.*)|*.*";
                    dialog.Multiselect = true;
                    if (dialog.ShowDialog() != true)
                    {
                        return;
                    }

                    var imageTags = new StringBuilder();

                    foreach (var file in dialog.FileNames
                                               .Select(p => new FileInfo(p))
                                               .Where(f => f.Exists))
                    {
                        try
                        {
                            using (var stream = file.OpenRead())
                            {
                                using (var img = TMImageHelper.LoadBitmap(stream))
                                {
                                    using (var newImg = TMImageHelper.ResizeImage(img, 512))
                                    {
                                        using (var temp = new MemoryStream())
                                        {
                                            newImg.Save(temp, ImageFormat.Jpeg);

                                            imageTags.AppendFormat("{{bild}}data:image/jpeg;base64,{0}{{/bild}}",
                                                                   Convert.ToBase64String(temp.ToArray()));
                                        }
                                    }
                                }
                            }
                        }
                        catch
                        {
                            //TODO
                        }
                    }

                    this.OnInsertText(imageTags.ToString());
                }
                catch
                {
                    //TODO
                }
            }

            private void InsertRecentFile(TMRecentFile file)
            {
                while (this.RecentFiles.Contains(file))
                {
                    this.RecentFiles.Remove(file);
                }

                if (file != null)
                {
                    this.RecentFiles.Insert(0, file);
                }

                this.TrySaveRecentFileList();
            }

            private void InsertStrikeThrough(object param)
            {
                this.OnSurroundTag("durchstreichen");
            }

            private void InsertUnderline(object param)
            {
                this.OnSurroundTag("unterstreichen");
            }

            private void NewDocument(object param)
            {
                var result = WpfToolKit.MessageBox.Show("Möchten Sie wirklich ein neues Dokument beginnen? Die aktuellen Änderungen gehen dabei verloren!",
                                                        "Neues Dokument",
                                                        MessageBoxButton.YesNo,
                                                        MessageBoxImage.Question,
                                                        MessageBoxResult.No);
                if (result != MessageBoxResult.Yes)
                {
                    return;
                }

                this.ResetDocument();
                this.DocumentFile = null;
            }

            private bool OnEventHandler(EventHandler handler)
            {
                if (handler != null)
                {
                    handler(this, EventArgs.Empty);
                    return true;
                }

                return false;
            }

            private bool OnInsertText(IEnumerable<char> text)
            {
                var handler = this.InsertText;
                if (handler != null)
                {
                    handler(this, new TMInsertTextEventArgs(text));
                    return true;
                }

                return false;
            }

            private bool OnSurroundTag(IEnumerable<char> tagName)
            {
                var prefix = string.Format("{{{0}}}",
                                           tagName.AsString());

                var suffix = string.Format("{{/{0}}}",
                                           tagName.AsString());

                return this.OnSurroundText(prefix: prefix,
                                           suffix: suffix);
            }

            private bool OnSurroundText(IEnumerable<char> prefix = null,
                                        IEnumerable<char> suffix = null)
            {
                var handler = this.SurroundText;
                if (handler != null)
                {
                    handler(this, new TMSurroundTextEventArgs(prefix: prefix,
                                                            suffix: suffix));
                    return true;
                }

                return false;
            }

            private void OpenDocument(object param)
            {
                try
                {
                    var dialog = new OpenFileDialog();
                    dialog.Filter = _DIALOG_FILTER;
                    if (dialog.ShowDialog() != true)
                    {
                        return;
                    }

                    var inputFile = new FileInfo(dialog.FileName);

                    this.Document = TMAuctionDesignerDocument.FromFile(inputFile);
                    this.DocumentFile = inputFile;

                    this.InsertRecentFile((TMRecentFile)inputFile);
                }
                catch
                {
                    //TODO
                }
            }

            private void Redo(object param)
            {
                this.OnEventHandler(this.DoRedo);
            }

            private void ResetDocument()
            {
                this.Document = new TMAuctionDesignerDocument();

                this.ResetTemplate();
            }

            private void ResetTemplate()
            {
                var doc = this.Document;
                if (doc == null)
                {
                    return;
                }

                // css
                using (var stream = this.GetType().Assembly.GetManifestResourceStream("MarcelJoachimKloubert.WpfAuctionDesigner._Res.Styles.Default.css"))
                {
                    using (var reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        doc.CssPart = new TextDocument(reader.ReadToEnd());
                    }
                }

                // html
                using (var stream = this.GetType().Assembly.GetManifestResourceStream("MarcelJoachimKloubert.WpfAuctionDesigner._Res.Templates.Default.html"))
                {
                    using (var reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        doc.HtmlPart = new TextDocument(reader.ReadToEnd());
                    }
                }
            }

            private void SaveAsDefault(object param)
            {
                try
                {
                    var file = this.GetDefaultFile();
                    if (file.Exists)
                    {
                        file.Delete();
                        file.Refresh();
                    }

                    var doc = this.Document;
                    if (doc != null)
                    {
                        doc.Save(file);
                    }
                }
                catch
                {
                    //TODO
                }
            }

            private void SaveDocument(object param)
            {
                try
                {
                    var doc = this.Document;
                    if (doc == null)
                    {
                        return;
                    }

                    var outputFile = this.DocumentFile;
                    if (outputFile == null)
                    {
                        var dialog = new SaveFileDialog();
                        dialog.Filter = _DIALOG_FILTER;
                        dialog.OverwritePrompt = true;
                        if (dialog.ShowDialog() != true)
                        {
                            return;
                        }

                        outputFile = new FileInfo(dialog.FileName);
                    }

                    outputFile.Refresh();
                    if (outputFile.Exists)
                    {
                        outputFile.Delete();
                        outputFile.Refresh();
                    }

                    doc.Save(outputFile);
                    this.DocumentFile = outputFile;

                    this.InsertRecentFile((TMRecentFile)outputFile);
                }
                catch
                {
                    //TODO
                }
            }

            private void ShowException(Exception ex)
            {
                if (ex == null)
                {
                    return;
                }

                var innerEx = ex.GetBaseException() ?? ex;

                WpfToolKit.MessageBox
                          .Show(innerEx.ToString(),
                                string.Format("[!!!ERROR!!!] {0}",
                                              innerEx.GetType().FullName),
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }

            private bool TrySaveRecentFileList()
            {
                lock (this._SYNC)
                {
                    try
                    {
                        var recentFile = this.GetRecentFile();
                        if (recentFile.Exists)
                        {
                            recentFile.Delete();
                            recentFile.Refresh();
                        }

                        using (var stream = recentFile.OpenWrite())
                        {
                            using (var writer = new StreamWriter(stream, Encoding.UTF8))
                            {
                                foreach (var file in this.RecentFiles.ToArray())
                                {
                                    writer.WriteLine(file.FullPath);
                                }
                            }
                        }

                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }

            private void Undo(object param)
            {
                this.OnEventHandler(this.DoUndo);
            }

            #endregion Methods
        }

        #endregion Nested Classes
    }
}
