using MarcelJoachimKloubert.CLRToolbox.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace MarcelJoachimKloubert.Sandbox
{
    public interface ITest
    {
        bool IchBinEinKunterbuntesProperty { get; }
        string IchBinEinKunterbuntesProperty2 { get; }
    }

    public class Test
    {
        public bool A { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ObjectFactory of = ObjectFactory.Instance;

            var obj = of.CreateProxyForInterface<ITest>();

            dynamic dynObj = obj;
            dynObj.IchBinEinKunterbuntesProperty = true;
            dynObj.IchBinEinKunterbuntesProperty2 = "TM";

            if (obj != null)
            {

            }
        }
    }
}
