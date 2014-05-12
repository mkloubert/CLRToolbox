// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;

namespace MarcelJoachimKloubert.CLRToolbox.ComponentModel
{
    /// <summary>
    /// Defines from where a property should receive notifications.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property,
                    AllowMultiple = true,
                    Inherited = false)]
    public sealed class ReceiveNotificationFromAttribute : Attribute
    {
        #region Fields (2)

        private readonly ReceiveNotificationFromOptions? _OPTIONS;
        private readonly string _SENDER_NAME;

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="ReceiveNotificationFromAttribute" /> class.
        /// </summary>
        /// <param name="senderName">
        /// The value for the <see cref="ReceiveNotificationFromAttribute.SenderName" /> property.
        /// </param>
        public ReceiveNotificationFromAttribute(string senderName)
            : this(senderName, ReceiveNotificationFromOptions.Default)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReceiveNotificationFromAttribute" /> class.
        /// </summary>
        /// <param name="senderName">
        /// The value for the <see cref="ReceiveNotificationFromAttribute.SenderName" /> property.
        /// </param>
        /// <param name="options">
        /// The value for the <see cref="ReceiveNotificationFromAttribute.Options" /> property.
        /// </param>
        public ReceiveNotificationFromAttribute(string senderName,
                                                ReceiveNotificationFromOptions? options)
        {
            this._SENDER_NAME = (senderName ?? string.Empty).Trim();
            if (this._SENDER_NAME == string.Empty)
            {
                this._SENDER_NAME = null;
            }

            this._OPTIONS = options;
        }

        #endregion Constructors

        #region Properties (2)

        /// <summary>
        /// Gets the options.
        /// </summary>
        public ReceiveNotificationFromOptions? Options
        {
            get { return this._OPTIONS; }
        }

        /// <summary>
        /// Gets the name of sender / sending member of the notification.
        /// </summary>
        public string SenderName
        {
            get { return this._SENDER_NAME; }
        }

        #endregion Properties
    }
}