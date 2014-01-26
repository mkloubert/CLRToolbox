// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using MarcelJoachimKloubert.CLRToolbox.Helpers;

namespace MarcelJoachimKloubert.CLRToolbox
{
    #region CLASS: WritableTuple

    /// <summary>
    /// Factory for creating wrtable tuples.
    /// </summary>
    public static class WritableTuple
    {
        #region Methods (16)

        // Public Methods (8) 

        /// <summary>
        /// Creates a <see cref="WritableTuple{T1}" /> instance.
        /// </summary>
        /// <typeparam name="T1">Type of the first item.</typeparam>
        /// <param name="item1">The value for the first item.</param>
        /// <returns>The new instance.</returns>
        public static WritableTuple<T1> Create<T1>(T1 item1)
        {
            return new WritableTuple<T1>(item1);
        }

        /// <summary>
        /// Creates a <see cref="WritableTuple{T1, T2}" /> instance.
        /// </summary>
        /// <typeparam name="T1">Type of the first item.</typeparam>
        /// <typeparam name="T2">Type of the second item.</typeparam>
        /// <param name="item1">The value the first item.</param>
        /// <param name="item2">The value the second item.</param>
        /// <returns>The new instance.</returns>
        public static WritableTuple<T1, T2> Create<T1, T2>(T1 item1, T2 item2)
        {
            return new WritableTuple<T1, T2>(item1, item2);
        }

        /// <summary>
        /// Creates a <see cref="WritableTuple{T1, T2, T3}" /> instance.
        /// </summary>
        /// <typeparam name="T1">Type of the first item.</typeparam>
        /// <typeparam name="T2">Type of the second item.</typeparam>
        /// <typeparam name="T3">Type of the third item.</typeparam>
        /// <param name="item1">The value the first item.</param>
        /// <param name="item2">The value the second item.</param>
        /// <param name="item3">The value the third item.</param>
        /// <returns>The new instance.</returns>
        public static WritableTuple<T1, T2, T3> Create<T1, T2, T3>(T1 item1, T2 item2, T3 item3)
        {
            return new WritableTuple<T1, T2, T3>(item1, item2, item3);
        }

        /// <summary>
        /// Creates a <see cref="WritableTuple{T1, T2, T3, T4}" /> instance.
        /// </summary>
        /// <typeparam name="T1">Type of the first item.</typeparam>
        /// <typeparam name="T2">Type of the second item.</typeparam>
        /// <typeparam name="T3">Type of the third item.</typeparam>
        /// <typeparam name="T4">Type of the fourth item.</typeparam>
        /// <param name="item1">The value the first item.</param>
        /// <param name="item2">The value the second item.</param>
        /// <param name="item3">The value the third item.</param>
        /// <param name="item4">The value the fourth item.</param>
        /// <returns>The new instance.</returns>
        public static WritableTuple<T1, T2, T3, T4> Create<T1, T2, T3, T4>(T1 item1, T2 item2, T3 item3, T4 item4)
        {
            return new WritableTuple<T1, T2, T3, T4>(item1, item2, item3, item4);
        }

        /// <summary>
        /// Creates a <see cref="WritableTuple{T1, T2, T3, T4, T5}" /> instance.
        /// </summary>
        /// <typeparam name="T1">Type of the first item.</typeparam>
        /// <typeparam name="T2">Type of the second item.</typeparam>
        /// <typeparam name="T3">Type of the third item.</typeparam>
        /// <typeparam name="T4">Type of the fourth item.</typeparam>
        /// <typeparam name="T5">Type of the fifth item.</typeparam>
        /// <param name="item1">The value the first item.</param>
        /// <param name="item2">The value the second item.</param>
        /// <param name="item3">The value the third item.</param>
        /// <param name="item4">The value the fourth item.</param>
        /// <param name="item5">The value the fifth item.</param>
        /// <returns>The new instance.</returns>
        public static WritableTuple<T1, T2, T3, T4, T5> Create<T1, T2, T3, T4, T5>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
        {
            return new WritableTuple<T1, T2, T3, T4, T5>(item1, item2, item3, item4, item5);
        }

