using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace MarcelJoachimKloubert.CLRToolbox.Tests
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            List<Type> allTypes = new List<Type>(Assembly.GetExecutingAssembly().GetTypes());
            allTypes.Sort(delegate(Type x, Type y)
                {
                    string strX = null;
                    if (x != null)
                    {
                        strX = x.Name.ToLower().Trim();
                    }

                    string strY = null;
                    if (y != null)
                    {
                        strY = y.Name.ToLower().Trim();
                    }

                    return string.Compare(strX, strY);
                });

            foreach (Type type in allTypes)
            {
                object[] testFixureAttribs = type.GetCustomAttributes(typeof(TestFixtureAttribute), true);
                if (testFixureAttribs.Length < 1)
                {
                    continue;
                }

                object obj = Activator.CreateInstance(type);
                Console.WriteLine("{0} ...", obj.GetType().Name);

                List<MethodInfo> allMethods = new List<MethodInfo>(obj.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic));
                allMethods.Sort(delegate(MethodInfo x, MethodInfo y)
                    {
                        string strX = null;
                        if (x != null)
                        {
                            strX = x.Name.ToLower().Trim();
                        }

                        string strY = null;
                        if (y != null)
                        {
                            strY = y.Name.ToLower().Trim();
                        }

                        return string.Compare(strX, strY);
                    });

                foreach (MethodInfo method in allMethods)
                {
                    object[] testAttribs = method.GetCustomAttributes(typeof(TestAttribute), true);
                    if (testAttribs.Length < 1)
                    {
                        continue;
                    }

                    try
                    {
                        Console.Write("\t{0} ... ", method.Name);

                        method.Invoke(obj, null);
                        Console.WriteLine("[OK]");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("[ERROR: {0}]",
                                          (ex.GetBaseException() ?? ex).Message);
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("===== ENTER =====");

            Console.ReadLine();
        }
    }
}