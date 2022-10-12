using UnityEngine.Events;

public interface IEventDispatcher
{
	void AddListener(string eventName, UnityAction<EventParams> callback);
	void DropListener(string eventName, UnityAction<EventParams> callback);
	void Dispatch(string eventName, EventParams eventData = null);
}