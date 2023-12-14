using System.Collections;
using System.Collections.Generic;
using UnityEngine;
	
namespace Drland.Cook
{
	public abstract class StaticSingleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		public static T Instance { get; private set; }

		protected virtual void Awake() => Instance = this as T;

		protected virtual void OnApplicationQuit()
		{
			Instance = null;	
			Destroy(gameObject);
		}
	}

	public abstract class Singleton<T> : StaticSingleton<T> where T : MonoBehaviour
	{
		protected override void Awake()
		{
			if (Instance != null) Destroy(gameObject);
			base.Awake();
		}
	}

	public abstract class PersistentSingleton<T> : StaticSingleton<T> where T : MonoBehaviour
	{
		protected override void Awake()
		{
			base.Awake();
			DontDestroyOnLoad(gameObject);
		}
	}
}