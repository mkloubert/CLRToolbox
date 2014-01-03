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
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;
using ICSharpCode.AvalonEdit.Document;
using MarcelJoachimKloubert.CLRToolbox.ComponentModel;
using MarcelJoachimKloubert.WpfAuctionDesigner.Classes.Controls;

namespace MarcelJoachimKloubert.WpfAuctionDesigner.Classes.Documents
{
    /// <summary>
    /// Stores the data of an auction designer document.
    /// </summary>
    public sealed class TMAuctionDesignerDocument : NotificationObjectBase
    {
        #region Fields (24)

        private TextDocument _articleDescription;
        private string _articleName;
        private string _articleState;
        private TextDocument _buyInfo;
        private TextDocument _cssPart;
        private TextDocument _deliveryInfo;
        private TextDocument _htmlOutput;
        private TextDocument _htmlPart;
        private string _producer;
        private TextDocument _remarks;
        private TextDocument _scopeOfSupply;
        private const string _XML_ATTRIB_ARTICLENAME = "name";
        private const string _XML_ATTRIB_ARTICLEPRODUCER = "producer";
        private const string _XML_ATTRIB_ARTICLESTATE = "state";
        private const string _XML_ELEMENT_ARTICLE = "article";
        private const string _XML_ELEMENT_BUYINFO = "buyInfo";
        private const string _XML_ELEMENT_CSS = "css";
        private const string _XML_ELEMENT_DELIVERYINFO = "deliveryInfo";
        private const string _XML_ELEMENT_DESCRIPTION = "description";
        private const string _XML_ELEMENT_HTML = "html";
        private const string _XML_ELEMENT_REMARKS = "remarks";
        private const string _XML_ELEMENT_ROOT = "auction_designer";
        private const string _XML_ELEMENT_SCOPEOFSUPPLY = "scopeOfSupply";
        private const string _XML_ELEMENT_TEMPLATE = "template";

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="TMAuctionDesignerDocument" /> class.
        /// </summary>
        public TMAuctionDesignerDocument()
        {
            this.ArticleDescription = new TextDocument();
            this.BuyInfo = new TextDocument();
            this.CssPart = new TextDocument();
            this.DeliveryInfo = new TextDocument();
            this.HtmlPart = new TextDocument();
            this.Remarks = new TextDocument();
            this.ScopeOfSupply = new TextDocument();
        }

        #endregion Constructors

        #region Properties (12)

        /// <summary>
        /// Gets the document that contains the article description.
        /// </summary>
        public TextDocument ArticleDescription
        {
            get { return this._articleDescription; }

            private set
            {
                if (this.SetTextDocumentProperty(ref this._articleDescription, value))
                {
                    this.UpdateHtmlOutput();
                }
            }
        }

        /// <summary>
        /// Gets or sets the name of the article.
        /// </summary>
        public string ArticleName
        {
            get { return this._articleName; }

            set
            {
                if (this.SetProperty(ref this._articleName, value))
                {
                    this.UpdateHtmlOutput();
                }
            }
        }

        /// <summary>
        /// Gets the state of the article.
        /// </summary>
        public string ArticleState
        {
            get { return this._articleState; }

            set
            {
                if (this.SetProperty(ref this._articleState, value))
                {
                    this.UpdateHtmlOutput();
                }
            }
        }

        /// <summary>
        /// Gets the document that contains the buy information.
        /// </summary>
        public TextDocument BuyInfo
        {
            get { return this._buyInfo; }

            private set
            {
                if (this.SetTextDocumentProperty(ref this._buyInfo, value))
                {
                    this.UpdateHtmlOutput();
                }
            }
        }

        /// <summary>
        /// Gets or sets the CSS part for the output document.
        /// </summary>
        public TextDocument CssPart
        {
            get { return this._cssPart; }

            set
            {
                if (this.SetTextDocumentProperty(ref this._cssPart, value))
                {
                    this.UpdateHtmlOutput();
                }
            }
        }

        /// <summary>
        /// Gets the document that contains the delivery information.
        /// </summary>
        public TextDocument DeliveryInfo
        {
            get { return this._deliveryInfo; }

            private set
            {
                if (this.SetTextDocumentProperty(ref this._deliveryInfo, value))
                {
                    this.UpdateHtmlOutput();
                }
            }
        }

        /// <summary>
        /// Gets the complete content of the output document.
        /// </summary>
        public TextDocument HtmlOutput
        {
            get { return this._htmlOutput; }

            private set
            {
                if (this.SetTextDocumentProperty(ref this._htmlOutput, value))
                {
                    this.OnPropertyChanged(() => this.HtmlOutputSource);
                    this.OnHtmlOutputSourceChanged();
                }
            }
        }