        /// <summary>
        /// Creates a <see cref="WritableTuple{T1, T2, T3, T4, T5, T6}" /> instance.
        /// </summary>
        /// <typeparam name="T1">Type of the first item.</typeparam>
        /// <typeparam name="T2">Type of the second item.</typeparam>
        /// <typeparam name="T3">Type of the third item.</typeparam>
        /// <typeparam name="T4">Type of the fourth item.</typeparam>
        /// <typeparam name="T5">Type of the fifth item.</typeparam>
        /// <typeparam name="T6">Type of the sixth item.</typeparam>
        /// <param name="item1">The value the first item.</param>
        /// <param name="item2">The value the second item.</param>
        /// <param name="item3">The value the third item.</param>
        /// <param name="item4">The value the fourth item.</param>
        /// <param name="item5">The value the fifth item.</param>
        /// <param name="item6">The value the sixth item.</param>
        /// <returns>The new instance.</returns>
        public static WritableTuple<T1, T2, T3, T4, T5, T6> Create<T1, T2, T3, T4, T5, T6>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
        {
            return new WritableTuple<T1, T2, T3, T4, T5, T6>(item1, item2, item3, item4, item5, item6);
        }

        /// <summary>
        /// Creates a <see cref="WritableTuple{T1, T2, T3, T4, T5, T6, T7}" /> instance.
        /// </summary>
        /// <typeparam name="T1">Type of the first item.</typeparam>
        /// <typeparam name="T2">Type of the second item.</typeparam>
        /// <typeparam name="T3">Type of the third item.</typeparam>
        /// <typeparam name="T4">Type of the fourth item.</typeparam>
        /// <typeparam name="T5">Type of the fifth item.</typeparam>
        /// <typeparam name="T6">Type of the sixth item.</typeparam>
        /// <typeparam name="T7">Type of the seventh item.</typeparam>
        /// <param name="item1">The value the first item.</param>
        /// <param name="item2">The value the second item.</param>
        /// <param name="item3">The value the third item.</param>
        /// <param name="item4">The value the fourth item.</param>
        /// <param name="item5">The value the fifth item.</param>
        /// <param name="item6">The value the sixth item.</param>
        /// <param name="item7">The value the seventh item.</param>
        /// <returns>The new instance.</returns>
        public static WritableTuple<T1, T2, T3, T4, T5, T6, T7> Create<T1, T2, T3, T4, T5, T6, T7>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
        {
            return new WritableTuple<T1, T2, T3, T4, T5, T6, T7>(item1, item2, item3, item4, item5, item6, item7);
        }

        /// <summary>
        /// Creates a <see cref="WritableTuple{T1, T2, T3, T4, T5, T6, T7, TRest}" /> instance.
        /// </summary>
        /// <typeparam name="T1">Type of the first item.</typeparam>
        /// <typeparam name="T2">Type of the second item.</typeparam>
        /// <typeparam name="T3">Type of the third item.</typeparam>
        /// <typeparam name="T4">Type of the fourth item.</typeparam>
        /// <typeparam name="T5">Type of the fifth item.</typeparam>
        /// <typeparam name="T6">Type of the sixth item.</typeparam>
        /// <typeparam name="T7">Type of the seventh item.</typeparam>
        /// <typeparam name="TRest">Type of the extension item.</typeparam>
        /// <param name="item1">The value the first item.</param>
        /// <param name="item2">The value the second item.</param>
        /// <param name="item3">The value the third item.</param>
        /// <param name="item4">The value the fourth item.</param>
        /// <param name="item5">The value the fifth item.</param>
        /// <param name="item6">The value the sixth item.</param>
        /// <param name="item7">The value the seventh item.</param>
        /// <param name="rest">The value the extension item.</param>
        /// <returns>The new instance.</returns>
        public static WritableTuple<T1, T2, T3, T4, T5, T6, T7, TRest> Create<T1, T2, T3, T4, T5, T6, T7, TRest>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, TRest rest)
        {
            return new WritableTuple<T1, T2, T3, T4, T5, T6, T7, TRest>(item1, item2, item3, item4, item5, item6, item7, rest);
        }
        // Internal Methods (8) 

        internal static int CombineHashCodes(int h1)
        {
            return h1;
        }

        internal static int CombineHashCodes(int h1, int h2)
        {
            return (h1 << 5) + h1 ^ h2;
        }

