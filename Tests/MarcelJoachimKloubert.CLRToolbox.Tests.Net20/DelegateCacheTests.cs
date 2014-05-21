using MarcelJoachimKloubert.CLRToolbox.Caching;
using MarcelJoachimKloubert.CLRToolbox.Diagnostics.Tests;
using System;
using System.Threading;

namespace MarcelJoachimKloubert.CLRToolbox.Tests
{
    [TestFixture]
    public partial class DelegateCacheTests
    {
        #region Methods

        [Test]
        public void ActionTest()
        {
            DelegateCache cache = new DelegateCache();

            int val = -1;
            DelegateCache.CachedAction action = delegate()
                {
                    ++val;
                };

            cache.SaveAction(action, TimeSpan.FromSeconds(3));

            for (int i = 0; i < 4; i++)
            {
                cache.InvokeAction(action);

                if (i < 3)
                {
                    Assert.AreEqual(val, 0);
                }
                else
                {
                    Assert.AreEqual(val, 1);
                }

                Thread.Sleep(1100);
            }
        }

        [Test]
        public void FuncTest()
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