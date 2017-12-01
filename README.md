# csharp
Super crazy C# doubts resolved in code 

## OptionalParameters
Tests the priority of Optional Parametric overload vs exact match overload. It also test the <code>sizeof</code> for custom structs with and without fields.

## MultiLevelInheritance
Inheritance through <code>virtual</code>, <code>override</code>, <code>new</code> and new virtual methods

## StaticDirective and ref
Static directives holding to two methods with same name in different classes.
Call by reference for reference types using <code>ref</code>

## ConstructorObjectInitializer
Determines the order of object initializer and parametric constructor

## AsyncMain
Shows how to call <code>async</code> methods in a console app using <code>Task.Run</code> delegate.

## BoxingUnBoxing
Shows that while boxing both the Value and the Type are stored with in the wrapped object.
And hence we cannot unbox the object back to any other type that is compatible with the original boxed type.
You can only unbox using the cast to the original type and assign to any compatible type but the unboxing cast should be only to the boxed type.

## ThreadingWithLock
Shows how to access a common resource by multiple threads through <code>lock</code> statement in multithreaded program. 

## AutoResetEvent
Shows how the <code>AutoResetEvent</code> in a multi threaded program is used to signal the events to wait till the AutoResetEvent is <code>Set</code> or <code>Reset</code>.

## CheckedUnchecked
Shows how an exception is raised in a <code>checked</code> context in run time if the overflow bit is set.
The inner most enclosed context will get the priority to throw or surpress the run time overflow exceptions.

## EnumFlags
Shows that a <code>enum</code> can inherit the integral value type and also how to use the enum for the bit flag operations using the <code>Flag</code> attribute on the enum declaration.

## FrameworkEvents
Shows how to implement <code>event</code> using the Framework Guidlines.

## DelegatesAsEventsAndHandlers
Shows that the <code>event</code> is just an access modifier on the <code>delegate</code> to declare that particular delegate as an event handler.
Defines a custom delegate to implement an event behaviour as the framework's <code>EventHandler</code> delegate.
Contains a <code>Delegates</code> class that contains the common delegates that the framework provides as its properties that can hold an methods with their corresponding signatures.

## ForEachEffectedNoIEnumerable
This shows how a collection is affected by using the <code>foreach</code> iterator and modifying the collection with in the loop.
It also shows that the collection need not to implement the <code>IEnumerable</code> and the <code>IEnumerator</code> interfaces to be eligible for the foreach loop. 

## AsyncException
This shows how to catch the exceptions in an <code>async</code> method. This method is not a standard and need not hold good for debugging the exception. But it shows that it is possible to catch the exceptions that occur in async method.

## ConstVsReadOnly
Shows the basic difference between a <code>const</code> and a <code>readonly</code> variable.




