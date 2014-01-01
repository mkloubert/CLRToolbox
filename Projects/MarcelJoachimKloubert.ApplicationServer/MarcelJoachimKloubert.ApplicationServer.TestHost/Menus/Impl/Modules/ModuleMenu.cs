using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Net;
using System.Text;
using MarcelJoachimKloubert.CLRToolbox.Extensions;
using MarcelJoachimKloubert.CLRToolbox.IO;
using MarcelJoachimKloubert.CLRToolbox.Serialization;
using MarcelJoachimKloubert.CLRToolbox.Serialization.Json;
using MarcelJoachimKloubert.CLRToolbox.ServiceLocation;

namespace MarcelJoachimKloubert.ApplicationServer.TestHost.Menus.Impl.Modules
{
    class ModuleMenu : MenuHandlerBase
    {
        #region Methods (3)

        // Public Methods (1) 

        public override void DrawMenu()
        {
            GlobalConsole.Current.WriteLine("[1] DocDB");
            GlobalConsole.Current.WriteLine("[2] RemoteComm");
            GlobalConsole.Current.WriteLine();
            GlobalConsole.Current.WriteLine("[x] Back");
        }
        // Protected Methods (1) 

        protected override void OnHandleInput(string input, ref IMenuHandler nextHandler, ref bool isValid)
        {
            switch (input)
            {
                case "1":
                    this.ExecuteAnWaitOnError((state) => state.Test_DocDB(),
                                              this);
                    break;

                case "2":
                    this.ExecuteAnWaitOnError((state) => state.Test_RemoteComm(),
                                              this);
                    break;

                case "x":
                    nextHandler = new RootMenu();
                    break;

                default:
                    isValid = false;
                    break;
            }
        }
        // Private Methods (1) 

        private void Test_RemoteComm()
        {
            var serializer = ServiceLocator.Current.GetInstance<ISerializer>();

            var request = (HttpWebRequest)HttpWebRequest.Create("https://localhost:23979/exec/0F4EC862D33746E1BB6DA54734DF0D7B");
            request.SetBasicAuth("test", "test");
            request.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) =>
                {
                    return true;
                };

            request.Method = "POST";

            using (var reqStream = request.GetRequestStream())
            {
                var @params = new Dictionary<string, object>();
                @params["a"] = "Hallo, Echo!";

                var json = serializer.ToJson(@params);
                var jsonBlob = Encoding.UTF8.GetBytes(json);

                reqStream.Write(jsonBlob, 0, jsonBlob.Length);
                reqStream.Close();
            }

            var response = request.GetResponse();
            using (var respStream = response.GetResponseStream())
            {
                string jsonResult;
                using (var temp = new MemoryStream())
                {
                    respStream.CopyTo(temp);

                    jsonResult = Encoding.UTF8.GetString(temp.ToArray());
                }

                var jsonResultObj = serializer.FromJson(jsonResult);

                var parameterList = (IList<object>)jsonResultObj["params"];
                foreach (IDictionary<string, object> p in parameterList)
                {

                }
            }
        }

        private void Test_DocDB()
        {
            GlobalConsole.Current.WriteLine("Username: ");
            GlobalConsole.Current.Write("> ");
            var user = GlobalConsole.Current.ReadLine();
            if (string.IsNullOrWhiteSpace(user))
            {
                return;
            }

            GlobalConsole.Current.WriteLine("Password: ");
            GlobalConsole.Current.Write("> ");
            var pwd = GlobalConsole.Current.ReadPassword();
            if (pwd.Length < 1)
            {
                return;
            }

            var serializer = ServiceLocator.Current.GetInstance<ISerializer>();

            var request = (HttpWebRequest)HttpWebRequest.Create("https://localhost:1781/a/b/c");
            request.SetBasicAuth(user, pwd);

            request.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) =>
                {
                    return true;
                };

            request.Method = "GET";
            request.ContentType = "application/json";
            var response = request.GetResponse();
            using (var respStream = response.GetResponseStream())
            {
                string jsonResult;
                using (var temp = new MemoryStream())
                {
                    respStream.CopyTo(temp);

                    jsonResult = Encoding.UTF8.GetString(temp.ToArray());
                }

                var jsonResultObj = serializer.FromJson<JsonParameterResult>(jsonResult);
            }

            //using (var reqStream = request.GetRequestStream())
            //{
            //    var serializer = ServiceLocator.Current.GetInstance<ISerializer>();

            //    var json = serializer.ToJson(new Test()) ?? string.Empty;

            //    var data = Encoding.UTF8.GetBytes(json);
            //    reqStream.Write(data, 0, data.Length);

            //    reqStream.Flush();
            //    reqStream.Close();

            //    var response = request.GetResponse();
            //    using (var respStream = response.GetResponseStream())
            //    {
            //        string jsonResult;
            //        using (var temp = new MemoryStream())
            //        {
            //            respStream.CopyTo(temp);

            //            jsonResult = Encoding.UTF8.GetString(temp.ToArray());
            //        }

            //        var jsonResultObj = serializer.FromJson<IDictionary<string, object>>(jsonResult);
            //    }
            //}
        }

        #endregion Methods

        #region Nested Classes (1)

        private sealed class Test
        {
            #region Fields (1)

            public int a = 1000;

            #endregion Fields
        }

        #endregion Nested Classes
    }
}
