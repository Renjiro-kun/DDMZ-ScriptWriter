using Avalonia.Controls;
using System.IO;
using DDScriptWriter.ViewModels;
using System.Collections.Generic;

namespace DDScriptWriter.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
    }

	private void AddLine_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
	{
        (DataContext as MainViewModel).MessagesList.Add(new MessageInternal( 0, "test" ));
	}

	private void SaveScript_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
	{
		const string fileName = "script.scr";

		using (var stream = File.Open(fileName, FileMode.Create))
		{
			using(var writer = new BinaryWriter(stream, System.Text.Encoding.UTF8, false))
			{
				writer.Write('D');
				writer.Write('D');
				writer.Write('S');
				writer.Write('R');
				byte type = 1;
				writer.Write(type);
				var messages = (DataContext as MainViewModel).MessagesList;
				short messageCount = (short)messages.Count;
				writer.Write(messageCount);

				foreach(var msg in messages)
				{
					writer.Write(msg.ID);
					byte msgType = 0;
					writer.Write(msgType);
					string outStr = msg.Message + '\0';
					writer.Write(outStr);
				}
			}
		}
	}
}
