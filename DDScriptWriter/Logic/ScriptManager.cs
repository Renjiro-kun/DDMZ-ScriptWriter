using Avalonia.Controls.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDScriptWriter.Logic
{
	public enum MessageType : byte
	{
		GENERIC = 0,
		NARRATED = 1,
		CHOICE = 2,
		MULTILINE = 3,
		MULTILINE_NARRATED = 4
	}

	public struct Message
	{
		public MessageType Type;
		public IMessageData Data;
	}

	public interface IMessageData { }

	public struct MessageDataGeneric : IMessageData
	{
		public byte CharactersCount;
		public string MessageString;
	}

	class Script
	{
		public Script()
		{
			_Messages = new List<Message>();
		}

		public void AddMessage(Message msg) { _Messages.Add(msg); }

		List<Message> _Messages;
	}

	public class ScriptManager
	{
		private static ScriptManager _instance = null;

		public static ScriptManager Instance
		{
			get
			{
				if(_instance == null)
				{
					_instance = new ScriptManager();
				}
				return _instance;
			}
		}


		private short _IdCounter;

		public short CurrentId 
		{
			get 
			{
				short retValue = _IdCounter;
				_IdCounter++;
				return retValue;
			}
		}

		public void ResetIdCounter() { _IdCounter = 0; }

		private ScriptManager()
		{
			_IdCounter = 0;
		}

	}
}
