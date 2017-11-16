using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCSharp.MultiLevelInheritance;
using System.Threading.Tasks;
using TestCSharp.OptionalParameters;
using TestCSharp.ConstructorObjectInitializer;
using System.Net.Http;

namespace TestCSharp
{
    class Program
    {
        private List<int> list;

        static void Main(string[] args)
        {
            #region OptionalParameters
            //OptionalVsOverload ov = new OptionalVsOverload();
            //ov.M1(1, 'a');
            #endregion

            #region MultiLevelInheritance
            //A a = new A();
            //a.M1();
            //a.M2();
            //B b = new B();
            //b.M1();
            //b.M2();
            //C c = new C();
            //c.M1();
            //c.M2();
            //D d = new D();
            //d.M1();
            //d.M2();

            //a = (A)b;
            //a.M1();
            //a.M2();
            //a = (A)c;
            //a.M1();
            //a.M2();
            //a = (A)d;
            //a.M1();
            //a.M2(); 
            #endregion

            #region StaticDirective and ref
            //TestStaticDirective s = new TestStaticDirective();
            //s.MM();

            //StaticDirective.A a = new StaticDirective.A()
            //{
            //    Name = "ABC",
            //    Number = 2
            //};
            //StaticDirective.A.m(a);
            //Console.WriteLine("Name: " + a.Name); 
            #endregion

            #region ConstructorObjectInitializer
            //ConstructorObjectInitializer.A a = new ConstructorObjectInitializer.A("Suneel") { Age = 5, Name = "Sachin" };
            //Console.WriteLine(DateTime.Now.Millisecond);
            //Console.WriteLine(a.Name); 
            #endregion

            #region AsyncMain
            //string asyncResult = Task.Run(myMethod).GetAwaiter().GetResult();
            //Console.WriteLine(asyncResult);
            #endregion




            Console.ReadLine();
        }


        #region AsyncMethod
        static async Task<string> myMethod()
        {
            var http = new HttpClient();
            return await http.GetStringAsync("https://github.com/SuneelPodapati");
        }
        #endregion
    }
}
