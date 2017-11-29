using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCSharp.MultiLevelInheritance;
using System.Threading.Tasks;
using TestCSharp.OptionalParameters;
using TestCSharp.ConstructorObjectInitializer;
using System.Net.Http;
using System.Threading;
using TestCSharp.ThreadingWithLock;
using TestCSharp.EnumFlags;
using TestCSharp.DelegatesAsEventsAndHandlers;
using TestCSharp.ForEachEffectedNoIEnumerable;

namespace TestCSharp
{
    public class Program
    {
        private static List<int> list;

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

            #region BoxingUnBoxing
            //short i = 1;
            //object o = i;   // Boxing stores the info about the boxed value type in the object
            //try
            //{
            //    int a = (int)i;  // Direct casting is possible through the value types default implicit conversion
            //    Console.WriteLine(a);
            //    int s = (int)o;  // Not possible as Unboxing type should also match the boxed type even if there is a default implicit conversion between boxed and unboxed types
            //    Console.WriteLine(s);
            //    int ss = (short)o; // Possible as Unboxing is successful and then there is a default implicit conversion between short and int
            //    Console.WriteLine(ss);
            //}
            //catch (Exception exp)
            //{
            //    Console.WriteLine(exp.Message);
            //}
            #endregion

            #region ThreadingWithLock
            //Thread[] threads = new Thread[10];
            //Account acc = new Account(10000);
            //for (int i = 0; i < 10; i++)
            //{
            //    Thread t = new Thread(new ThreadStart(acc.DoTransactions));
            //    threads[i] = t;
            //}
            //for (int i = 0; i < 10; i++)
            //{
            //    threads[i].Start();
            //}

            ////block main thread until all other threads have ran to completion.
            //foreach (var t in threads)
            //    t.Join();
            #endregion

            #region AutoResetEvent
            //autoEvent = new AutoResetEvent(false);

            //Console.WriteLine("main thread starting worker thread...");
            //Thread t = new Thread(DoWork);
            //t.Start();

            //Console.WriteLine("main thread sleeping for 1 second...");
            //var v = Console.ReadLine();
            //Thread.Sleep(1000);

            //Console.WriteLine("main thread signaling worker thread...");
            //autoEvent.Set(); 
            #endregion

            #region CheckedUnchecked
            //int maxIntValue = int.MaxValue;
            //var v = 0;

            //unchecked
            //{
            //    checked
            //    {
            //        v = maxIntValue + 10;
            //    }
            //} 
            #endregion

            #region EnumFlags
            //Console.WriteLine(default(CarOptions));
            //CarOptions c = (CarOptions)7; // Does not generates any error.
            //Console.WriteLine(c); // returns the result same as | operated if possible, or else the simple int
            //CarOptions options = CarOptions.SunRoof | CarOptions.FogLights;
            //Console.WriteLine(options); // Use the Flags attribute to display the name of each enum element that has the value 1
            #endregion

            #region DelegatesAsEventsAndHandlers
            //Vehicle v = new Vehicle();
            //Thread t = new Thread(v.Accelerate);
            //t.Start();
            //v.Punctured += (s, e) => Console.WriteLine($"Punctured at {e.InitialSpeed} kmph. with {e.TyresLeft} tyres left.");
            //v.Punctured += (s, e) => Console.WriteLine($"Vehicle has Punctured at {e.Temparature} degree temparature. So the tyre " + (e.Temparature > 60 ? "cannot" : "can") + " be reused");
            #endregion

            #region ForEachEffectedNoIEnumerable
            //var names = new String[] { "Suneel", "Sachin", "Sourav", "Ray", "Vale", "Franko", "Nicky" };
            //foreach (var item in names)
            //{
            //    Console.WriteLine(item);
            //    names[3] = "Rossi";  // Changes the enumerating list thta is being cached
            //    names = names.Reverse().ToArray(); // Effects the array reference, but the cached array does not have any effect so the iteration will have no effect
            //}
            //names.ToList().ForEach(x => Console.WriteLine(x)); 




            //var people = new People();  // foreach only needs the members not the interfaces
            //foreach (string person in people)  // Removing the IEnumerable implementation makes the foreach source to be strongly typed.
            //{
            //    Console.WriteLine(person);
            //}
            #endregion



            Console.ReadLine();
        }

        #region ConstVsReadOnly
        //public readonly int readOnly = 1;
        //public const int constant = 1 + 2; // Expressions that can give a constant value can be used 
        //public Program()
        //{
        //    readOnly = 10;
        //    // constant = 20; // Only one assignment and declaration for const at the 
        //    readOnly = 20;
        //} 
        #endregion

        #region AsyncMethod
        static async Task<string> myMethod()
        {
            var http = new HttpClient();
            return await http.GetStringAsync("https://github.com/SuneelPodapati");
        }
        #endregion

        #region AutoResetEvent
        //static AutoResetEvent autoEvent;

        //static void DoWork()
        //{
        //    Console.WriteLine("   worker thread started, now waiting on event...");
        //    autoEvent.WaitOne();
        //    Console.WriteLine("   worker thread reactivated, now exiting...");
        //} 
        #endregion
    }
}
