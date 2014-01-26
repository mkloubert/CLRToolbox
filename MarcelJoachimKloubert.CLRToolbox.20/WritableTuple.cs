// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


namespace MarcelJoachimKloubert.CLRToolbox
{
    #region CLASS: WritableTuple

    /// <summary>
    /// Factory for creating wrtable tuples.
    /// </summary>
    public static class WritableTuple
    {
        #region Methods (8)

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

        #endregion Methods
    }

    #endregion

    #region CLASS: WritableTuple<T1>

    /// <summary>
    /// A writable tuple with one item.
    /// </summary>
    /// <typeparam name="T1">Type of the first item.</typeparam>
    public class WritableTuple<T1>
    {
        #region Fields (1)

        private T1 _item1;

        #endregion Fields

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of <see cref="WritableTuple{T1}" /> class.
        /// </summary>
        /// <param name="item1">The value for <see cref="WritableTuple{T1}.Item1" /> property.</param>
        public WritableTuple(T1 item1)
        {
            this._item1 = item1;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="WritableTuple{T1}" /> class.
        /// </summary>
        public WritableTuple()
        {

        }

        #endregion Constructors

        #region Properties (1)

        /// <summary>
        /// Gets or sets the value of the first item of that tuple.
        /// </summary>
        public T1 Item1
        {
            get { return this._item1; }

            set { this._item1 = value; }
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
    public class WritableTuple<T1, T2> : WritableTuple<T1>
    {
        #region Fields (1)

        private T2 _item2;

        #endregion Fields

        #region Constructors (2)

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
    public class WritableTuple<T1, T2, T3> : WritableTuple<T1, T2>
    {
        #region Fields (1)

        private T3 _item3;

        #endregion Fields

        #region Constructors (2)

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
    public class WritableTuple<T1, T2, T3, T4> : WritableTuple<T1, T2, T3>
    {
        #region Fields (1)

        private T4 _item4;

        #endregion Fields

        #region Constructors (2)

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
    public class WritableTuple<T1, T2, T3, T4, T5> : WritableTuple<T1, T2, T3, T4>
    {
        #region Fields (1)

        private T5 _item5;

        #endregion Fields

        #region Constructors (2)

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
    public class WritableTuple<T1, T2, T3, T4, T5, T6> : WritableTuple<T1, T2, T3, T4, T5>
    {
        #region Fields (1)

        private T6 _item6;

        #endregion Fields

        #region Constructors (2)

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
    public class WritableTuple<T1, T2, T3, T4, T5, T6, T7> : WritableTuple<T1, T2, T3, T4, T5, T6>
    {
        #region Fields (1)

        private T7 _item7;

        #endregion Fields

        #region Constructors (2)

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
    public class WritableTuple<T1, T2, T3, T4, T5, T6, T7, TRest> : WritableTuple<T1, T2, T3, T4, T5, T6, T7>
    {
        #region Fields (1)

        private TRest _rest;

        #endregion Fields

        #region Constructors (2)

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