        /// <summary>
        /// Gets the value of <see cref="ViewModel.HtmlOutput" />
        /// as <see cref="string" />.
        /// </summary>
        public string HtmlOutputSource
        {
            get
            {
                string result = null;

                var htmlOutput = this.HtmlOutput;
                if (htmlOutput != null)
                {
                    using (var reader = htmlOutput.CreateReader())
                    {
                        result = reader.ReadToEnd();
                    }
                }

                return result;
            }
        }

        /// <summary>
        /// Gets or sets the HTML part for the output document.
        /// </summary>
        public TextDocument HtmlPart
        {
            get { return this._htmlPart; }

            set
            {
                if (this.SetTextDocumentProperty(ref this._htmlPart, value))
                {
                    this.UpdateHtmlOutput();
                }
            }
        }

        /// <summary>
        /// Gets or sets the name of the producer.
        /// </summary>
        public string Producer
        {
            get { return this._producer; }

            set
            {
                if (this.SetProperty(ref this._producer, value))
                {
                    this.UpdateHtmlOutput();
                }
            }
        }

        /// <summary>
        /// Gets the document that contains the remarks.
        /// </summary>
        public TextDocument Remarks
        {
            get { return this._remarks; }

            private set
            {
                if (this.SetTextDocumentProperty(ref this._remarks, value))
                {
                    this.UpdateHtmlOutput();
                }
            }
        }

        /// <summary>
        /// Gets the document that contains with the scope of supply.
        /// </summary>
        public TextDocument ScopeOfSupply
        {
            get { return this._scopeOfSupply; }

            private set
            {
                if (this.SetTextDocumentProperty(ref this._scopeOfSupply, value))
                {
                    this.UpdateHtmlOutput();
                }
            }
        }

        #endregion Properties

        #region Delegates and Events (1)

        // Events (1) 

        /// <summary>
        /// Is invoked when the preview should be updated.
        /// </summary>
        public event EventHandler HtmlOutputSourceChanged;

        #endregion Delegates and Events

        #region Methods (7)

        // Public Methods (3) 

        /// <summary>
        /// Creates an instance of that class from a file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>The document that was loaded from file.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="file" /> is <see langword="null" />.
        /// </exception>
        public static TMAuctionDesignerDocument FromFile(FileInfo file)
        {
            if (file == null)
            {
                throw new ArgumentNullException("file");
            }

            var result = new TMAuctionDesignerDocument();

            XDocument xmlDoc;
            using (var stream = new FileStream(file.FullName,
                                               FileMode.Open,
                                               FileAccess.Read))
            {
                xmlDoc = XDocument.Load(stream);
            }

            var articleElement = xmlDoc.XPathSelectElements("//" + _XML_ELEMENT_ROOT + "/" + _XML_ELEMENT_ARTICLE).LastOrDefault();
            if (articleElement != null)
            {
                var nameAttrib = articleElement.Attribute(_XML_ATTRIB_ARTICLENAME);
                if (nameAttrib != null)
                {
                    result.ArticleName = nameAttrib.Value;
                }

                var producerAttrib = articleElement.Attribute(_XML_ATTRIB_ARTICLEPRODUCER);
                if (producerAttrib != null)
                {
                    result.Producer = producerAttrib.Value;
                }

                var stateAttrib = articleElement.Attribute(_XML_ATTRIB_ARTICLESTATE);
                if (stateAttrib != null)
                {
                    result.ArticleState = stateAttrib.Value;
                }

                var descriptionElement = articleElement.Elements(_XML_ELEMENT_DESCRIPTION).LastOrDefault();
                if (descriptionElement != null)
                {
                    result.ArticleDescription = new TextDocument(descriptionElement.Value ?? string.Empty);
                }

                var scopeOfSupplyElement = articleElement.Elements(_XML_ELEMENT_SCOPEOFSUPPLY).LastOrDefault();
                if (scopeOfSupplyElement != null)
                {
                    result.ScopeOfSupply = new TextDocument(scopeOfSupplyElement.Value ?? string.Empty);
                }

                var deliveryInfoElement = articleElement.Elements(_XML_ELEMENT_DELIVERYINFO).LastOrDefault();
                if (deliveryInfoElement != null)
                {
                    result.DeliveryInfo = new TextDocument(deliveryInfoElement.Value ?? string.Empty);
                }

                var buyInfoElement = articleElement.Elements(_XML_ELEMENT_BUYINFO).LastOrDefault();
                if (buyInfoElement != null)
                {
                    result.BuyInfo = new TextDocument(buyInfoElement.Value ?? string.Empty);
                }

                var remarksElement = articleElement.Elements(_XML_ELEMENT_REMARKS).LastOrDefault();
                if (remarksElement != null)
                {
                    result.Remarks = new TextDocument(remarksElement.Value ?? string.Empty);
                }
            }

            var templateElement = xmlDoc.XPathSelectElements("//" + _XML_ELEMENT_ROOT + "/" + _XML_ELEMENT_TEMPLATE).LastOrDefault();
            if (templateElement != null)
            {
                var cssElement = templateElement.Elements(_XML_ELEMENT_CSS).LastOrDefault();
                if (cssElement != null)
                {
                    result.CssPart = new TextDocument(cssElement.Value ?? string.Empty);
                }

                var htmlElement = templateElement.Elements(_XML_ELEMENT_HTML).LastOrDefault();
                if (htmlElement != null)
                {
                    result.HtmlPart = new TextDocument(htmlElement.Value ?? string.Empty);
                }
            }

            return result;
        }

