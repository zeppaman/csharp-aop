using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Linq;

namespace QueryProxy.Proxy
{
    public class ProxyFacotory<T> : DispatchProxy
    {
        private T _decorated;
        private T _interface;

        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            var interfaceMethods = _decorated.GetType().GetMethods();
            foreach (var method in interfaceMethods)
            {
                Debug.WriteLine("method" + method.Name);
            }

            var annotations = targetMethod.GetCustomAttributes(true);
            var aopAttr = annotations.FirstOrDefault(x => typeof(AOPAttribute).IsAssignableFrom(x.GetType())) as AOPAttribute;

            if (aopAttr != null)
            {
                return aopAttr.Execute(targetMethod, args, annotations);
            }

            var result = targetMethod.Invoke(_decorated, args);
            return result;
        }

        public static T Create<T>() where T : class
        {
            object proxy = Create<T, ProxyFacotory<T>>();
            ((ProxyFacotory<T>)proxy).SetParameters(proxy as T);

            return (T)proxy;
        }

        private void SetParameters(T decorated)
        {
            _decorated = decorated;
        }
    }
}