        internal static int CombineHashCodes(int h1, int h2, int h3)
        {
            return WritableTuple.CombineHashCodes(WritableTuple.CombineHashCodes(h1, h2), h3);
        }

        internal static int CombineHashCodes(int h1, int h2, int h3, int h4)
        {
            return WritableTuple.CombineHashCodes(WritableTuple.CombineHashCodes(h1, h2), WritableTuple.CombineHashCodes(h3, h4));
        }

        internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5)
        {
            return WritableTuple.CombineHashCodes(WritableTuple.CombineHashCodes(h1, h2, h3, h4), h5);
        }

        internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6)
        {
            return WritableTuple.CombineHashCodes(WritableTuple.CombineHashCodes(h1, h2, h3, h4), WritableTuple.CombineHashCodes(h5, h6));
        }

        internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6, int h7)
        {
            return WritableTuple.CombineHashCodes(WritableTuple.CombineHashCodes(h1, h2, h3, h4), WritableTuple.CombineHashCodes(h5, h6, h7));
        }

        internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8)
        {
            return WritableTuple.CombineHashCodes(WritableTuple.CombineHashCodes(h1, h2, h3, h4), WritableTuple.CombineHashCodes(h5, h6, h7, h8));
        }

        #endregion Methods
    }

    #endregion

    #region CLASS: WritableTuple<T1>

    /// <summary>
    /// A writable tuple with one item.
    /// </summary>
    /// <typeparam name="T1">Type of the first item.</typeparam>
#if !WINDOWS_PHONE
    [global::System.Serializable]
#endif
    public class WritableTuple<T1> :
#if !WINDOWS_PHONE
 global::System.MarshalByRefObject,
#endif
 IStructuralComparable, IComparable, IComparable<WritableTuple<T1>>,
 IStructuralEquatable, IEquatable<WritableTuple<T1>>
