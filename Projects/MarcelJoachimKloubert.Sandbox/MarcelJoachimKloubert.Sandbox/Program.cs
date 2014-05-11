using MarcelJoachimKloubert.CLRToolbox.Execution.Workflows;
using MarcelJoachimKloubert.CLRToolbox.Objects;
using System.Collections.Generic;

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

    internal class Program
    {
        private static void Main(string[] args)
        {
            ObjectFactory of = ObjectFactory.Instance;

            var obj = of.CreateProxyForInterface<ITest>();

            dynamic dynObj = obj;
            dynObj.IchBinEinKunterbuntesProperty = true;
            dynObj.IchBinEinKunterbuntesProperty2 = "TM";

            if (obj != null)
            {
            }

            var workflow = DelegateWorkflow.Create(Station_A1);
            foreach (var action in workflow)
            {
                action();
            }

            // better for thread safe operations
            workflow.Execute();
        }

        private static void Station_A1(IWorkflowExecutionContext ctx)
        {
            ctx.Next = Station_B1;
        }

        private static void Station_B1(IWorkflowExecutionContext ctx)
        {
            ctx.Next = Station_C1;
        }

        private static void Station_C1(IWorkflowExecutionContext ctx)
        {
            var result = new Dictionary<string, object>();
        }
    }
}