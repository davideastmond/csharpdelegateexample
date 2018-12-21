# C# Delegates and callbacks
This is an example and step-by-step guide of how I got callbacks and events (across two forms) to work in C# (this also works for Xamarin.iOS).
In this example, I have two forms (or viewControllers) VC-1 and VC-2. I should be able to trigger an event from VC-2 and have VC-1
respond to the event, for example the user clicks a submit button on VC-2, and the data is passed back to VC-1

### 1. Declare a globally accessible public delegate type at the namespace level with the method signature you desire.

For example:
```
namespace myNameSpace 
{
	public delegate void InfoReceived (object sender, EventArgs e);
	...
	public class whateverClass
	{
		
	}
}
```

### 2. In a seperate file (for clarity), create your protocol (a public class) with public fields that match the delegate's method signature.

For example, the properties / fields in the protocol class will match * sender * and  * EventArgs *.
```
public class myCallBackProtocol
{
	public object senderObject;
	public EventArgs pEventArgs;
}
```

### 3. In the protocol, create a public event of the type of your delegate, in this example, InfoReceived
example: 

``` public event InfoReceived OnInfoReceived; // This will be subscribed to later ```

### 4 a. Create a public method in your protocol whose arugments conform to the delegate method signature. In this public method, assign the desired fields / properties. This method is to be called at the exact time the event is to be raised and this is when the properties of the public protocol will be assigned.


### 4 b. Subscribe to the event (from step 3) in the protocol using the += synatax. The event handler subsequently created needs no code.
### 4 c. After subscribing, manually raise the event itself on the very next line. The arguments in the method in step 4a. should be used as arguments when manually raising the event itself.

```
public void RaiseEvent(object sender, EventArgs e)
{
	// Assign properties
	this.senderObject = sender;
	this.pEventArgs = e;
	OnInfoReceived += MyInfoProtocol_OnInfoReceived; // Subscribe to event
	OnInfoReceived(sender, e); // Manually raise the event

}

// Event method created after subscribing
void MyInfoProtocol_OnInfoReceived (object sender, EventArgs e)
{
	// no code needed
}
```

### 5. In ViewController-2 (VC-2) (the destination view controller), create a public property ( a delegate property) of the type of your protocol and use the new keyword to assign it an instance.
Example:

```
public partial class ViewController_2 : UIViewController
{
	public MyCallBackProtocol delegate_info = new MyCallBackProtocol();

	public ViewController_2 (IntPtr handle) : base (handle)
	{
		// constructor
	}
}
```

### 5 a. In the constructor of VC-2 subscribe to the public property's event (the event from step 3). The subscription needs no code. This needs to be done to avoid NullReferenceException.
Example
```
public partial class ViewController_2 : UIViewController
{
	public MyCallBackProtocol delegate_info = new MyCallBackProtocol();

	public ViewController_2 (IntPtr handle) : base (handle)
	{
		// constructor
		delegate_info += delegate_info_OnInfoReceived; // Subscribe to the event
	}
}

void delegate_info_OnInfoReceived (object sender, EventArgs e)
{
	// No code required here
}
```

### 6. *For Xamarin.iOS:* In ViewController-1 (VC-1), implement the preprareForSegue delegate method. Ensure your segueIdentifiers match up and that the correct class is associated with the DestinationViewControllerProperty.

### 6 a. In preparing for the segue obtain a reference to the destination view controller, and using its delegate property (from step 5), subscribe to the event you created in step 3.

### 6 b. In the subscribed event method, here is where you can get the information that is delievered when the event is triggered.

### 7. In VC-2, use its delegate property (from step 5) to call the method that triggers the actual event (the method was created in step 4 a) whenever you need to - such as a button click or whatever.

