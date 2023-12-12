using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Cook
{
	public class UIData
	{
		public UIController Controller;
		public CanvasGroup CanvasGroup;
		public GraphicRaycaster GraphicRaycaster;
	}

	public class UICachedUnit
	{
		public string prefab;
		public ResourceRequest resourceRequest;
		public GameObject instanceUI;
		public Object asset;
	}

	public class UIManager : Singleton<UIManager>
	{
		private const int UI_LAYER_NORMAL = 0;
		private const int UI_LAYER_TUTORIAL = 1;


		[SerializeField] Camera _cameraUI;
		[SerializeField] EventSystem _eventSystem;
		[SerializeField] List<Transform> _UILayers;
		[SerializeField] protected Canvas _uiCanvas;

		[SerializeField] private UIController _firstUI;

		private List<UIController> _listUI;

		private UIController _currentUI;

		private readonly Stack<UIController> _history = new Stack<UIController>();

		private void Start()
		{
			// Show("UI/GameOverUI");
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.A))
			{
				Show("UI/GameOverUI");
			}
		}

		public T GetUI<T>() where T : UIController
		{
			for (int i = 0; i < Instance._listUI.Count; i++)
			{
				if (Instance._listUI[i] is T uiController)
				{
					return uiController;
				}
			}
			return null;
		}

		public void Show<T>(bool remember = true) where T : UIController
		{
			for (int i = 0; i < Instance._listUI.Count; i++)
			{
				if (Instance._listUI[i] is T)
				{
					if (Instance._currentUI != null)
					{
						if (remember)
						{
							Instance._history.Push(Instance._currentUI);
						}
						Instance._currentUI.Hide();
					}
					Instance._listUI[i].Show();
					Instance._currentUI = Instance._listUI[i];
				}
			}
		}

		public void Show(UIController uiController, bool remember = true)
		{
			if (Instance._currentUI != null)
			{
				if (remember)
				{
					Instance._history.Push(Instance._currentUI);
				}
				Instance._currentUI.Hide();
			}
			uiController.Show();
			Instance._currentUI = uiController;
		}

		public void ShowLast()
		{
			if (Instance._listUI.Count != 0)
			{
				Show(Instance._history.Pop(), false);
			}
		}

		public void Show(string path, int layer = UI_LAYER_NORMAL)
		{
			var ui = Instantiate(Resources.LoadAsync(path).asset).GetComponent<UIController>();
			ui.transform.SetParent(_UILayers[UI_LAYER_NORMAL], false);
			ui.Show();
		}


		// public T ShowUIOnTop<T>(string path, int layer = UI_LAYER_NORMAL) where T : UIController
		// {
		// 	return ShowUIOnTop<T>(path, false, layer);
		// }

		// public void ShowUIOnTop(UIController instanceUI, int layer = UI_LAYER_NORMAL)
		// {
		// 	ShowUIOnTop(instanceUI, false, layer);
		// }

		// public T ShowUIOnTop<T>(string path, bool overlap, int layer = UI_LAYER_NORMAL) where T : UIController
		// {
		// 	bool needStart;
		// 	var instanceUI = ShowUIOnTop<T>(path, overlap, null, out needStart, layer);
		// 	if (needStart)
		// 	{
		// 		instanceUI.UIStart();
		// 	}

		// 	return instanceUI;
		// }

		public void ShowUIOnTop(UIController instanceUI, bool overlap, int layer = UI_LAYER_NORMAL)
		{
			// var RootUI = _UILayers[layer];
			// if (instanceUI.transform.parent != RootUI)
			// {
			// 	instanceUI.transform.SetParent(RootUI, false);
			// 	RectTransform rectTrans = instanceUI.GetComponent<RectTransform>();
			// 	rectTrans.anchorMin = Vector2.zero;
			// 	rectTrans.anchorMax = Vector2.one;
			// 	rectTrans.offsetMin = rectTrans.offsetMax = Vector2.zero;
			// 	rectTrans.localScale = Vector3.one;

			// 	Canvas canvas = instanceUI.GetComponent<Canvas>();
			// 	canvas.renderMode = RenderMode.ScreenSpaceCamera;
			// 	canvas.worldCamera = _cameraUI;
			// 	canvas.planeDistance = 0f;
			// }

			// if (!overlap)
			// {
			// 	instanceUI.transform.SetAsLastSibling();
			// }

			// var activeData = new ActiveControllerData() { Controller = instanceUI };
			// VisibleUIs[layer].ActiveControllers.Add(activeData);

			// if (!instanceUI.gameObject.activeSelf)
			// {
			// 	instanceUI.gameObject.SetActive(true);
			// }

			// EnableUI(activeData, layer);
			// instanceUI.UIStart();
		}

		// IEnumerator InstancetiateUI(UICachedUnit cached, int layer = UI_LAYER_NORMAL)
		// {
		// 	yield return cached.resourceRequest;

		// 	var RootUI = _UILayers[layer];
		// 	var objInstance = Instantiate(cached.resourceRequest.asset) as GameObject;
		// 	objInstance.SetActive(false);
		// 	objInstance.transform.SetParent(RootUI, false);

		// 	RectTransform rectTrans = objInstance.GetComponent<RectTransform>();
		// 	rectTrans.anchorMin = Vector2.zero;
		// 	rectTrans.anchorMax = Vector2.one;
		// 	rectTrans.offsetMin = rectTrans.offsetMax = Vector2.zero;

		// 	yield return null;

		// 	cached.instanceUI = objInstance;
		// }
	}
}
