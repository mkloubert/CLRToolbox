// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Reflection;
using System.Text;

namespace System
{
    #region CLASS: Tuple

    /// <summary>
    /// 
    /// </summary>
    /// <see href="http://msdn.microsoft.com/en-us/library/system.tuple%28v=vs.110%29.aspx" />
    public static class Tuple
    {
        #region Methods (17)

        // Public Methods (8) 

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd384265%28v=vs.110%29.aspx" />
        public static Tuple<T1> Create<T1>(T1 item1)
        {
            return new Tuple<T1>(item1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd387181%28v=vs.110%29.aspx" />
        public static Tuple<T1, T2> Create<T1, T2>(T1 item1, T2 item2)
        {
            return new Tuple<T1, T2>(item1, item2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd383822%28v=vs.110%29.aspx" />
        public static Tuple<T1, T2, T3> Create<T1, T2, T3>(T1 item1, T2 item2, T3 item3)
        {
            return new Tuple<T1, T2, T3>(item1, item2, item3);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd413709%28v=vs.110%29.aspx" />
        public static Tuple<T1, T2, T3, T4> Create<T1, T2, T3, T4>(T1 item1, T2 item2, T3 item3, T4 item4)
        {
            return new Tuple<T1, T2, T3, T4>(item1, item2, item3, item4);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd387087%28v=vs.110%29.aspx" />
        public static Tuple<T1, T2, T3, T4, T5> Create<T1, T2, T3, T4, T5>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
        {
            return new Tuple<T1, T2, T3, T4, T5>(item1, item2, item3, item4, item5);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd386886%28v=vs.110%29.aspx" />
        public static Tuple<T1, T2, T3, T4, T5, T6> Create<T1, T2, T3, T4, T5, T6>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
        {
            return new Tuple<T1, T2, T3, T4, T5, T6>(item1, item2, item3, item4, item5, item6);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd387145%28v=vs.110%29.aspx" />
        public static Tuple<T1, T2, T3, T4, T5, T6, T7> Create<T1, T2, T3, T4, T5, T6, T7>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
        {
            return new Tuple<T1, T2, T3, T4, T5, T6, T7>(item1, item2, item3, item4, item5, item6, item7);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd386921%28v=vs.110%29.aspx" />
        public static Tuple<T1, T2, T3, T4, T5, T6, T7, T8> Create<T1, T2, T3, T4, T5, T6, T7, T8>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8)
        {
            return new Tuple<T1, T2, T3, T4, T5, T6, T7, T8>(item1, item2, item3, item4, item5, item6, item7, item8);
        }
        // Internal Methods (9) 

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
            return Tuple.CombineHashCodes(Tuple.CombineHashCodes(h1, h2), h3);
        }

        internal static int CombineHashCodes(int h1, int h2, int h3, int h4)
        {
            return Tuple.CombineHashCodes(Tuple.CombineHashCodes(h1, h2), Tuple.CombineHashCodes(h3, h4));
        }

        internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5)
        {
            return Tuple.CombineHashCodes(Tuple.CombineHashCodes(h1, h2, h3, h4), h5);
        }

        internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6)
        {
            return Tuple.CombineHashCodes(Tuple.CombineHashCodes(h1, h2, h3, h4), Tuple.CombineHashCodes(h5, h6));
        }

        internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6, int h7)
        {
            return Tuple.CombineHashCodes(Tuple.CombineHashCodes(h1, h2, h3, h4), Tuple.CombineHashCodes(h5, h6, h7));
        }

        internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8)
        {
            return Tuple.CombineHashCodes(Tuple.CombineHashCodes(h1, h2, h3, h4), Tuple.CombineHashCodes(h5, h6, h7, h8));
        }

        internal static string ToString(ITuple tuple)
        {
            StringBuilder result = new StringBuilder();
            result.Append("(");

            FieldInfo[] fields = tuple.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            for (int i = 0; i < fields.Length; i++)
            {
                if (i > 0)
                {
                    result.Append(", ");
                }

                FieldInfo f = fields[i];

            }

            result.Append(")");

            return result.ToString();
        }

        #endregion Methods
    }

    #endregion

    #region CLASS: Tuple<T1>

    /// <summary>
    /// 
    /// </summary>
    /// <see href="http://msdn.microsoft.com/en-us/library/dd386941%28v=vs.110%29.aspx" />
#if !WINDOWS_PHONE
    [global::System.Serializable]
#endif
    public class Tuple<T1> : TMTupleBase
    {
        #region Fields (1)

        private readonly T1 _ITEM1;

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd386914%28v=vs.110%29.aspx" />
        public Tuple(T1 item1)
        {
            this._ITEM1 = item1;
        }

        #endregion Constructors

        #region Properties (1)

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd386926%28v=vs.110%29.aspx" />
        public T1 Item1
        {
            get { return this._ITEM1; }
        }

        #endregion Properties
    }

    #endregion

    #region CLASS: Tuple<T1, T2>

    /// <summary>
    /// 
    /// </summary>
    /// <see href="http://msdn.microsoft.com/en-us/library/dd268536%28v=vs.110%29.aspx" />
#if !WINDOWS_PHONE
    [global::System.Serializable]
#endif
    public class Tuple<T1, T2> : TMTupleBase
    {
        #region Fields (2)

        private readonly T1 _ITEM1;
        private readonly T2 _ITEM2;

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd267681%28v=vs.110%29.aspx" />
        public Tuple(T1 item1, T2 item2)
        {
            this._ITEM1 = item1;
            this._ITEM2 = item2;
        }

        #endregion Constructors

        #region Properties (2)

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd386940%28v=vs.110%29.aspx" />
        public T1 Item1
        {
            get { return this._ITEM1; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd386892%28v=vs.110%29.aspx" />
        public T2 Item2
        {
            get { return this._ITEM2; }
        }

        #endregion Properties
    }

    #endregion

    #region CLASS: Tuple<T1, T2, T3>

    /// <summary>
    /// 
    /// </summary>
    /// <see href="http://msdn.microsoft.com/en-us/library/dd387150%28v=vs.110%29.aspx" />
#if !WINDOWS_PHONE
    [global::System.Serializable]
#endif
    public class Tuple<T1, T2, T3> : TMTupleBase
    {
        #region Fields (3)

        private readonly T1 _ITEM1;
        private readonly T2 _ITEM2;
        private readonly T3 _ITEM3;

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd387244%28v=vs.110%29.aspx" />
        public Tuple(T1 item1, T2 item2, T3 item3)
        {
            this._ITEM1 = item1;
            this._ITEM2 = item2;
            this._ITEM3 = item3;
        }

        #endregion Constructors

        #region Properties (3)

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd387110%28v=vs.110%29.aspx" />
        public T1 Item1
        {
            get { return this._ITEM1; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd387039%28v=vs.110%29.aspx" />
        public T2 Item2
        {
            get { return this._ITEM2; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd386924%28v=vs.110%29.aspx" />
        public T3 Item3
        {
            get { return this._ITEM3; }
        }

        #endregion Properties
    }

    #endregion

    #region CLASS: Tuple<T1, T2, T3, T4>

    /// <summary>
    /// 
    /// </summary>
    /// <see href="http://msdn.microsoft.com/en-us/library/dd414846%28v=vs.110%29.aspx" />
#if !WINDOWS_PHONE
    [global::System.Serializable]
#endif
    public class Tuple<T1, T2, T3, T4> : TMTupleBase
    {
        #region Fields (4)

        private readonly T1 _ITEM1;
        private readonly T2 _ITEM2;
        private readonly T3 _ITEM3;
        private readonly T4 _ITEM4;

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd383935%28v=vs.110%29.aspx" />
        public Tuple(T1 item1, T2 item2, T3 item3, T4 item4)
        {
            this._ITEM1 = item1;
            this._ITEM2 = item2;
            this._ITEM3 = item3;
            this._ITEM4 = item4;
        }

        #endregion Constructors

        #region Properties (4)

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd414788%28v=vs.110%29.aspx" />
        public T1 Item1
        {
            get { return this._ITEM1; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd414849%28v=vs.110%29.aspx" />
        public T2 Item2
        {
            get { return this._ITEM2; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd386933%28v=vs.110%29.aspx" />
        public T3 Item3
        {
            get { return this._ITEM3; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd386867%28v=vs.110%29.aspx" />
        public T4 Item4
        {
            get { return this._ITEM4; }
        }

        #endregion Properties
    }

    #endregion

    #region CLASS: Tuple<T1, T2, T3, T4, T5>

    /// <summary>
    /// 
    /// </summary>
    /// <see href="http://msdn.microsoft.com/en-us/library/dd414892%28v=vs.110%29.aspx" />
#if !WINDOWS_PHONE
    [global::System.Serializable]
#endif
    public class Tuple<T1, T2, T3, T4, T5> : TMTupleBase
    {
        #region Fields (5)

        private readonly T1 _ITEM1;
        private readonly T2 _ITEM2;
        private readonly T3 _ITEM3;
        private readonly T4 _ITEM4;
        private readonly T5 _ITEM5;

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd383323%28v=vs.110%29.aspx" />
        public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
        {
            this._ITEM1 = item1;
            this._ITEM2 = item2;
            this._ITEM3 = item3;
            this._ITEM4 = item4;
            this._ITEM5 = item5;
        }

        #endregion Constructors

        #region Properties (5)

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd413591%28v=vs.110%29.aspx" />
        public T1 Item1
        {
            get { return this._ITEM1; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd414851%28v=vs.110%29.aspx" />
        public T2 Item2
        {
            get { return this._ITEM2; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd383497%28v=vs.110%29.aspx" />
        public T3 Item3
        {
            get { return this._ITEM3; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd386968%28v=vs.110%29.aspx" />
        public T4 Item4
        {
            get { return this._ITEM4; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd386890%28v=vs.110%29.aspx" />
        public T5 Item5
        {
            get { return this._ITEM5; }
        }

        #endregion Properties
    }

    #endregion

    #region CLASS: Tuple<T1, T2, T3, T4, T5, T6>

    /// <summary>
    /// 
    /// </summary>
    /// <see href="http://msdn.microsoft.com/en-us/library/dd386877%28v=vs.110%29.aspx" />
#if !WINDOWS_PHONE
    [global::System.Serializable]
#endif
    public class Tuple<T1, T2, T3, T4, T5, T6> : TMTupleBase
    {
        #region Fields (6)

        private readonly T1 _ITEM1;
        private readonly T2 _ITEM2;
        private readonly T3 _ITEM3;
        private readonly T4 _ITEM4;
        private readonly T5 _ITEM5;
        private readonly T6 _ITEM6;

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd384358%28v=vs.110%29.aspx" />
        public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
        {
            this._ITEM1 = item1;
            this._ITEM2 = item2;
            this._ITEM3 = item3;
            this._ITEM4 = item4;
            this._ITEM5 = item5;
            this._ITEM6 = item6;
        }

        #endregion Constructors

        #region Properties (6)

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd386907%28v=vs.110%29.aspx" />
        public T1 Item1
        {
            get { return this._ITEM1; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd414893%28v=vs.110%29.aspx" />
        public T2 Item2
        {
            get { return this._ITEM2; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd386896%28v=vs.110%29.aspx" />
        public T3 Item3
        {
            get { return this._ITEM3; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd386874%28v=vs.110%29.aspx" />
        public T4 Item4
        {
            get { return this._ITEM4; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd414848%28v=vs.110%29.aspx" />
        public T5 Item5
        {
            get { return this._ITEM5; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd384078%28v=vs.110%29.aspx" />
        public T6 Item6
        {
            get { return this._ITEM6; }
        }

        #endregion Properties
    }

    #endregion

    #region CLASS: Tuple<T1, T2, T3, T4, T5, T6, T7>

    /// <summary>
    /// 
    /// </summary>
    /// <see href="http://msdn.microsoft.com/en-us/library/dd387185%28v=vs.110%29.aspx" />
#if !WINDOWS_PHONE
    [global::System.Serializable]
#endif
    public class Tuple<T1, T2, T3, T4, T5, T6, T7> : TMTupleBase
    {
        #region Fields (7)

        private readonly T1 _ITEM1;
        private readonly T2 _ITEM2;
        private readonly T3 _ITEM3;
        private readonly T4 _ITEM4;
        private readonly T5 _ITEM5;
        private readonly T6 _ITEM6;
        private readonly T7 _ITEM7;

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd414891%28v=vs.110%29.aspx" />
        public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
        {
            this._ITEM1 = item1;
            this._ITEM2 = item2;
            this._ITEM3 = item3;
            this._ITEM4 = item4;
            this._ITEM5 = item5;
            this._ITEM6 = item6;
            this._ITEM7 = item7;
        }

        #endregion Constructors

        #region Properties (7)

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd387176%28v=vs.110%29.aspx" />
        public T1 Item1
        {
            get { return this._ITEM1; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd387147%28v=vs.110%29.aspx" />
        public T2 Item2
        {
            get { return this._ITEM2; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd387177%28v=vs.110%29.aspx" />
        public T3 Item3
        {
            get { return this._ITEM3; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd386897%28v=vs.110%29.aspx" />
        public T4 Item4
        {
            get { return this._ITEM4; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd413854%28v=vs.110%29.aspx" />
        public T5 Item5
        {
            get { return this._ITEM5; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd386973%28v=vs.110%29.aspx" />
        public T6 Item6
        {
            get { return this._ITEM6; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd383934%28v=vs.110%29.aspx" />
        public T7 Item7
        {
            get { return this._ITEM7; }
        }

        #endregion Properties
    }

    #endregion

    #region CLASS: Tuple<T1, T2, T3, T4, T5, T6, T7, TRest>

    /// <summary>
    /// 
    /// </summary>
    /// <see href="http://msdn.microsoft.com/en-us/library/dd383325%28v=vs.110%29.aspx" />
#if !WINDOWS_PHONE
    [global::System.Serializable]
#endif
    public class Tuple<T1, T2, T3, T4, T5, T6, T7, TRest> : TMTupleBase
    {
        #region Fields (8)

        private readonly T1 _ITEM1;
        private readonly T2 _ITEM2;
        private readonly T3 _ITEM3;
        private readonly T4 _ITEM4;
        private readonly T5 _ITEM5;
        private readonly T6 _ITEM6;
        private readonly T7 _ITEM7;
        private readonly TRest _REST;

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd387149%28v=vs.110%29.aspx" />
        public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, TRest r)
        {
            this._ITEM1 = item1;
            this._ITEM2 = item2;
            this._ITEM3 = item3;
            this._ITEM4 = item4;
            this._ITEM5 = item5;
            this._ITEM6 = item6;
            this._ITEM7 = item7;
            this._REST = r;
        }

        #endregion Constructors

        #region Properties (8)

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd386908%28v=vs.110%29.aspx" />
        public T1 Item1
        {
            get { return this._ITEM1; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd384357%28v=vs.110%29.aspx" />
        public T2 Item2
        {
            get { return this._ITEM2; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd386865%28v=vs.110%29.aspx" />
        public T3 Item3
        {
            get { return this._ITEM3; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd384077%28v=vs.110%29.aspx" />
        public T4 Item4
        {
            get { return this._ITEM4; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd413386%28v=vs.110%29.aspx" />
        public T5 Item5
        {
            get { return this._ITEM5; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd386943%28v=vs.110%29.aspx" />
        public T6 Item6
        {
            get { return this._ITEM6; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd386938%28v=vs.110%29.aspx" />
        public T7 Item7
        {
            get { return this._ITEM7; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd386918%28v=vs.110%29.aspx" />
        public TRest Rest
        {
            get { return this._REST; }
        }

        #endregion Properties
    }

    #endregion
}
