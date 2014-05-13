using MarcelJoachimKloubert.CLRToolbox.Execution.Workflows;
using MarcelJoachimKloubert.CLRToolbox.Objects;
using System;
using System.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Extensions;
using System.Linq;

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

        public static explicit operator string(Test t)
        {
            return t == null ? null : t.ToString();
        }
    }

    public class Test2Parent
    {
        public virtual void Step4(IWorkflowExecutionContext ctx)
        {

        }
    }

    public class Test2 : Test2Parent
    {
        [WorkflowStart]
        [WorkflowStart("wurst")]
        [NextWorkflowStep("Step2")]
        [NextWorkflowStep("Step1", "Wurst")]
        public void Start()
        {

        }

        [NextWorkflowStep("Step3")]
        [NextWorkflowStep("Step2", "Wurst")]
        public void Step1()
        {

        }

        [NextWorkflowStep("Step3")]
        public void Step2(IWorkflowExecutionContext ctx)
        {
            ctx.Next = this.Step4;
        }

        public void Step3(IWorkflowExecutionContext ctx)
        {

        }

        [NextWorkflowStep("Step5")]
        public override void Step4(IWorkflowExecutionContext ctx)
        {

        }

        public void Step5()
        {

        }
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
                action(new object[] { "TM", "MK" });
            }

            var t2 = new Test2();

            var workflow2 = new AttributeWorkflow();
            workflow2.ExecuteFor(t2, "  WUrSt ");

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

            foreach (var op in typeof(Test).GetExplicitOperators())
            {

            }
        }
    }
}