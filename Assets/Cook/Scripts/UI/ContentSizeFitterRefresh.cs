using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Drland.Cook
{
	public class ContentSizeFitterRefresh : MonoBehaviour
	{
		private void Awake()
		{
			RefreshContentFitters();
		}

		public void RefreshContentFitters()
		{
			var rectTransform = (RectTransform)transform;
			RefreshContentFitter(rectTransform);
		}

		private void RefreshContentFitter(RectTransform transformHasContentFitter)
		{
			if (transformHasContentFitter == null || !transformHasContentFitter.gameObject.activeSelf) return;

			foreach (RectTransform child in transformHasContentFitter)
			{
				RefreshContentFitter(child);
			}

			var layoutGroup = transformHasContentFitter.GetComponent<LayoutGroup>();
			var contentSizeFitter = transformHasContentFitter.GetComponent<ContentSizeFitter>();
			if (layoutGroup != null)
			{
				layoutGroup.SetLayoutHorizontal();
				layoutGroup.SetLayoutVertical();
			}

			if (contentSizeFitter != null)
			{
				LayoutRebuilder.ForceRebuildLayoutImmediate(transformHasContentFitter);
			}
		}
	}
}

