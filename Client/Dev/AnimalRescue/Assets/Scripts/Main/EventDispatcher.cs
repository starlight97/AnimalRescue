using System.Collections.Generic;
using UnityEngine.Events;

public class EventDispatcher : IEventDispatcher
{

	private Dictionary<string, List<UnityAction<EventParams>>> dicListeners = new Dictionary<string, List<UnityAction<EventParams>>>();


	public void AddListener(string eventName, UnityAction<EventParams> callback)
	{
		List<UnityAction<EventParams>> evtListeners = null;
		if (dicListeners.TryGetValue(eventName, out evtListeners))
		{
			evtListeners.Remove(callback);
			evtListeners.Add(callback);
		}
		else
		{
			evtListeners = new List<UnityAction<EventParams>>();
			evtListeners.Add(callback);
			this.dicListeners.Add(eventName, evtListeners);
		}
	}

	public void DropListener(string eventName, UnityAction<EventParams> callback)
	{
		List<UnityAction<EventParams>> evtListeners = null;
		if (this.dicListeners.TryGetValue(eventName, out evtListeners))
		{
			for (int i = 0; i < evtListeners.Count; i++)
			{
				evtListeners.Remove(callback);
			}
		}
	}

	public void Dispatch(string eventName, EventParams data)
	{
		List<UnityAction<EventParams>> evtListeners = null;
		if (this.dicListeners.TryGetValue(eventName, out evtListeners))
		{
			for (int i = 0; i < evtListeners.Count; i++)
			{
				evtListeners[i](data);
			}
		}
	}
}
