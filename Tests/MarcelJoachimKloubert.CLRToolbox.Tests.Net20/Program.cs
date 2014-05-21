using MarcelJoachimKloubert.CLRToolbox.Diagnostics.Tests;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace MarcelJoachimKloubert.CLRToolbox.Tests
{
    internal static class Program
    {
        #region Fields (1)

        private static object _SYNC = new object();

        #endregion Fields

        #region Methods (4)

        private static void InvokeConsole(Action action)
        {
            InvokeConsole(action, null);
        }

        private static void InvokeConsole(Action action, ConsoleColor? foreColor)
        {
            InvokeConsole(action, foreColor, null);
        }

        private static void InvokeConsole(Action action, ConsoleColor? foreColor, ConsoleColor? bgColor)
        {
            lock (_SYNC)
            {
                ConsoleColor oldForeColor = Console.ForegroundColor;
                ConsoleColor oldBgColor = Console.BackgroundColor;

                try
                {
                    if (foreColor.HasValue)
                    {
                        Console.ForegroundColor = foreColor.Value;
                    }

                    if (bgColor.HasValue)
                    {
                        Console.BackgroundColor = bgColor.Value;
                    }

                    action();
                }
                finally
                {
                    Console.ForegroundColor = oldForeColor;
                    Console.BackgroundColor = oldBgColor;
                }
            }
        }

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
                object[] testFixureAttribs = type.GetCustomAttributes(typeof(global::MarcelJoachimKloubert.CLRToolbox.Diagnostics.Tests.TestFixtureAttribute), true);
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
                    object[] testAttribs = method.GetCustomAttributes(typeof(global::MarcelJoachimKloubert.CLRToolbox.Diagnostics.Tests.TestAttribute), true);
                    if (testAttribs.Length < 1)
                    {
                        continue;
                    }

                    try
                    {
                        TestAttribute ta = (TestAttribute)testAttribs[0];

                        Console.Write("\t'{0}' ... ", method.Name);

                        method.Invoke(obj, null);

                        InvokeConsole(delegate()
                                      {
                                          Console.WriteLine("[OK]");
                                      }, ConsoleColor.Green
                                       , ConsoleColor.Black);
                    }
                    catch (Exception ex)
                    {
                        InvokeConsole(delegate()
                                      {
                                          Console.WriteLine("[ERROR: {0}]",
                                                            (ex.GetBaseException() ?? ex).Message);
                                      }, ConsoleColor.Red
                                       , ConsoleColor.Black);
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("===== ENTER =====");

            Console.ReadLine();
        }

        #endregion Methods
    }
}