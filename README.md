# winforms-mvc - A library for MVC pattern in c# Windows Forms Application
MVC for C# Windows Forms

## MVC Usage

To use the MVC framework, you'll have to make some changes to the class containing the `Main` method, defaults to `Program.cs` in WinForms applications.

```
static class Program 
{
	[STAThread]
	static void Main() 
	{
		Application.EnableVisualStyles();
		Application.SetCompatibleTextRenderingDefault(false);

		AppManager.Start<TController>(); // replaced: Application.Run(formObject); where generic type TController is your starting controller
	}
}
```

##### AppManager

The AppManager class is initially used to keep the STA (Single Threaded Apartment) thread running, because WinForms applications run on one single thread. The AppManager singleton can be used to "travel" to different forms using the `Load` method from this class. The Load method requires a generic type which inherits the abstract `Controller` class. When the entered generic type is `Loadable`, it will execute the successhandler of the controller. If the controller was not able to load, the application will prompt a message to the user and exits the application when the message closes using the failedhandler. Both success and failed handlers can be overridden.

Because the AppManager is a singleton, we can use all the functionality of the AppManager instance anywhere in our application once the instance is initialized using `Start<TController>`.

Example: 
```
AppManager.Instance.Load<LoginController>(); 
```

##### Controllers

Controllers are used to handle events or other functionalities that should be processed on the backend. I use this MVC framework for pushing and pulling of data on the controller. When the `view` submits the data entered in it's form, the data will be send to the controller through events (callbacks) and the controller will insert them in the prefered type of `repository`.

Every controller must inherit the abstract Controller class or any other unsealed class that is derived from that class. 

Example:
```
public class LoginController : Controller 
{
	
	private LoginView _view;

	public override View 
	{
		get 
		{
			return _view;
		}
	}

	public LoginController() 
	{
		_view = new LoginView();
		_view.LoginAttempt += OnLoginAttemptHandler;
	}

	public override bool Loadable() 
	{
		return AppManager.Instance.User == null;
	}

	protected void OnLoginAttemptHandler(LoginAttemptEventArgs e) { /* ... */ }
	
}
```

##### Views

A `Form` is defined as a view in this MVC framework. This way we are not only benefitting from the built in designer of Visual Studios, but also the other libraries which alter or extend the designer of the winforms. A view does not know where the data is comming from, it has no relation to anything other than the `models`. 

When data, which was entered or created in the view, should be submitted to the backend, the view has to create an event which the controller can subscribe too to handle the submitted data (eg. store in dbo).

Example:
```
public partial class LoginView : Form, IView
{
	
	public event LoginAttemptEventHandler LoginAttempt;

	public LoginView() { /* ... */ }

	public Form Form 
	{
		get 
		{
			return this;
		}
	}

	public String Title
	{
		get 
		{
			return Text;
		}
		set 
		{
			Text = value;
		}
	}

	private void buttonLogin_Click(object sender, EventArgs e) 
	{
		string email = null; // the entered email address
		string password = null; // the entered password

		bool flag = false; // checks if the entered fields are valid
		/* 
			...
			...
			... 
		*/

		if (flag) 
		{
			OnLoginAttempt(new LoginAttempt(email, password));
		}
	}

	protected void OnLoginAttempt(LoginAttemptEventArgs e)
	{
		if (LoginAttempt != null)
		{
			LoginAttempt(e);
		}
	}

}
```
