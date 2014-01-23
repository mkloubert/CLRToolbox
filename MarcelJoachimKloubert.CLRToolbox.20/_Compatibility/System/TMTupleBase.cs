// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using MarcelJoachimKloubert.CLRToolbox.Helpers;

namespace System
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class TMTupleBase : ITuple, IStructuralComparable, IStructuralEquatable, IComparable
    {
        #region Properties (1)

        int ITuple.Size
        {
            get { return this.GetTupleFields().Length; }
        }

        #endregion Properties

        #region Methods (9)

        // Public Methods (1) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="object.ToString()" />
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append("(");

            return ((ITuple)this).ToString(result);
        }
        // Private Methods (8) 

        private FieldInfo[] GetTupleFields()
        {
            return this.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
        }

        private object[] GetTupleFieldValues()
        {
            IEnumerable<object> values = CollectionHelper.Select(this.GetTupleFields(),
                                                                 delegate(FieldInfo f)
                                                                 {
                                                                     return f.GetValue(this);
                                                                 });

            return CollectionHelper.ToArray(values);
        }

        int IComparable.CompareTo(object obj)
        {
            return ((IStructuralComparable)this).CompareTo(obj, Comparer<object>.Default);
        }

        int IStructuralComparable.CompareTo(object other, IComparer comparer)
        {
            if (other == null)
            {
                return 1;
            }

            TMTupleBase otherTuple = other as TMTupleBase;
            if (otherTuple == null ||
                !(otherTuple.GetType().Equals(this.GetType())))
            {
                throw new ArgumentException("other");
            }

            object[] thisValues = this.GetTupleFieldValues();
            object[] otherValues = otherTuple.GetTupleFieldValues();
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

        bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
        {
            TMTupleBase otherTuple = other as TMTupleBase;
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

            return CollectionHelper.SequenceEqual(this.GetTupleFieldValues(),
                                                  otherTuple.GetTupleFieldValues());
        }

        int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
        {
            IEnumerable<object> hashCodes = CollectionHelper.Select(this.GetTupleFieldValues(),
                                                                    delegate(object v)
                                                                    {
                                                                        return (object)comparer.GetHashCode(v);
                                                                    });

            MethodInfo getHashCodeMethod = CollectionHelper.Single(typeof(global::System.Tuple).GetMethods(BindingFlags.NonPublic | BindingFlags.Static),
                                                                   delegate(MethodInfo m)
                                                                   {
                                                                       return m.Name == "CombineHashCodes" &&
                                                                              m.GetParameters().Length == this.GetTupleFields().Length;
                                                                   });

            return (int)getHashCodeMethod.Invoke(null,
                                                 CollectionHelper.ToArray(hashCodes));
        }

        int ITuple.GetHashCode(IEqualityComparer comparer)
        {
            return ((IStructuralEquatable)this).GetHashCode(comparer);
        }

        string ITuple.ToString(StringBuilder sb)
        {
            StringBuilder result = new StringBuilder();

            FieldInfo[] fields = this.GetTupleFields();
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
}
