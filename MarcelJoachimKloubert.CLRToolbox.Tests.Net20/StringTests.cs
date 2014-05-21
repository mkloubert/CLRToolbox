using MarcelJoachimKloubert.CLRToolbox.Helpers;
using NUnit.Framework;
using System;

namespace MarcelJoachimKloubert.CLRToolbox.Tests
{
    [TestFixture]
    public partial class StringTests
    {
        #region Methods

        [Test(Description = "'AsString' helper method")]
        public void AsString()
        {
            string a = "TM";
            object b = "MK".ToCharArray();
            object c = a.ToCharArray();
            object d = null;
            object e = string.Empty;
            object f = DBNull.Value;
            DateTime? g = new DateTime(1979, 9, 5, 23, 9, 19, 79);
            DateTime? h = null;

            // check 'a'
            Assert.AreEqual(a,
                            StringHelper.AsString(a));

            // check 'b'
            Assert.AreEqual(b,
                            StringHelper.AsString(b));

            // 'a' == 'c'
            Assert.AreEqual(a,
                            StringHelper.AsString(c));

            // 'd' == null
            Assert.IsNull(StringHelper.AsString(d));

            // 'e' != null
            Assert.IsNotNull(StringHelper.AsString(e));

            // DBNull.Value == null
            Assert.IsNull(StringHelper.AsString(f));

            // DBNull.Value != null
            Assert.IsNotNull(StringHelper.AsString(f, false));

            // 'g' != null
            Assert.IsNotNull(StringHelper.AsString(g));

            // 'h' == null
            Assert.IsNull(StringHelper.AsString(h));

            // check 'g'
            Assert.AreEqual(StringHelper.AsString(g),
                            g.Value.ToString());
        }

        [Test(Description = "'IsNullOrWhiteSpace' helper method")]
        public void IsNullOrWhiteSpace()
        {
            string a = "TM";
            string b = null;
            char[] c = "MK".ToCharArray();
            string d = string.Empty;
            char[] e = "  ".ToCharArray();
            char[] f = null;

            // check 'a'
            Assert.IsFalse(StringHelper.IsNullOrWhiteSpace(a));

            // check 'b'
            Assert.IsTrue(StringHelper.IsNullOrWhiteSpace(b));

            // check 'c'
            Assert.IsFalse(StringHelper.IsNullOrWhiteSpace(c));

            // check 'd'
            Assert.IsTrue(StringHelper.IsNullOrWhiteSpace(d));

            // check 'e'
            Assert.IsTrue(StringHelper.IsNullOrWhiteSpace(e));

            // check 'f'
            Assert.IsTrue(StringHelper.IsNullOrWhiteSpace(f));
        }

        #endregion Methods
    }
}