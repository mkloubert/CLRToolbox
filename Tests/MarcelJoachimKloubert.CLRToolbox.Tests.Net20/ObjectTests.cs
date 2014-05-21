using MarcelJoachimKloubert.CLRToolbox.Diagnostics.Tests;
using MarcelJoachimKloubert.CLRToolbox.Objects;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace MarcelJoachimKloubert.CLRToolbox.Tests
{
    [TestFixture]
    public partial class ObjectTests
    {
        #region Fields (3)

        private readonly Random _RANDOM = new Random();
        private readonly ObjectFactory _FACTORY;
        private readonly PropertyInfo[] _INTERFACE_TEST1_PROPERTIES;

        #endregion Fields

        #region Constructors (1)

        public ObjectTests()
        {
            this._FACTORY = new ObjectFactory();
            this._INTERFACE_TEST1_PROPERTIES = typeof(ITest1).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }

        #endregion Constructors

        #region Methods (6)

        [Test]
        public void ObjectFactory_CheckFactoryInstances()
        {
            Assert.AreNotSame(this._FACTORY,
                              ObjectFactory.Instance);

            Assert.AreSame(ObjectFactory.Instance,
                           ObjectFactory.Instance);
        }

        [Test]
        public void ObjectFactory_CheckObjectInstances()
        {
            ITest1 a1;
            ITest1 a2;
            ITest1 b;
            ITest1 c;
            ITest1 d1;
            ITest1 d2;
            this.CreateNewTest1Instances(out a1, out a2, out b, out c, out d1, out d2);

            // check 'a1' with others
            Assert.AreNotSame(a1, b);
            Assert.AreSame(a1, a1);
            Assert.AreSame(a1, a2);
            Assert.AreNotSame(a1, c);
            Assert.AreNotSame(a1, d1);
            Assert.AreNotSame(a1, d2);

            // check 'a2' with others
            Assert.AreNotSame(a2, b);
            Assert.AreSame(a2, a2);
            Assert.AreNotSame(a2, c);
            Assert.AreNotSame(a2, d1);
            Assert.AreNotSame(a2, d2);

            // check 'b' with others
            Assert.AreSame(b, b);
            Assert.AreNotSame(b, c);
            Assert.AreNotSame(b, d1);
            Assert.AreNotSame(b, d2);

            // check 'c' with others
            Assert.AreSame(c, c);
            Assert.AreNotSame(c, d1);
            Assert.AreNotSame(c, d2);

            // check 'd1' with others
            Assert.AreSame(d1, d1);
            Assert.AreSame(d1, d2);

            // check 'd2' with others
            Assert.AreSame(d2, d2);
        }

        [Test]
        public void ObjectFactory_CheckObjectProxyTypes()
        {
            ITest1 a1;
            ITest1 a2;
            ITest1 b;
            ITest1 c;
            ITest1 d1;
            ITest1 d2;
            this.CreateNewTest1Instances(out a1, out a2, out b, out c, out d1, out d2);

            // a1
            {
                Assert.AreEqual(a1.GetType().FullName,
                                a2.GetType().FullName);

                Assert.AreNotEqual(a1.GetType().FullName,
                                   b.GetType().FullName);

                Assert.AreEqual(a1.GetType().FullName,
                                c.GetType().FullName);

                Assert.AreNotEqual(a1.GetType().FullName,
                                   d1.GetType().FullName);

                Assert.AreNotEqual(a1.GetType().FullName,
                                   d2.GetType().FullName);
            }

            // a1
            {
                Assert.AreNotEqual(a2.GetType().FullName,
                                   b.GetType().FullName);

                Assert.AreEqual(a2.GetType().FullName,
                                c.GetType().FullName);

                Assert.AreNotEqual(a2.GetType().FullName,
                                   d1.GetType().FullName);

                Assert.AreNotEqual(a2.GetType().FullName,
                                   d2.GetType().FullName);
            }

            // b
            {
                Assert.AreNotEqual(b.GetType().FullName,
                                   c.GetType().FullName);

                Assert.AreEqual(b.GetType().FullName,
                                d1.GetType().FullName);

                Assert.AreEqual(b.GetType().FullName,
                                d2.GetType().FullName);
            }

            // c
            {
                Assert.AreNotEqual(c.GetType().FullName,
                                   d1.GetType().FullName);

                Assert.AreNotEqual(c.GetType().FullName,
                                   d2.GetType().FullName);
            }

            // d1 + d2
            {
                Assert.AreEqual(d1.GetType().FullName,
                                d2.GetType().FullName);
            }
        }

        [Test]
        public void ObjectFactory_CheckObjectProxyProperties()
        {
            ITest1 a1;
            ITest1 a2;
            ITest1 b;
            ITest1 c;
            ITest1 d1;
            ITest1 d2;
            List<ITest1> instances = this.CreateNewTest1Instances(out a1, out a2, out b, out c, out d1, out d2);

            foreach (ITest1 t in instances)
            {
                Type objType = t.GetType();
                PropertyInfo[] objProperties = objType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                // check if number of properties are equal
                Assert.AreEqual(objProperties.Length,
                                this._INTERFACE_TEST1_PROPERTIES.Length);

                // check if all properties exists in proxy type
                // and if property types are the same
                foreach (PropertyInfo property in this._INTERFACE_TEST1_PROPERTIES)
                {
                    PropertyInfo objProp = objType.GetProperty(property.Name,
                                                               BindingFlags.Public | BindingFlags.Instance);

                    Assert.IsNotNull(objProp);
                    Assert.AreEqual(property.PropertyType,
                                    objProp.PropertyType);
                }
            }
        }

        [Test]
        public void ObjectFactory_CheckObjectProxyPropertiesGettersAndSettes()
        {
            ITest1 a1;
            ITest1 a2;
            ITest1 b;
            ITest1 c;
            ITest1 d1;
            ITest1 d2;
            this.CreateNewTest1Instances(out a1, out a2, out b, out c, out d1, out d2);

            object valA1 = new object();
            object valA2 = new object();
            object valB = new object();
            object valC = new object();
            object valD1 = new object();
            object valD2 = new object();

            a1.A = valA1;
            Assert.AreSame(a1.A, valA1);
            Assert.AreSame(a2.A, valA1);

            a2.A = valA2;
            Assert.AreSame(a1.A, valA2);
            Assert.AreSame(a2.A, valA2);

            b.A = valB;
            Assert.AreSame(b.A, valB);

            c.A = valC;
            Assert.AreSame(c.A, valC);

            d1.A = valD1;
            Assert.AreSame(d1.A, valD1);
            Assert.AreSame(d2.A, valD1);

            d2.A = valD2;
            Assert.AreSame(d1.A, valD2);
            Assert.AreSame(d2.A, valD2);
        }

        private List<ITest1> CreateNewTest1Instances(out ITest1 a1, out ITest1 a2,
                                                     out ITest1 b,
                                                     out ITest1 c,
                                                     out ITest1 d1, out ITest1 d2)
        {
            a1 = this._FACTORY.CreateProxyForInterface<ITest1>();
            a2 = a1;
            b = ObjectFactory.Instance.CreateProxyForInterface<ITest1>();
            c = this._FACTORY.CreateProxyForInterface<ITest1>();
            d1 = ObjectFactory.Instance.CreateProxyForInterface<ITest1>();
            d2 = d1;

            return new List<ITest1>(new ITest1[] { a1, a2, b, c, d1, d2 });
        }

        #endregion Methods

        public interface ITest1
        {
            #region Data Members (1)

            object A { get; set; }

            #endregion Data Members
        }
    }
}