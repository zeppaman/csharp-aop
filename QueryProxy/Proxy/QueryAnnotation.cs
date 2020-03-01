using QueryProxy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace QueryProxy.Proxy
{
    public class QueryAttribute : AOPAttribute
    {
        public string Template { get; set; }
        public   QueryAttribute(string template)
        {
            this.Template = template;
        }

        public override object Execute(MethodInfo targetMethod, object[] args, object[] annotations)
        {
            return new List<Fruit>()
            {
                new Fruit(){  Name ="Lemon"},
                new Fruit(){  Name ="Orange"}
            };
        }
    }
}
