using MarcelJoachimKloubert.CLRToolbox.Objects;
using NUnit.Framework;
using System;
using System.Reflection;

namespace MarcelJoachimKloubert.CLRToolbox.Tests
{
    [TestFixture]
    public class ObjectTests
    {
        private readonly Random _RANDOM = new Random();

        public interface ITest1
        {
            int A { get; set; }
        }

        #region Methods

        [Test]
        public void ObjectFactoryTest1()
        {
            PropertyInfo[] test1InterfaceProperties = typeof(ITest1).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            ObjectFactory factory = new ObjectFactory();
            ObjectFactory defFactory = ObjectFactory.Instance;

            ITest1 a = factory.CreateProxyForInterface<ITest1>();
            ITest1 b = defFactory.CreateProxyForInterface<ITest1>();
            ITest1 c = factory.CreateProxyForInterface<ITest1>();
            ITest1 d = defFactory.CreateProxyForInterface<ITest1>();

            ITest1[] instances = new ITest1[] { a, b, c, d };

            foreach (ITest1 t in instances)
            {
                Type objType = t.GetType();
                PropertyInfo[] objProperties = objType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                // check if number of properties are equal
                Assert.AreEqual(objProperties.Length,
                                test1InterfaceProperties.Length);

                // check if all properties exists in propxy type
                // and if types are the same
                foreach (PropertyInfo property in test1InterfaceProperties)
                {
                    PropertyInfo objProp = objType.GetProperty(property.Name,
                                                               BindingFlags.Public | BindingFlags.Instance);

                    Assert.IsNotNull(objProp);
                    Assert.AreEqual(property.PropertyType,
                                    objProp.PropertyType);
                }
            }

            Assert.AreNotEqual(a.GetType().Name,
                               b.GetType().Name);

            Assert.AreEqual(a.GetType().Name,
                            c.GetType().Name);

            Assert.AreNotEqual(b.GetType().Name,
                               c.GetType().Name);
            
            Assert.AreEqual(b.GetType().Name,
                            d.GetType().Name);

            Assert.AreNotSame(a, b);
            Assert.AreSame(a, a);
            Assert.AreNotSame(a, c);
            Assert.AreNotSame(a, d);
            
            Assert.AreNotSame(b, a);
            Assert.AreSame(b, b);
            Assert.AreNotSame(b, c);
            Assert.AreNotSame(b, d);
            
            Assert.AreNotSame(c, a);
            Assert.AreSame(c, c);
            Assert.AreNotSame(c, b);
            Assert.AreNotSame(c, d);
            
            Assert.AreNotSame(d, a);
            Assert.AreNotSame(d, b);
            Assert.AreNotSame(d, c);
            Assert.AreSame(d, d);

            int valA = _RANDOM.Next();
            int valB = _RANDOM.Next();
            int valC = _RANDOM.Next();

            a.A = valA;
            Assert.AreEqual(a.A, valA);

            b.A = valB;
            Assert.AreEqual(b.A, valB);

            c.A = valC;
            Assert.AreEqual(c.A, valC);
        }

        #endregion Methods
    }
}