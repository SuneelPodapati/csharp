using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
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
            public void RenderUI()
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

        class EventPropertyButton  // Where events are declared as event properties
        {
            // The class EventPropertyButton defines two event properties, MouseUp and MouseDown.

            // Define the delegate collection (EventHandlerList) to hold the delegates as a key value pairs.
            protected EventHandlerList listEventDelegates = new EventHandlerList();

            // Define a unique key for each event to hold its corresponding event handlers in the collection.
            static readonly object mouseDownEventKey = new object();
            static readonly object mouseUpEventKey = new object();

            // Define the MouseDown event property.
            public event EventHandler MouseDown
            {
                // Add the input delegate to the collection.
                add
                {
                    // The event handler that is attached to this event by the subscriber will come in to this add property through the value.
                    // eventPropertyComponent.MouseDown += MyEventHandler triggers the call to this add property on this event
                    listEventDelegates.AddHandler(mouseDownEventKey, value);
                }
                // Remove the input delegate from the collection.
                remove
                {
                    // The event handler that is removed from this event by the subscriber will come in to this remove property through the value.
                    // eventPropertyComponent.MouseDown -= MyEventHandler triggers the call to this remove property on this event
                    listEventDelegates.RemoveHandler(mouseDownEventKey, value);
                }
            }

            // Raise the event with the delegate specified by mouseDownEventKey
            private void OnMouseDown(MouseOverEventArgs e)
            {
                EventHandler mouseDown =
                    (EventHandler)listEventDelegates[mouseDownEventKey];
                mouseDown?.Invoke(this, e);
            }

            // Define the MouseUp event property.
            public event EventHandler MouseUp
            {
                // Add the input delegate to the collection.
                add
                {
                    listEventDelegates.AddHandler(mouseUpEventKey, value);
                }
                // Remove the input delegate from the collection.
                remove
                {
                    listEventDelegates.RemoveHandler(mouseUpEventKey, value);
                }
            }

            // Raise the event with the delegate specified by mouseUpEventKey
            private void OnMouseUp(MouseOverEventArgs e)
            {
                EventHandler mouseUp =
                    (EventHandler)listEventDelegates[mouseUpEventKey];
                mouseUp?.Invoke(this, e);
            }
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
                    TyreTemparature += (Speed / 10) + 3.14f - (Speed / 4);
                    Console.WriteLine($"Speed: {Speed} and Temparature: {TyreTemparature}");
                    Thread.Sleep(1000);
                }
            }

            private void Puncture(PuncturedArgs e)
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

    namespace ExpressionsAndTrees
    {
        // Base Visitor class:
        public abstract class Visitor
        {
            private readonly Expression node;

            protected Visitor(Expression node)
            {
                this.node = node;
            }

            public abstract void Visit(string prefix);

            public ExpressionType NodeType => this.node.NodeType;
            public static Visitor CreateFromExpression(Expression node)
            {
                switch (node.NodeType)
                {
                    case ExpressionType.Constant:
                        return new ConstantVisitor((ConstantExpression)node);
                    case ExpressionType.Lambda:
                        return new LambdaVisitor((LambdaExpression)node);
                    case ExpressionType.Parameter:
                        return new ParameterVisitor((ParameterExpression)node);
                    case ExpressionType.Add:
                        return new BinaryVisitor((BinaryExpression)node);
                    default:
                        Console.Error.WriteLine($"Node not processed yet: {node.NodeType}");
                        return default(Visitor);
                }
            }
        }

        // Lambda Visitor
        public class LambdaVisitor : Visitor
        {
            private readonly LambdaExpression node;
            public LambdaVisitor(LambdaExpression node) : base(node)
            {
                this.node = node;
            }

            public override void Visit(string prefix)
            {
                Console.WriteLine($"{prefix}This expression is a {NodeType} expression type");
                Console.WriteLine($"{prefix}The name of the lambda is {((node.Name == null) ? "<null>" : node.Name)}");
                Console.WriteLine($"{prefix}The return type is {node.ReturnType.ToString()}");
                Console.WriteLine($"{prefix}The expression has {node.Parameters.Count} argument(s). They are:");
                // Visit each parameter:
                foreach (var argumentExpression in node.Parameters)
                {
                    var argumentVisitor = Visitor.CreateFromExpression(argumentExpression);
                    argumentVisitor.Visit(prefix + "\t");
                }
                Console.WriteLine($"{prefix}The expression body is:");
                // Visit the body:
                var bodyVisitor = Visitor.CreateFromExpression(node.Body);
                bodyVisitor.Visit(prefix + "\t");
            }
        }

        // Binary Expression Visitor:
        public class BinaryVisitor : Visitor
        {
            private readonly BinaryExpression node;
            public BinaryVisitor(BinaryExpression node) : base(node)
            {
                this.node = node;
            }

            public override void Visit(string prefix)
            {
                Console.WriteLine($"{prefix}This binary expression is a {NodeType} expression");
                var left = Visitor.CreateFromExpression(node.Left);
                Console.WriteLine($"{prefix}The Left argument is:");
                left.Visit(prefix + "\t");
                var right = Visitor.CreateFromExpression(node.Right);
                Console.WriteLine($"{prefix}The Right argument is:");
                right.Visit(prefix + "\t");
            }
        }

        // Parameter visitor:
        public class ParameterVisitor : Visitor
        {
            private readonly ParameterExpression node;
            public ParameterVisitor(ParameterExpression node) : base(node)
            {
                this.node = node;
            }

            public override void Visit(string prefix)
            {
                Console.WriteLine($"{prefix}This is an {NodeType} expression type");
                Console.WriteLine($"{prefix}Type: {node.Type.ToString()}, Name: {node.Name}, ByRef: {node.IsByRef}");
            }
        }

        // Constant visitor:
        public class ConstantVisitor : Visitor
        {
            private readonly ConstantExpression node;
            public ConstantVisitor(ConstantExpression node) : base(node)
            {
                this.node = node;
            }

            public override void Visit(string prefix)
            {
                Console.WriteLine($"{prefix}This is an {NodeType} expression type");
                Console.WriteLine($"{prefix}The type of the constant value is {node.Type}");
                Console.WriteLine($"{prefix}The value of the constant value is {node.Value}");
            }
        }
    }

    namespace ObserverPattern
    {
        public class BaggageInfo
        {
            private int flightNo;
            private string origin;
            private int location;

            internal BaggageInfo(int flight, string from, int carousel)
            {
                this.flightNo = flight;
                this.origin = from;
                this.location = carousel;
            }

            public int FlightNumber
            {
                get { return this.flightNo; }
            }

            public string From
            {
                get { return this.origin; }
            }

            public int Carousel
            {
                get { return this.location; }
            }
        }

        public class BaggageHandler : IObservable<BaggageInfo>
        {
            private List<IObserver<BaggageInfo>> observers;
            private List<BaggageInfo> flights;

            public BaggageHandler()
            {
                observers = new List<IObserver<BaggageInfo>>();
                flights = new List<BaggageInfo>();
            }

            public IDisposable Subscribe(IObserver<BaggageInfo> observer)
            {
                // Check whether observer is already registered. If not, add it
                if (!observers.Contains(observer))
                {
                    observers.Add(observer);
                    // Provide observer with existing data.
                    foreach (var item in flights)
                        observer.OnNext(item);
                }
                return new Unsubscriber<BaggageInfo>(observers, observer);
            }

            // Called to indicate all baggage is now unloaded.
            public void BaggageStatus(int flightNo)
            {
                BaggageStatus(flightNo, String.Empty, 0);
            }

            public void BaggageStatus(int flightNo, string from, int carousel)
            {
                var info = new BaggageInfo(flightNo, from, carousel);

                // Carousel is assigned, so add new info object to list.
                if (carousel > 0 && !flights.Contains(info))
                {
                    flights.Add(info);
                    foreach (var observer in observers)
                        observer.OnNext(info);
                }
                else if (carousel == 0)
                {
                    // Baggage claim for flight is done
                    var flightsToRemove = new List<BaggageInfo>();
                    foreach (var flight in flights)
                    {
                        if (info.FlightNumber == flight.FlightNumber)
                        {
                            flightsToRemove.Add(flight);
                            foreach (var observer in observers)
                                observer.OnNext(info);
                        }
                    }
                    foreach (var flightToRemove in flightsToRemove)
                        flights.Remove(flightToRemove);

                    flightsToRemove.Clear();
                }
            }

            public void LastBaggageClaimed()
            {
                foreach (var observer in observers)
                    observer.OnCompleted();

                observers.Clear();
            }
        }

        internal class Unsubscriber<BaggageInfo> : IDisposable
        {
            private List<IObserver<BaggageInfo>> _observers;
            private IObserver<BaggageInfo> _observer;

            internal Unsubscriber(List<IObserver<BaggageInfo>> observers, IObserver<BaggageInfo> observer)
            {
                this._observers = observers;
                this._observer = observer;
            }

            public void Dispose()
            {
                if (_observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        }

        public class ArrivalsMonitor : IObserver<BaggageInfo>
        {
            private string name;
            private List<string> flightInfos = new List<string>();
            private IDisposable cancellation;
            private string fmt = "{0,-20} {1,5}  {2, 3}";

            public ArrivalsMonitor(string name)
            {
                if (String.IsNullOrEmpty(name))
                    throw new ArgumentNullException("The observer must be assigned a name.");

                this.name = name;
            }

            public virtual void Subscribe(BaggageHandler provider)
            {
                cancellation = provider.Subscribe(this);
            }

            public virtual void Unsubscribe()
            {
                cancellation.Dispose();
                flightInfos.Clear();
            }

            public virtual void OnCompleted()
            {
                flightInfos.Clear();
            }

            // No implementation needed: Method is not called by the BaggageHandler class.
            public virtual void OnError(Exception e)
            {
                // No implementation.
            }

            // Update information.
            public virtual void OnNext(BaggageInfo info)
            {
                bool updated = false;

                // Flight has unloaded its baggage; remove from the monitor.
                if (info.Carousel == 0)
                {
                    var flightsToRemove = new List<string>();
                    string flightNo = String.Format("{0,5}", info.FlightNumber);

                    foreach (var flightInfo in flightInfos)
                    {
                        if (flightInfo.Substring(21, 5).Equals(flightNo))
                        {
                            flightsToRemove.Add(flightInfo);
                            updated = true;
                        }
                    }
                    foreach (var flightToRemove in flightsToRemove)
                        flightInfos.Remove(flightToRemove);

                    flightsToRemove.Clear();
                }
                else
                {
                    // Add flight if it does not exist in the collection.
                    string flightInfo = String.Format(fmt, info.From, info.FlightNumber, info.Carousel);
                    if (!flightInfos.Contains(flightInfo))
                    {
                        flightInfos.Add(flightInfo);
                        updated = true;
                    }
                }
                if (updated)
                {
                    flightInfos.Sort();
                    Console.WriteLine("Arrivals information from {0}", this.name);
                    foreach (var flightInfo in flightInfos)
                        Console.WriteLine(flightInfo);

                    Console.WriteLine();
                }
            }
        }
    }
}
