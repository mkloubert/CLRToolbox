using MarcelJoachimKloubert.CLRToolbox;
using MarcelJoachimKloubert.CLRToolbox.ComponentModel;
using MarcelJoachimKloubert.CLRToolbox.Data.Xml;
using MarcelJoachimKloubert.CLRToolbox.Execution.Workflows;
using MarcelJoachimKloubert.CLRToolbox.Execution.Workflows.Impl;
using MarcelJoachimKloubert.CLRToolbox.Extensions;
using MarcelJoachimKloubert.CLRToolbox.Objects;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;

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

    internal class Test3 : NotificationObjectBase
    {
        public int A
        {
            get { return this.Get<int>(); }

            set { this.Set<int>(value); }
        }

        [ReceiveNotificationFrom("A")]
        public long B
        {
            get { return this.A * 2; }
        }

        [ReceiveValueFrom("A")]
        public void ReceiveNewValueFromA_1()
        {
        }

        [ReceiveValueFrom("A")]
        public void ReceiveNewValueFromA_2(int newValue)
        {
        }

        [ReceiveValueFrom("A")]
        private void ReceiveNewValueFromA_3(int newValue, object oldValue)
        {
        }

        [ReceiveValueFrom("A", ReceiveValueFromOptions.IfEqual)]
        public static void ReceiveNewValueFromA_4(int newValue, long oldValue, IEnumerable<char> senderName)
        {
        }

        [ReceiveValueFrom("A")]
        private static void ReceiveNewValueFromA_5(int newValue, int oldValue, string senderName, NotificationObjectBase obj)
        {
        }

        [ReceiveValueFrom("A", ReceiveValueFromOptions.IfEqual | ReceiveValueFromOptions.IfDifferent)]
        protected static void ReceiveNewValueFromA_6(int newValue, int oldValue, string senderName, Test3 obj, MemberTypes type)
        {
        }

        private long _a2;

        [ReceiveValueFrom("A")]
        protected long A2
        {
            get { return this._a2; }

            set
            {
                this._a2 = value;
            }
        }

        [ReceiveValueFrom("A")]
        protected double _a3;
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
            var xml = XmlDocumentFactory.Parse(@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes""?>
<verzeichnis x:a=""12"" xmlns:x=""http://www.w3.org/1999/xhtml"">
     <titel>Wikipedia Städteverzeichnis</titel>
     <eintrag>
          <stichwort>Genf</stichwort>
          <eintragstext>Genf ist der Sitz von ...</eintragstext>
     </eintrag>
     <eintrag>
          <stichwort>Köln</stichwort>
          <eintragstext>Köln ist eine Stadt, die ...</eintragstext>
     </eintrag>
</verzeichnis>");

            var xml2 = new XmlDocument();
            xml2.LoadXml(xml.ToString());

            foreach (var e in xml.Elements())
            {
                foreach (var a in e.Attributes())
                {

                }

                foreach (var n2 in e)
                {

                }
            }

            var r = new CryptoRandom();
            var rv = r.Next(-1000, 1000);

            ObjectFactory of = ObjectFactory.Instance;

            var obj = of.CreateProxyForInterface<ITest>();

            dynamic dynObj = obj;
            dynObj.IchBinEinKunterbuntesProperty = true;
            dynObj.IchBinEinKunterbuntesProperty2 = "TM";

            if (obj != null)
            {
            }

            var workflow = DelegateWorkflow.Create(Station_A1);

            var t2 = new Test2();

            var t3 = new Test3();
            t3.A = 5979;
            t3.A = 5979;
            t3.A = 23979;

            var workflow2 = AttributeWorkflow.Create(t2);

            var aggWF = new AggregateWorkflow();
            aggWF.Add(workflow);
            aggWF.Add(workflow2);

            aggWF.Execute();
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