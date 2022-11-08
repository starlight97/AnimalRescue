using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SceneMain : MonoBehaviour, IEventDispatcher
{
    private EventDispatcher eventDispatcher = new EventDispatcher();
    public UnityEvent onDestroy = new UnityEvent();
    public bool useOnDestoryEvent = true;

    protected OptionManager optionManager;
    protected UIBase uiBase;

    public virtual void Init(SceneParams param = null)
    {
        this.optionManager = GameObject.FindObjectOfType<OptionManager>();
        this.uiBase = GameObject.FindObjectOfType<UIBase>();
    }

    public void OptionInit()
    {
        this.optionManager.Init();
    }

    public void AddListener(string eventName, UnityAction<EventParams> callback)
    {
        this.eventDispatcher.AddListener(eventName, callback);
    }

    public void DropListener(string eventName, UnityAction<EventParams> callback)
    {
        this.eventDispatcher.DropListener(eventName, callback);
    }

    public void Dispatch(string eventName, EventParams eventData = null)
    {
        this.eventDispatcher.Dispatch(eventName, eventData);
    }

    private void OnDestroy()
    {
        if (this.useOnDestoryEvent)
            this.onDestroy.Invoke();
    }

}
