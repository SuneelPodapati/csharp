using System;
using System.Collections;
using System.Threading;


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
            private int a;
            private char b;

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

    namespace EnumFlags
    {
        [Flags]  // Helps for logical operations using the enums. Try to use one bit per enum like 0, 1, 2, 4, 8, 16..
        public enum CarOptions : int
        {
            SunRoof = 0x01,
            Spoiler = 0x02,
            FogLights = 0x04,
            TintedWindows = 0x08,
        }
    }

    namespace DelegatesAsEventsAndHandlers
    {
        public class Delegates
        {
            public Action Action { get; set; }
            public Action<int> Action2 { get; set; }
            public Func<int> Func { get; set; }
            public Func<int, int> Func2 { get; set; }
            public Func<int, int, int> Func3 { get; set; }
            public Predicate<int> Predicate { get; set; }
            public EventHandler<int> EventHandler { get; set; }
            
        }
        public class Vehicle
        {
            private float tyreTemparature;

            public float TyreTemparature
            {
                get { return tyreTemparature; }
                private set
                {
                    if (tyreTemparature + value < 60)
                    {
                        tyreTemparature += value;
                    }
                    else
                    {
                        tyreTemparature = tyreTemparature + value;
                        Puncture(new PuncturedArgs() { InitialSpeed = Speed, FinalSpeed = 0, Temparature = TyreTemparature, TyresLeft = Tyres });
                    }
                }
            }

            public int Tyres { get; private set; } = 4;
            public float Speed { get; private set; } = 0;

            public PuncturedHandler Punctured;
            //public event PuncturedHandler Punctured;  // So event is a access modifier on the EventHandler delegate reference
            public delegate void PuncturedHandler(object s, PuncturedArgs e);

            public void Accelerate()
            {
                while (Tyres == 4)
                {
                    Speed += 10;
                    TyreTemparature += (Speed/10) + 3.14f - (Speed/4);
                    Console.WriteLine($"Speed: {Speed} and Temparature: {TyreTemparature}");
                    Thread.Sleep(1000);
                }
            }

            private void Puncture( PuncturedArgs e)
            {
                Tyres--;
                Punctured?.Invoke(this, e);
            }

        }
        public class PuncturedArgs
        {
            public float InitialSpeed { get; set; }
            public float FinalSpeed { get; set; }
            public int TyresLeft { get; set; }
            public float Temparature { get; set; }

        }
    }

    namespace ForEachEffectedNoIEnumerable
    {
        public class People
        {
            private string[] persons = new String[] { "Suneel", "Sachin", "Sourav", "Ray", "Vale", "Franko", "Nicky" };
            
            public PeopleEnumerator GetEnumerator()  // IEnumerable is not needed for foreach
            {
                return new PeopleEnumerator(this);
            }
            public class PeopleEnumerator           // Enumerator need not implement IEnumerator
            {
                private int position = -1;
                private People t;
                
                public PeopleEnumerator(People t)
                {
                    this.t = t;
                }
                public bool MoveNext()
                {
                    if (position < t.persons.Length - 1)
                    {
                        position++;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                public void Reset()
                {
                    position = -1;
                }
                public string Current  // The return type of Current need not be osbject but can be specific by removing IEnumerator
                {
                    get
                    {
                        return t.persons[position];
                    }
                }  
            }
        }
    }

    namespace FrameworkEvents
    {
        public class Button
        {
            public int PointerLocation { get; set; } = 0;
            public event EventHandler<MouseOverEventArgs> MouseOver;
            
            public void MoveCursor()
            {
                while (PointerLocation != 100)
                {
                    Console.WriteLine($"Moving Cursor From {PointerLocation} to {PointerLocation + 1}");
                    PointerLocation++;
                    if (PointerLocation > 50)
                    {
                        FireEvent(new MouseOverEventArgs { Location = this.PointerLocation, Position = -1 });
                        return;
                    }
                }
            }
            protected virtual void FireEvent(MouseOverEventArgs args)
            {
                MouseOver?.Invoke(this, args);
            }
        }

        public class MouseOverEventArgs : EventArgs
        {
            public int Position { get; set; }
            public int Location { get; set; }

        }

        public class UI
        {
            private Button button;
            public  void RenderUI()
            {
                button = new Button();
                Console.WriteLine("Subscribing to MouseOver");
                button.MouseOver += Button_MouseOver;
                button.MouseOver += Button_MouseOver1;
                button.MouseOver += (s, e) =>
                Console.WriteLine($"Handler 3: Mouse is Over the element at {e.Location} on {Thread.CurrentThread.ManagedThreadId}");
                button.MoveCursor();
            }

            private void Button_MouseOver(object s, MouseOverEventArgs e)
            {
                Console.WriteLine($"Handler 1: Mouse is Over the element at {e.Location} on {Thread.CurrentThread.ManagedThreadId}");
            }
            private void Button_MouseOver1(object s, MouseOverEventArgs e)
            {
                Console.WriteLine($"Handler 2: Mouse is Over the element at: {e.Location} on {Thread.CurrentThread.ManagedThreadId}");
            }
        }
    }


}
