using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static TestCSharp.StaticDirective.A;
using static TestCSharp.StaticDirective.B;


namespace TestCSharp
{
    namespace OptionalParameters
    {
        public class OptionalVsOverload
        {
            public void M1(int a, char b, string c = "Optional") // I am not that important if I have a matching overload
            {
                Console.WriteLine("M1 Optional");
            }
            public void M1(int a, char b)  // I have the priority since I am an absolute match
            {
                Console.WriteLine("M1 Overload");
            }


        }

        struct TestStaticDirective
        {
            public void MM()
            {
                // A.m();  // Fully Qualified name is needed
                TestStaticDirective? v;
                v = null;
                unsafe
                {
                    Console.WriteLine(sizeof(TestStaticDirective));
                }
            }
        }

    }

    namespace MultiLevelInheritance
    {
        public class A
        {
            public virtual void M1()
            {
                Console.WriteLine("I am A in : " + this + " : virtual");
            }
            public void M2()
            {
                Console.WriteLine("I am A in : " + this + " : concrete");
            }

            // AM1, AM2
        }

        public class B : A
        {
            public override void M1()
            {
                Console.WriteLine("I am B in : " + this + " : Override");
            }
            public new void M2()
            {
                Console.WriteLine("I am B in : " + this + " : new");
            }

            // BM1, AM2, BM2
        }

        public class C : B
        {
            public new void M1()
            {
                Console.WriteLine("I am C in : " + this + " : new");
            }
            public new virtual void M2()
            {
                Console.WriteLine("I am C in : " + this + " : new virtual");
            }

            // BM1, CM1, AM2, BM2, CM2
        }

        public class D : C
        {
            public new void M1()
            {
                Console.WriteLine("I am D in : " + this + " : Override");
            }
            public override void M2()
            {
                Console.WriteLine("I am D in : " + this + " : Override");
            }

            // BM1, CM1, DM1, AM2, BM2, DM2
        }
    }

    namespace StaticDirective
    {
        public class A
        {
            public int Number { get; set; }
            public string Name { get; set; }
            public static void m(A a)
            {
                a.Name = "Suneel";
                //a = new A()
                //{
                //    Name = "Suneel",  // Testing the ref for reference types
                //    Number = 1
                //};
            }
        }

        public class B
        {
            public static void m()
            {

            }
        }

    }

    namespace ConstructorObjectInitializer
    {
        public class A
        {
            public int Age { get; set; }
            public string Name { get; set; }

            public A(string name)
            {
                Name = name;
                Console.WriteLine(DateTime.Now.Millisecond);
                Console.WriteLine(Name);

            }
        }

    }

    namespace ThreadingWithLock
    {
        public class Account
        {
            private Object thisLock = new Object();
            int balance;

            Random r = new Random();

            public Account(int initial)
            {
                balance = initial;
            }

            int Withdraw(int amount)
            {

                // This condition never is true unless the lock statement
                // is commented out.
                if (balance < 0)
                {
                    throw new Exception("Negative Balance");
                }

                // Comment out the next line to see the effect of leaving out 
                // the lock keyword.
                lock (thisLock)
                {
                    if (balance >= amount)
                    {
                        Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
                        Console.WriteLine("Balance before Withdrawal :  " + balance);
                        Console.WriteLine("Amount to Withdraw        : -" + amount);
                        balance = balance - amount;
                        Console.WriteLine("Balance after Withdrawal  :  " + balance);
                        Console.WriteLine();
                        return amount;
                    }
                    else
                    {
                        return 0; // transaction rejected
                    }
                }
            }

            public void DoTransactions()
            {
                for (int i = 0; i < 50; i++)
                {
                    Withdraw(r.Next(1, 100));
                }
            }
        }
    }
}
