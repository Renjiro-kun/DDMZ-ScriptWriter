using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DDScriptWriter.ViewModels;

public class MessageInternal
{
	public short ID { get; set; }
	public string Message { get; set; }

	public MessageInternal(short id, string message)
	{
		Message = message;
		ID = id;
	}

}

public class MainViewModel : ViewModelBase
{
    public string Greeting => "Welcome to Avalonia!";
	public ObservableCollection<MessageInternal> MessagesList { get; set; }

	public MainViewModel()
    {
		MessagesList = new ObservableCollection<MessageInternal>(new List<MessageInternal>
		{
			new MessageInternal(0, "Test"),
			new MessageInternal(1, "Test 2"),
			new MessageInternal(3, "Test 4")
		});
	}
}
