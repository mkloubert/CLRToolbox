using NUnit.Framework;
using System;

namespace MarcelJoachimKloubert.CLRToolbox.Tests
{
    [TestFixture]
    public class RandomTests
    {
        #region Methods

        [Test]
        public void CryptoRandomTest1()
        {
            CryptoRandom r = new CryptoRandom();

            for (int i = 0; i < 1000000; i++)
            {
                int v1 = r.Next(0, 1000);
                Assert.IsTrue(v1 >= 0 && v1 < 1000);

                int v2 = r.Next(-1000, 100000);
                Assert.IsTrue(v2 >= -1000 && v1 < 100000);

                int v3 = r.Next(0, 0);
                Assert.IsFalse(v3 != 0);

                int v4 = r.Next(-1, 1);
                Assert.IsTrue(v4 >= -1 && v4 < 1);
            }
        }

        [Test]
        public void CryptoRandomSeed()
        {
            CryptoRandom r = CryptoRandom.Create(Guid.NewGuid().ToByteArray());

            for (int i = 0; i < 1000000; i++)
            {
                int v1 = r.Next(0, 10000);
                Assert.IsTrue(v1 >= 0 && v1 < 10000);

                int v2 = r.Next(-2100, 107000);
                Assert.IsTrue(v2 >= -2100 && v1 < 107000);

                int v3 = r.Next(1, 1);
                Assert.IsFalse(v3 != 1);

                int v4 = r.Next(-1, 1);
                Assert.IsTrue(v4 >= -1 && v4 < 1);
            }
        }

        #endregion Methods
    }
}