#if !WINDOWS_PHONE
, global::System.Runtime.Serialization.ISerializable
#endif
    {
        #region Fields (1)

        private T1 _item1;

        #endregion Fields

        #region Constructors (3)

        /// <summary>
        /// Initializes a new instance of <see cref="WritableTuple{T1}" /> class.
        /// </summary>
        /// <param name="item1">The value for <see cref="WritableTuple{T1}.Item1" /> property.</param>
        public WritableTuple(T1 item1)
        {
            this._item1 = item1;
        }
#if !WINDOWS_PHONE

        /// <summary>
        /// Initializes a new instance of <see cref="WritableTuple{T1}" /> class.
        /// </summary>
        /// <param name="info">The serialization context.</param>
        /// <param name="context">The streaming context.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="info" /> is <see langword="null" />.
        /// </exception>
        protected WritableTuple(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            ((ISerializable)this).GetObjectData(info, context);
        }
#endif

        /// <summary>
        /// Initializes a new instance of <see cref="WritableTuple{T1}" /> class.
        /// </summary>
        public WritableTuple()
        {

        }

        #endregion Constructors

        #region Methods (11)

        // Public Methods (3) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IComparable.CompareTo(object)" />
        public int CompareTo(object other)
        {
            return ((IStructuralComparable)this).CompareTo(other, Comparer<object>.Default);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="object.Equals(object)" />
        public override bool Equals(object other)
        {
            return ((IStructuralEquatable)this).Equals(other, EqualityComparer<object>.Default);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="object.GetHashCode()" />
        public override int GetHashCode()
        {
            return ((IStructuralEquatable)this).GetHashCode(EqualityComparer<object>.Default);
        }

        // Private Methods (8) 

        int IComparable<WritableTuple<T1>>.CompareTo(WritableTuple<T1> other)
        {
            return this.CompareTo((object)other);
        }

        int IStructuralComparable.CompareTo(object other, IComparer comparer)
        {
            if (other == null)
            {
                return 1;
            }

            WritableTuple<T1> otherTuple = other as WritableTuple<T1>;
            if (otherTuple == null ||
                !(otherTuple.GetType().Equals(this.GetType())))
            {
                throw new ArgumentException("other");
            }

            object[] thisValues = this.GetTuplePropertyValues();
            object[] otherValues = otherTuple.GetTuplePropertyValues();
            int size = thisValues.Length;

            for (int i = 0; i < (size - 1); i++)
            {
                int cv = comparer.Compare(thisValues[i], otherValues[i]);
                if (cv != 0)
                {
                    return cv;
                }
            }

            return comparer.Compare(thisValues[size - 1],
                                    otherValues[size - 1]);
        }

        bool IEquatable<WritableTuple<T1>>.Equals(WritableTuple<T1> other)
        {
            return this.Equals((object)other);
        }

        bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
        {
            WritableTuple<T1> otherTuple = other as WritableTuple<T1>;
            if (otherTuple == null)
            {
                // no tuple
                return false;
            }

            if (otherTuple.GetType().Equals(this.GetType()) == false)
            {
                // not the same type
                return false;
            }

            return CollectionHelper.SequenceEqual(this.GetTuplePropertyValues(),
                                                  otherTuple.GetTuplePropertyValues());
        }

        int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
        {
            object[] fieldValues = this.GetTuplePropertyValues();

            // calculate hashes for each tuple value
            IEnumerable<object> hashCodes = CollectionHelper.Select(fieldValues,
                                                                    delegate(object v)
                                                                    {
                                                                        return (object)comparer.GetHashCode(v);
                                                                    });

            // find Tuple.CombineHashCodes method
            MethodInfo getHashCodeMethod = CollectionHelper.Single(typeof(MarcelJoachimKloubert.CLRToolbox.WritableTuple).GetMethods(BindingFlags.NonPublic | BindingFlags.Static),
                                                                   delegate(MethodInfo m)
                                                                   {
                                                                       return m.Name == "CombineHashCodes" &&
                                                                              m.GetParameters().Length == fieldValues.Length;
                                                                   });

            return (int)getHashCodeMethod.Invoke(null,
                                                 CollectionHelper.ToArray(hashCodes));
        }
#if !WINDOWS_PHONE

        void global::System.Runtime.Serialization.ISerializable.GetObjectData(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context)
        {
            if (info == null)
            {
                return;
            }

            foreach (global::System.Reflection.PropertyInfo property in this.GetTupleProperties())
            {
                string name = property.Name;
                global::System.Type type = property.PropertyType;

                object value = info.GetValue(name, type);
                property.SetValue(this, value, new object[0]);
            }
        }
#endif
        private PropertyInfo[] GetTupleProperties()
        {
            IEnumerable<PropertyInfo> properties = CollectionHelper.Where(this.GetType()
                                                                              .GetProperties(BindingFlags.Instance | BindingFlags.Public),
                                                                          delegate(PropertyInfo p)
                                                                          {
                                                                              return p.Name.StartsWith("Item") ||
                                                                                     p.Name == "Rest";
                                                                          });

            return CollectionHelper.ToArray(properties);
        }

        private object[] GetTuplePropertyValues()
        {
            IEnumerable<object> values = CollectionHelper.Select(this.GetTupleProperties(),
                                                                 delegate(PropertyInfo p)
                                                                 {
                                                                     return p.GetValue(this, new object[0]);
                                                                 });

            return CollectionHelper.ToArray(values);
        }

        #endregion Methods

        #region Properties (2)

        /// <summary>
        /// Gets or sets the value of the first item of that tuple.
        /// </summary>
        public T1 Item1
        {
            get { return this._item1; }

            set { this._item1 = value; }
        }

        /// <summary>
        /// Gets the number of items the tuple is handling.
        /// </summary>
        public int Size
        {
            get { return this.GetType().GetGenericArguments().Length; }
        }

        #endregion Properties
    }

    #endregion

    #region CLASS: WritableTuple<T1, T2>

    /// <summary>
    /// A writable tuple with two items.
    /// </summary>
    /// <typeparam name="T1">Type of the first item.</typeparam>
    /// <typeparam name="T2">Type of the second item.</typeparam>
#if !WINDOWS_PHONE
    [global::System.Serializable]
#endif
    public class WritableTuple<T1, T2> : WritableTuple<T1>
    {
        #region Fields (1)

        private T2 _item2;

        #endregion Fields

        #region Constructors (3)

        /// <summary>
        /// Initializes a new instance of <see cref="WritableTuple{T1, T2}" /> class.
        /// </summary>
        /// <param name="item1">The value for <see cref="WritableTuple{T1}.Item1" /> property.</param>
        /// <param name="item2">The value for <see cref="WritableTuple{T1, T2}.Item2" /> property.</param>
        public WritableTuple(T1 item1, T2 item2)
            : base(item1)
        {
            this._item2 = item2;
        }
#if !WINDOWS_PHONE

        /// <summary>
        /// Initializes a new instance of <see cref="WritableTuple{T1, T2}" /> class.
        /// </summary>
        /// <param name="info">The serialization context.</param>
        /// <param name="context">The streaming context.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="info" /> is <see langword="null" />.
        /// </exception>
        protected WritableTuple(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {

        }
#endif
        /// <summary>
        /// Initializes a new instance of <see cref="WritableTuple{T1, T2}" /> class.
        /// </summary>
        public WritableTuple()
        {

        }

        #endregion Constructors

        #region Properties (1)

        /// <summary>
        /// Gets or sets the value of the second item of that tuple.
        /// </summary>
        public T2 Item2
        {
            get { return this._item2; }

            set { this._item2 = value; }
        }

        #endregion Properties
    }

    #endregion

    #region CLASS: WritableTuple<T1, T2, T3>

    /// <summary>
    /// A writable tuple with five items.
    /// </summary>
    /// <typeparam name="T1">Type of the first item.</typeparam>
    /// <typeparam name="T2">Type of the second item.</typeparam>
    /// <typeparam name="T3">Type of the third item.</typeparam>
#if !WINDOWS_PHONE
    [global::System.Serializable]
#endif
    public class WritableTuple<T1, T2, T3> : WritableTuple<T1, T2>
    {
        #region Fields (1)

        private T3 _item3;

        #endregion Fields

        #region Constructors (3)

        /// <summary>
        /// Initializes a new instance of <see cref="WritableTuple{T1, T2, T3}" /> class.
        /// </summary>
        /// <param name="item1">The value for <see cref="WritableTuple{T1}.Item1" /> property.</param>
        /// <param name="item2">The value for <see cref="WritableTuple{T1, T2}.Item2" /> property.</param>
        /// <param name="item3">The value for <see cref="WritableTuple{T1, T2, T3}.Item3" /> property.</param>
        public WritableTuple(T1 item1, T2 item2, T3 item3)
            : base(item1, item2)
        {
            this._item3 = item3;
        }
#if !WINDOWS_PHONE

        /// <summary>
        /// Initializes a new instance of <see cref="WritableTuple{T1, T2, T3}" /> class.
        /// </summary>
        /// <param name="info">The serialization context.</param>
        /// <param name="context">The streaming context.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="info" /> is <see langword="null" />.
        /// </exception>
        protected WritableTuple(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {

        }
#endif
        /// <summary>
        /// Initializes a new instance of <see cref="WritableTuple{T1, T2, T3}" /> class.
        /// </summary>
        public WritableTuple()
        {

        }

        #endregion Constructors

        #region Properties (1)

        /// <summary>
        /// Gets or sets the value of the third item of that tuple.
        /// </summary>
        public T3 Item3
        {
            get { return this._item3; }

            set { this._item3 = value; }
        }

        #endregion Properties
    }

    #endregion

    #region CLASS: WritableTuple<T1, T2, T3, T4>

    /// <summary>
    /// A writable tuple with four items.
    /// </summary>
    /// <typeparam name="T1">Type of the first item.</typeparam>
    /// <typeparam name="T2">Type of the second item.</typeparam>
    /// <typeparam name="T3">Type of the third item.</typeparam>
    /// <typeparam name="T4">Type of the fourth item.</typeparam>
#if !WINDOWS_PHONE
    [global::System.Serializable]
#endif
    public class WritableTuple<T1, T2, T3, T4> : WritableTuple<T1, T2, T3>
    {
        #region Fields (1)

        private T4 _item4;

        #endregion Fields

        #region Constructors (3)

        /// <summary>
        /// Initializes a new instance of <see cref="WritableTuple{T1, T2, T3, T4}" /> class.
        /// </summary>
        /// <param name="item1">The value for <see cref="WritableTuple{T1}.Item1" /> property.</param>
        /// <param name="item2">The value for <see cref="WritableTuple{T1, T2}.Item2" /> property.</param>
        /// <param name="item3">The value for <see cref="WritableTuple{T1, T2, T3}.Item3" /> property.</param>
        /// <param name="item4">The value for <see cref="WritableTuple{T1, T2, T3, T4}.Item4" /> property.</param>
        public WritableTuple(T1 item1, T2 item2, T3 item3, T4 item4)
            : base(item1, item2, item3)
        {
            this._item4 = item4;
        }
#if !WINDOWS_PHONE

        /// <summary>
        /// Initializes a new instance of <see cref="WritableTuple{T1, T2, T3, T4}" /> class.
        /// </summary>
        /// <param name="info">The serialization context.</param>
        /// <param name="context">The streaming context.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="info" /> is <see langword="null" />.
        /// </exception>
        protected WritableTuple(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {

        }
#endif
        /// <summary>
        /// Initializes a new instance of <see cref="WritableTuple{T1, T2, T3, T4}" /> class.
        /// </summary>
        public WritableTuple()
        {

        }

        #endregion Constructors

        #region Properties (1)

        /// <summary>
        /// Gets or sets the value of the fourth item of that tuple.
        /// </summary>
        public T4 Item4
        {
            get { return this._item4; }

            set { this._item4 = value; }
        }

        #endregion Properties
    }

    #endregion

    #region CLASS: WritableTuple<T1, T2, T3, T4, T5>

    /// <summary>
    /// A writable tuple with five items.
    /// </summary>
    /// <typeparam name="T1">Type of the first item.</typeparam>
    /// <typeparam name="T2">Type of the second item.</typeparam>
    /// <typeparam name="T3">Type of the third item.</typeparam>
    /// <typeparam name="T4">Type of the fourth item.</typeparam>
    /// <typeparam name="T5">Type of the fifth item.</typeparam>
#if !WINDOWS_PHONE
    [global::System.Serializable]
#endif
    public class WritableTuple<T1, T2, T3, T4, T5> : WritableTuple<T1, T2, T3, T4>
    {
        #region Fields (1)

        private T5 _item5;

        #endregion Fields

        #region Constructors (3)

        /// <summary>
        /// Initializes a new instance of <see cref="WritableTuple{T1, T2, T3, T4, T5}" /> class.
        /// </summary>
        /// <param name="item1">The value for <see cref="WritableTuple{T1}.Item1" /> property.</param>
        /// <param name="item2">The value for <see cref="WritableTuple{T1, T2}.Item2" /> property.</param>
        /// <param name="item3">The value for <see cref="WritableTuple{T1, T2, T3}.Item3" /> property.</param>
        /// <param name="item4">The value for <see cref="WritableTuple{T1, T2, T3, T4}.Item4" /> property.</param>
        /// <param name="item5">The value for <see cref="WritableTuple{T1, T2, T3, T4, T5}.Item5" /> property.</param>
        public WritableTuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
            : base(item1, item2, item3, item4)
        {
            this._item5 = item5;
        }
#if !WINDOWS_PHONE

        /// <summary>
        /// Initializes a new instance of <see cref="WritableTuple{T1, T2, T3, T4, T5}" /> class.
        /// </summary>
        /// <param name="info">The serialization context.</param>
        /// <param name="context">The streaming context.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="info" /> is <see langword="null" />.
        /// </exception>
        protected WritableTuple(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {

        }
#endif
        /// <summary>
        /// Initializes a new instance of <see cref="WritableTuple{T1, T2, T3, T4, T5}" /> class.
        /// </summary>
        public WritableTuple()
        {

        }

        #endregion Constructors

        #region Properties (1)

        /// <summary>
        /// Gets or sets the value of the fifth item of that tuple.
        /// </summary>
        public T5 Item5
        {
            get { return this._item5; }

            set { this._item5 = value; }
        }

        #endregion Properties
    }

    #endregion

    #region CLASS: WritableTuple<T1, T2, T3, T4, T5, T6>

    /// <summary>
    /// A writable tuple with six items.
    /// </summary>
    /// <typeparam name="T1">Type of the first item.</typeparam>
    /// <typeparam name="T2">Type of the second item.</typeparam>
    /// <typeparam name="T3">Type of the third item.</typeparam>
    /// <typeparam name="T4">Type of the fourth item.</typeparam>
    /// <typeparam name="T5">Type of the fifth item.</typeparam>
    /// <typeparam name="T6">Type of the sixth item.</typeparam>
#if !WINDOWS_PHONE
    [global::System.Serializable]
#endif
    public class WritableTuple<T1, T2, T3, T4, T5, T6> : WritableTuple<T1, T2, T3, T4, T5>
    {
        #region Fields (1)

        private T6 _item6;

        #endregion Fields

        #region Constructors (3)

        /// <summary>
        /// Initializes a new instance of <see cref="WritableTuple{T1, T2, T3, T4, T5, T6}" /> class.
        /// </summary>
        /// <param name="item1">The value for <see cref="WritableTuple{T1}.Item1" /> property.</param>
        /// <param name="item2">The value for <see cref="WritableTuple{T1, T2}.Item2" /> property.</param>
        /// <param name="item3">The value for <see cref="WritableTuple{T1, T2, T3}.Item3" /> property.</param>
        /// <param name="item4">The value for <see cref="WritableTuple{T1, T2, T3, T4}.Item4" /> property.</param>
        /// <param name="item5">The value for <see cref="WritableTuple{T1, T2, T3, T4, T5}.Item5" /> property.</param>
        /// <param name="item6">The value for <see cref="WritableTuple{T1, T2, T3, T4, T5, T6}.Item6" /> property.</param>
        public WritableTuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
            : base(item1, item2, item3, item4, item5)
        {
            this._item6 = item6;
        }
#if !WINDOWS_PHONE

        /// <summary>
        /// Initializes a new instance of <see cref="WritableTuple{T1, T2, T3, T4, T5, T6}" /> class.
        /// </summary>
        /// <param name="info">The serialization context.</param>
        /// <param name="context">The streaming context.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="info" /> is <see langword="null" />.
        /// </exception>
        protected WritableTuple(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {

        }
#endif
        /// <summary>
        /// Initializes a new instance of <see cref="WritableTuple{T1, T2, T3, T4, T5, T6}" /> class.
        /// </summary>
        public WritableTuple()
        {

        }

        #endregion Constructors

        #region Properties (1)

        /// <summary>
        /// Gets or sets the value of the sixth item of that tuple.
        /// </summary>
        public T6 Item6
        {
            get { return this._item6; }

            set { this._item6 = value; }
        }

        #endregion Properties
    }

    #endregion

    #region CLASS: WritableTuple<T1, T2, T3, T4, T5, T6, T7>

    /// <summary>
    /// A writable tuple with seven items.
    /// </summary>
    /// <typeparam name="T1">Type of the first item.</typeparam>
    /// <typeparam name="T2">Type of the second item.</typeparam>
    /// <typeparam name="T3">Type of the third item.</typeparam>
    /// <typeparam name="T4">Type of the fourth item.</typeparam>
    /// <typeparam name="T5">Type of the fifth item.</typeparam>
    /// <typeparam name="T6">Type of the sixth item.</typeparam>
    /// <typeparam name="T7">Type of the seventh item.</typeparam>
#if !WINDOWS_PHONE
    [global::System.Serializable]
#endif
    public class WritableTuple<T1, T2, T3, T4, T5, T6, T7> : WritableTuple<T1, T2, T3, T4, T5, T6>
    {
        #region Fields (1)

        private T7 _item7;

        #endregion Fields

        #region Constructors (3)

        /// <summary>
        /// Initializes a new instance of <see cref="WritableTuple{T1, T2, T3, T4, T5, T6, T7}" /> class.
        /// </summary>
        /// <param name="item1">The value for <see cref="WritableTuple{T1}.Item1" /> property.</param>
        /// <param name="item2">The value for <see cref="WritableTuple{T1, T2}.Item2" /> property.</param>
        /// <param name="item3">The value for <see cref="WritableTuple{T1, T2, T3}.Item3" /> property.</param>
        /// <param name="item4">The value for <see cref="WritableTuple{T1, T2, T3, T4}.Item4" /> property.</param>
        /// <param name="item5">The value for <see cref="WritableTuple{T1, T2, T3, T4, T5}.Item5" /> property.</param>
        /// <param name="item6">The value for <see cref="WritableTuple{T1, T2, T3, T4, T5, T6}.Item6" /> property.</param>
        /// <param name="item7">The value for <see cref="WritableTuple{T1, T2, T3, T4, T5, T6, T7}.Item7" /> property.</param>
        public WritableTuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
            : base(item1, item2, item3, item4, item5, item6)
        {
            this._item7 = item7;
        }
#if !WINDOWS_PHONE

        /// <summary>
        /// Initializes a new instance of <see cref="WritableTuple{T1, T2, T3, T4, T5, T6, T7}" /> class.
        /// </summary>
        /// <param name="info">The serialization context.</param>
        /// <param name="context">The streaming context.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="info" /> is <see langword="null" />.
        /// </exception>
        protected WritableTuple(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {

        }
#endif
        /// <summary>
        /// Initializes a new instance of <see cref="WritableTuple{T1, T2, T3, T4, T5, T6, T7}" /> class.
        /// </summary>
        public WritableTuple()
        {

        }

        #endregion Constructors

        #region Properties (1)

        /// <summary>
        /// Gets or sets the value of the seventh item of that tuple.
        /// </summary>
        public T7 Item7
        {
            get { return this._item7; }

            set { this._item7 = value; }
        }

        #endregion Properties
    }

    #endregion

    #region CLASS: WritableTuple<T1, T2, T3, T4, T5, T6, T7, TRest>

    /// <summary>
    /// A writable tuple with seven items and an extension item.
    /// </summary>
    /// <typeparam name="T1">Type of the first item.</typeparam>
    /// <typeparam name="T2">Type of the second item.</typeparam>
    /// <typeparam name="T3">Type of the third item.</typeparam>
    /// <typeparam name="T4">Type of the fourth item.</typeparam>
    /// <typeparam name="T5">Type of the fifth item.</typeparam>
    /// <typeparam name="T6">Type of the sixth item.</typeparam>
    /// <typeparam name="T7">Type of the seventh item.</typeparam>
    /// <typeparam name="TRest">Type of the extension item.</typeparam>
#if !WINDOWS_PHONE
    [global::System.Serializable]
#endif
    public sealed class WritableTuple<T1, T2, T3, T4, T5, T6, T7, TRest> : WritableTuple<T1, T2, T3, T4, T5, T6, T7>
    {
        #region Fields (1)

        private TRest _rest;

        #endregion Fields

        #region Constructors (3)

        /// <summary>
        /// Initializes a new instance of <see cref="WritableTuple{T1, T2, T3, T4, T5, T6, T7, TRest}" /> class.
        /// </summary>
        /// <param name="item1">The value for <see cref="WritableTuple{T1}.Item1" /> property.</param>
        /// <param name="item2">The value for <see cref="WritableTuple{T1, T2}.Item2" /> property.</param>
        /// <param name="item3">The value for <see cref="WritableTuple{T1, T2, T3}.Item3" /> property.</param>
        /// <param name="item4">The value for <see cref="WritableTuple{T1, T2, T3, T4}.Item4" /> property.</param>
        /// <param name="item5">The value for <see cref="WritableTuple{T1, T2, T3, T4, T5}.Item5" /> property.</param>
        /// <param name="item6">The value for <see cref="WritableTuple{T1, T2, T3, T4, T5, T6}.Item6" /> property.</param>
        /// <param name="item7">The value for <see cref="WritableTuple{T1, T2, T3, T4, T5, T6, T7}.Item7" /> property.</param>
        /// <param name="rest">The value for <see cref="WritableTuple{T1, T2, T3, T4, T5, T6, T7, TRest}.Rest" /> property.</param>
        public WritableTuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, TRest rest)
            : base(item1, item2, item3, item4, item5, item6, item7)
        {
            this._rest = rest;
        }
#if !WINDOWS_PHONE

        private WritableTuple(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {

        }
#endif
        /// <summary>
        /// Initializes a new instance of <see cref="WritableTuple{T1, T2, T3, T4, T5, T6, T7, TRest}" /> class.
        /// </summary>
        public WritableTuple()
        {

        }

        #endregion Constructors

        #region Properties (1)

        /// <summary>
        /// Gets or sets the value of the extension item of that tuple.
        /// </summary>
        public TRest Rest
        {
            get { return this._rest; }

            set { this._rest = value; }
        }

        #endregion Properties
    }

    #endregion
}
