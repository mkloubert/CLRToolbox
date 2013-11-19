using System;

namespace MarcelJoachimKloubert.CLRToolbox
{
    /// <summary>
    /// Class that converts objects.
    /// </summary>
    public static class TMConvert
    {
        #region Methods (1)

        // Public Methods (1) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="Convert.ChangeType(object, Type)" />
        public static T ChangeType<T>(object value)
        {
            if (value is T)
            {
                return (T)value;
            }

            return value != null ? (T)global::System.Convert.ChangeType(value, typeof(T)) : default(T);
        }

        #endregion Methods
    }
}