        /// <summary>
        /// Saves the data of that document to a file.
        /// </summary>
        /// <param name="file">The target file.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="file" /> is <see langword="null" />.
        /// </exception>
        public void Save(FileInfo file)
        {
            if (file == null)
            {
                throw new ArgumentNullException("file");
            }

            using (var stream = new FileStream(file.FullName,
                                               FileMode.CreateNew,
                                               FileAccess.ReadWrite))
            {
                this.Save(stream);
            }
        }

        /// <summary>
        /// Saves the data of that document to a stream.
        /// </summary>
        /// <param name="stream">The target stream.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="stream" /> is <see langword="null" />.
        /// </exception>
        public void Save(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }

            var xmlDoc = new XDocument(new XDeclaration("1.0", Encoding.UTF8.WebName, "yes"));
            xmlDoc.Add(new XElement(_XML_ELEMENT_ROOT));

            // article
            {
                var articleElement = new XElement(_XML_ELEMENT_ARTICLE);
                {
                    var articleName = this.ArticleName;
                    if (!string.IsNullOrWhiteSpace(articleName))
                    {
                        articleElement.SetAttributeValue(_XML_ATTRIB_ARTICLENAME,
                                                         articleName.Trim());
                    }

                    var producer = this.Producer;
                    if (!string.IsNullOrWhiteSpace(producer))
                    {
                        articleElement.SetAttributeValue(_XML_ATTRIB_ARTICLEPRODUCER,
                                                         producer.Trim());
                    }

                    var articleState = this.ArticleState;
                    if (!string.IsNullOrWhiteSpace(articleState))
                    {
                        articleElement.SetAttributeValue(_XML_ATTRIB_ARTICLESTATE,
                                                         articleState.Trim());
                    }

                    var description = this.ArticleDescription;
                    if (description != null)
                    {
                        using (var reader = description.CreateReader())
                        {
                            var descriptionElement = new XElement(_XML_ELEMENT_DESCRIPTION);
                            descriptionElement.Value = reader.ReadToEnd();

                            articleElement.Add(descriptionElement);
                        }
                    }

                    var scopeOfSupply = this.ScopeOfSupply;
                    if (scopeOfSupply != null)
                    {
                        using (var reader = scopeOfSupply.CreateReader())
                        {
                            var scopeOfSupplyElement = new XElement(_XML_ELEMENT_SCOPEOFSUPPLY);
                            scopeOfSupplyElement.Value = reader.ReadToEnd();

                            articleElement.Add(scopeOfSupplyElement);
                        }
                    }

                    var deliveryInfo = this.DeliveryInfo;
                    if (deliveryInfo != null)
                    {
                        using (var reader = deliveryInfo.CreateReader())
                        {
                            var deliveryInfoElement = new XElement(_XML_ELEMENT_DELIVERYINFO);
                            deliveryInfoElement.Value = reader.ReadToEnd();

                            articleElement.Add(deliveryInfoElement);
                        }
                    }

                    var buyInfo = this.BuyInfo;
                    if (buyInfo != null)
                    {
                        using (var reader = buyInfo.CreateReader())
                        {
                            var buyInfoElement = new XElement(_XML_ELEMENT_BUYINFO);
                            buyInfoElement.Value = reader.ReadToEnd();

                            articleElement.Add(buyInfoElement);
                        }
                    }

                    var remarks = this.Remarks;
                    if (remarks != null)
                    {
                        using (var reader = remarks.CreateReader())
                        {
                            var remarksElement = new XElement(_XML_ELEMENT_REMARKS);
                            remarksElement.Value = reader.ReadToEnd();

                            articleElement.Add(remarksElement);
                        }
                    }
                }

                xmlDoc.Root.Add(articleElement);
            }

