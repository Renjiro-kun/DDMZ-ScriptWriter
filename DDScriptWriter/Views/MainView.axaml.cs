using Avalonia.Controls;
using System.IO;
using DDScriptWriter.ViewModels;
using System.Collections.Generic;
using Avalonia.Platform.Storage;
using DDScriptWriter.Logic;

namespace DDScriptWriter.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
    }

	private void AddLine_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
	{
        (DataContext as MainViewModel).MessagesList.Add(new MessageInternal( ScriptManager.Instance.CurrentId, "test" ));
	}


	private void ReadScriptFile(string filename)
	{
		var messages = (DataContext as MainViewModel).MessagesList;
		messages.Clear();
		ScriptManager.Instance.ResetIdCounter();

		using (var stream = File.Open(filename, FileMode.Open))
		{
			using (var reader = new BinaryReader(stream, System.Text.Encoding.UTF8, false))
			{
				byte[] magicNumber = new byte[4];
				reader.Read(magicNumber, 0, 4);
				byte scriptType = reader.ReadByte();
				short messageCount = reader.ReadInt16();

				for (int i = 0; i < messageCount; i++)
				{
					short id = reader.ReadInt16();
					short idTmp = ScriptManager.Instance.CurrentId;
					short msgType = reader.ReadByte();
					string text = reader.ReadString();
					// Removing '\0' from the string
					text = text.Remove(text.Length - 1, 1);
					messages.Add(new MessageInternal(id,text));
				}

			}
		}
	}
	private void WriteScriptFile(string filename)
	{
		using (var stream = File.Open(filename, FileMode.Create))
		{
			using (var writer = new BinaryWriter(stream, System.Text.Encoding.UTF8, false))
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

				foreach (var msg in messages)
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

	private async void OpenScript_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
	{
		// Get top level from the current control. Alternatively, you can use Window reference instead.
		var topLevel = TopLevel.GetTopLevel(this);

		FilePickerFileType scrType = new FilePickerFileType("Script Files")
		{
			Patterns = new[] { "*.scr" }
		};

		// Start async operation to open the dialog.
		var file = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
		{
			Title = "Open Script File",
			FileTypeFilter = new[] { scrType }
		});

		if (file is not null)
		{
			ReadScriptFile(file[0].Path.AbsolutePath);
		}
	}

	private async void SaveScript_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
	{
		// Get top level from the current control. Alternatively, you can use Window reference instead.
		var topLevel = TopLevel.GetTopLevel(this);

		FilePickerFileType scrType = new FilePickerFileType("Script Files")
		{
			Patterns = new[] { "*.scr" }
		};

		// Start async operation to open the dialog.
		var file = await topLevel.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
		{
			Title = "Save Script File",
			FileTypeChoices = new[] { scrType }
		});

		if (file is not null)
		{
			WriteScriptFile(file.Path.AbsolutePath);
		}
	}
}
