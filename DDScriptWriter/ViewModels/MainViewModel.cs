using System.Collections.Generic;
using System.Collections.ObjectModel;
using DDScriptWriter.Logic;


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
			new MessageInternal(ScriptManager.Instance.CurrentId, "Test"),
			new MessageInternal(ScriptManager.Instance.CurrentId, "Test 2"),
			new MessageInternal(ScriptManager.Instance.CurrentId, "Test 4")
		});
	}
}
