// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using DotNetWikiBot;
using MarcelJoachimKloubert.ClrDocToMediaWiki.Classes;
using MarcelJoachimKloubert.CLRToolbox.Configuration.Impl;
using System;
using System.IO;
using System.Linq;

namespace MarcelJoachimKloubert.ClrDocToMediaWiki
{
    internal static class Program
    {
        private static int Main(string[] args)
        {
            try
            {
                var configFile = new FileInfo(Path.Combine(Environment.CurrentDirectory,
                                                           "config.ini"));

                var ini = new IniFileConfigRepository(configFile);

                foreach (var task in ini.GetCategoryNames())
                {
                    try
                    {
                        var asmFile = new FileInfo(ini.GetValue<string>("assembly", task));
                        var siteUrl = new Uri(ini.GetValue<string>("site", task));

                        var user = ini.GetValue<string>("user", task).Trim();
                        var pwd = ini.GetValue<string>("password", task);

                        string @namespace;
                        ini.TryGetValue<string>("namespace", out @namespace, task);

                        if (string.IsNullOrWhiteSpace(@namespace))
                        {
                            @namespace = null;
                        }
                        else
                        {
                            @namespace = @namespace.Trim();
                        }

                        if (@namespace != null)
                        {
                            while (@namespace.EndsWith("/"))
                            {
                                @namespace = @namespace.Substring(0, @namespace.Length - 1).Trim();
                            }
                        }

                        if (string.IsNullOrWhiteSpace(@namespace))
                        {
                            @namespace = null;
                        }
                        else
                        {
                            @namespace = @namespace.Trim();
                        }

                        // CLRToolbox:Sandbox
                        var site = new Site(siteUrl.ToString(),
                                            user, pwd);

                        var doc = AssemblyDocumentation.FromFile(asmFile);

                        var types = doc.GetTypes().ToArray();
                        foreach (var t in types)
                        {
                            var test = t.ClrType.IsPublic;
                            
                            var constructors = t.GetConstructors().ToArray();
                            var events = t.GetEvents().ToArray();
                            var fields = t.GetFields().ToArray();
                            var methods = t.GetMethods().ToArray();
                            var properties = t.GetProperties().ToArray();

                            var m2 = methods.Where(m => m.GetParameters().Any()).ToArray();
                            var p2 = properties.Where(p => p.GetIndexParameters().Any()).ToArray();
                        }

                        var page = new Page(site, "CLRToolbox:Sandbox");
                        page.LoadWithMetadata();
                        page.ResolveRedirect();
                        

                    }
                    catch
                    {
                    }
                }

                return 0;
            }
            catch
            {
                return 1;
            }
        }
    }
}