            // template
            {
                var templateElement = new XElement(_XML_ELEMENT_TEMPLATE);

                var cssPart = this.CssPart;
                if (cssPart != null)
                {
                    var cssElement = new XElement(_XML_ELEMENT_CSS);

                    using (var reader = cssPart.CreateReader())
                    {
                        cssElement.Value = reader.ReadToEnd();
                    }

                    templateElement.Add(cssElement);
                }

                var htmlPart = this.HtmlPart;
                if (htmlPart != null)
                {
                    var htmlElement = new XElement(_XML_ELEMENT_HTML);

                    using (var reader = htmlPart.CreateReader())
                    {
                        htmlElement.Value = reader.ReadToEnd();
                    }

                    templateElement.Add(htmlElement);
                }

                xmlDoc.Root.Add(templateElement);
            }

            xmlDoc.Save(stream);
        }
        // Private Methods (4) 

        private bool OnHtmlOutputSourceChanged()
        {
            var handler = this.HtmlOutputSourceChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
                return true;
            }

            return false;
        }

        private bool SetTextDocumentProperty(ref TextDocument curDoc,
                                             TextDocument newDoc,
                                             [CallerMemberName] IEnumerable<char> propertyName = null)
        {
            // unregister old document for text change
            var oldValue = curDoc;
            if (oldValue != null)
            {
                oldValue.TextChanged -= this.TextDocument_TextChanged;
            }

            var result = this.SetProperty(field: ref curDoc,
                                          newValue: newDoc,
                                          propertyName: propertyName);

            // unregister new document for text change
            var newValue = curDoc;
            if (newValue != null)
            {
                newValue.TextChanged += this.TextDocument_TextChanged;
            }

            return result;
        }

        private void TextDocument_TextChanged(object sender, EventArgs e)
        {
            this.UpdateHtmlOutput();
        }

        private void UpdateHtmlOutput()
        {
            var htmlOut = new StringBuilder();

            var css = this.CssPart;
            if (css != null)
            {
                using (var reader = css.CreateReader())
                {
                    htmlOut.AppendFormat(@"
<style type=""text/css"">

{0}

</style>
", reader.ReadToEnd());
                }
            }

            var html = this.HtmlPart;
            if (html != null)
            {
                StringBuilder htmlSrc;
                using (var reader = html.CreateReader())
                {
                    htmlSrc = new StringBuilder(reader.ReadToEnd());
                }

                // replace tags
                {
                    var textReplacers = new Dictionary<string, Func<TMAuctionDesignerDocument, string>>()
                    {
                        { "BEZEICHNUNG", d => d.ArticleName },
                        { "HERSTELLER", d => d.Producer },
                        { "ZUSTAND", d => d.ArticleState },
                    };

                    foreach (var tr in textReplacers)
                    {
                        var tagName = tr.Key;
                        var func = tr.Value;

                        htmlSrc.Replace(string.Format("<{0}>",
                                                      tagName),
                                        TMEditorHelper.ParseForHtml(func(this),
                                                                    false));
                    }

                    textReplacers = new Dictionary<string, Func<TMAuctionDesignerDocument, string>>()
                    {
                        { "BESCHREIBUNG", d => TMEditorHelper.ToString(d.ArticleDescription) },
                        { "LIEFERUMFANG", d => TMEditorHelper.ToString(d.ScopeOfSupply) },
                        { "VERSAND-INFORMATIONEN", d => TMEditorHelper.ToString(d.DeliveryInfo) },
                        { "KAUF-INFORMATIONEN", d => TMEditorHelper.ToString(d.BuyInfo) },
                        { "ANMERKUNGEN", d => TMEditorHelper.ToString(d.Remarks) },
                    };

                    foreach (var tr in textReplacers)
                    {
                        var tagName = tr.Key;
                        var func = tr.Value;

                        htmlSrc.Replace(string.Format("<{0}>",
                                                      tagName),
                                        TMEditorHelper.ParseForHtml(func(this),
                                                                    true));
                    }
                }

                htmlOut.AppendFormat(@"
{0}
", htmlSrc);
            }

            this.HtmlOutput = new TextDocument(htmlOut.ToString());
        }

        #endregion Methods
    }
}
