using MarcelJoachimKloubert.CLRToolbox.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Tests
{
    [TestFixture]
    public partial class CollectionTests
    {
        #region Fields (1)

        private readonly Random _RANDOM = new Random();

        #endregion Fields

        #region Methods

        [Test]
        public void AsArrayMethod()
        {
            int numberOfItems = this._RANDOM.Next(5000, 10000);

            IEnumerable<int> numbers = CollectionHelper.Range(0, numberOfItems);
            int[] numberArray = CollectionHelper.ToArray(numbers);

            Assert.AreNotSame(numbers,
                              CollectionHelper.AsArray(numbers));

            Assert.AreSame(numberArray,
                           CollectionHelper.AsArray(numberArray));
        }

        [Test]
        public void ForEachMethod()
        {
            int numberOfItems = this._RANDOM.Next(5000, 10000);

            IEnumerable<int> numbers = CollectionHelper.Range(0, numberOfItems);

            int index = 0;
            CollectionHelper.ForEach(numbers,
                                     delegate(IForEachItemExecutionContext<int> ctx)
                                     {
                                         Assert.AreEqual(ctx.Item, index++);
                                     });

            Assert.AreEqual(numberOfItems,
                            index);
        }

        /// <summary>
        /// Test <see cref="CollectionHelper.Range(int, int)" /> method.
        /// </summary>
        [Test]
        public void RangeMethod()
        {
            int numberOfItems = this._RANDOM.Next(5000, 10000);

            IEnumerable<int> numbers = CollectionHelper.Range(0, numberOfItems);

            int index = 0;
            foreach (int n in numbers)
            {
                Assert.AreEqual(n, index++);
            }

            Assert.AreEqual(numberOfItems,
                            CollectionHelper.Count(numbers));
        }

        [Test]
        public void SequenceEqualMethod()
        {
            int numberOfItems = this._RANDOM.Next(5000, 10000);

            IEnumerable<int> numbers1 = CollectionHelper.Range(0, numberOfItems);
            IEnumerable<int> numbers2 = CollectionHelper.Range(1, numberOfItems + 1);
            IEnumerable<int> numbers3 = CollectionHelper.Range(0, numberOfItems - 1);

            int[] number1Array = CollectionHelper.ToArray(numbers1);

            Assert.IsFalse(CollectionHelper.SequenceEqual(numbers1, numbers2));
            Assert.IsFalse(CollectionHelper.SequenceEqual(numbers2, numbers1));
            Assert.IsFalse(CollectionHelper.SequenceEqual(numbers1, numbers3));
            Assert.IsFalse(CollectionHelper.SequenceEqual(numbers2, numbers1));
            Assert.IsFalse(CollectionHelper.SequenceEqual(numbers2, numbers3));
            Assert.IsFalse(CollectionHelper.SequenceEqual(numbers3, numbers2));

            Assert.IsTrue(CollectionHelper.SequenceEqual(number1Array, number1Array));
            Assert.IsFalse(CollectionHelper.SequenceEqual(number1Array, numbers2));
            Assert.IsFalse(CollectionHelper.SequenceEqual(numbers2, number1Array));
            Assert.IsFalse(CollectionHelper.SequenceEqual(number1Array, numbers3));
            Assert.IsFalse(CollectionHelper.SequenceEqual(numbers2, number1Array));
        }

        /// <summary>
        /// Test <see cref="CollectionHelper.ToArray{T}(IEnumerable{T})" /> method.
        /// </summary>
        [Test]
        public void ToArrayMethod()
        {
            int numberOfItems = this._RANDOM.Next(5000, 10000);

            IEnumerable<int> numbers = CollectionHelper.Range(0, numberOfItems);

            int[] numberArray = CollectionHelper.ToArray(numbers);

            // unique instances
            Assert.AreNotSame(numbers,
                              numberArray);

            // same size?
            Assert.AreEqual(numberOfItems,
                            numberArray.Length);

            // check items
            using (IEnumerator<int> enumeratorLeft = numbers.GetEnumerator())
            {
                using (IEnumerator<int> enumeratorRight = ((IEnumerable<int>)numberArray).GetEnumerator())
                {
                    while (enumeratorLeft.MoveNext())
                    {
                        Assert.IsFalse((false == enumeratorRight.MoveNext()) ||
                                       (enumeratorLeft.Current != enumeratorRight.Current));
                    }

                    Assert.IsFalse(enumeratorRight.MoveNext());
                }
            }
        }

        #endregion Methods
    }
}