using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
