using MarcelJoachimKloubert.CLRToolbox.Caching;
using NUnit.Framework;
using System;
using System.Threading;

namespace MarcelJoachimKloubert.CLRToolbox.Tests
{
    [TestFixture]
    public class DelegateCacheTests
    {
        #region Methods

        [Test]
        public void Test1()
        {
            DelegateCache cache = new DelegateCache();

            int val = 0;
            DelegateCache.CachedFunc<int> func = delegate()
                {
                    return val++;
                };

            cache.SaveFunc(func, TimeSpan.FromSeconds(5));

            for (int i = 0; i < 6; i++)
            {
                int v = cache.InvokeFunc(func);

                if (i < 5)
                {
                    Assert.AreEqual(v, 0);
                }
                else
                {
                    Assert.AreEqual(v, 1);
                }

                Thread.Sleep(1100);
            }
        }

        #endregion Methods
    }
}