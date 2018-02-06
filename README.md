 # csharp
Super crazy C# doubts resolved in code 

## OptionalParameters
Tests the priority of Optional Parametric overload vs exact match overload. It also test the <code>sizeof</code> for custom structs with and without fields. An absolute match has higher priority than overloaded match with optional parameters.

## MultiLevelInheritance
Inheritance through <code>virtual</code>, <code>override</code>, <code>new</code> and new virtual methods. Every <code>new</code> method with same name as in parent will create a new child method with same name and both will exists. To call the base version, we have to use the base reference.

## StaticDirective and ref
Static directives holding to two methods with same name in different classes. In such scenerios we have to Call them with fully qualified name.
Call by reference for reference types using <code>ref</code> allows to change the whole object itself and replace it with new object

## ConstructorObjectInitializer
Determines the order of object initializer and parametric constructor, always constructors calls first follwed by object initializers

## AsyncMain
Shows how to call <code>async</code> methods in a console app using <code>Task.Run</code> delegate and awaiting the result using <code>GetAwaiter()</code> call

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
This shows how a collection is affected by using the <code>foreach</code> iterator and modifying the collection with in the loop. Generally they are cached for performance and hence the iteration will continue on the initial collection.
It also shows that the collection need not to implement the <code>IEnumerable</code> and the <code>IEnumerator</code> interfaces to be eligible for the foreach loop. 

## AsyncException
This shows how to catch the exceptions in an <code>async</code> method. This method is not a standard and need not hold good for debugging the exception. But it shows that it is possible to catch the exceptions that occur in async method.

## ExpressionsAndTrees
Shows how to parse and traverse an expression tree in c#. It also shows how to use a delegate to create and call a method on the fly at the runtime.

## ObserverPattern
Shows how Observer pattern is implemented in C# using the <code>IObserver</code>, <code>IObservable</code> and <code>IDisposable</code> interfaces. Basically Observable holds a list of Observers that subscribe to them and returns a Disposable to those observers so that when ever they want to unsubcribe, they can use the Disposable to unsubscribe.


## ConstVsReadOnly
Shows the basic difference between a <code>const</code> and a <code>readonly</code> variable. The main difference is that <code>const</code> should be initialized in declaration, but <code>readonly</code> can be initialized in constructor as well.




