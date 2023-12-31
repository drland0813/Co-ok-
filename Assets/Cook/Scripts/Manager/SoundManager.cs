using System.Collections.Generic;
using UnityEngine;

namespace Drland.Cook
{
	public enum SoundType
	{
		DeliveryFail,
		DeliverySuccess,
		FootStep,
		ObjectDrop,
		ObjectPickup,
		Chop,
		StoveSizzle,
		Trash,
		Warning,
		Walking
	}

	public class SoundManager : Singleton<SoundManager>
	{

		[SerializeField] private AudioClipRefSO _audioClipRefSO;

		private Dictionary<SoundType, AudioClip[]> _soundDict;
		protected override void Awake()
		{
			base.Awake();
			_soundDict = new Dictionary<SoundType, AudioClip[]>()
			{
				{ SoundType.DeliveryFail, _audioClipRefSO.DeliveryFail},
				{ SoundType.DeliverySuccess, _audioClipRefSO.DeliverySuccess},
				{ SoundType.FootStep, _audioClipRefSO.Footstep},
				{ SoundType.ObjectDrop, _audioClipRefSO.ObjectDrop},
				{ SoundType.ObjectPickup, _audioClipRefSO.ObjectPickup},
				{ SoundType.Chop, _audioClipRefSO.Chop},
				{ SoundType.StoveSizzle, _audioClipRefSO.StoveSizzle},
				{ SoundType.Trash, _audioClipRefSO.Trash},
				{ SoundType.Warning, _audioClipRefSO.Warning},
				{ SoundType.Walking, _audioClipRefSO.Walking}
			};
		}

		public void PlaySound(SoundType type, Transform point)
		{
			if (_soundDict.TryGetValue(type, out var audioClips))
			{
				PlaySound(audioClips, point.position);
			}
			else
			{
				Debug.LogWarning("Sound not found");
			}
		}

		private void PlaySound(IReadOnlyList<AudioClip> audioClips, Vector3 point, float volume = 1f)
		{
			AudioSource.PlayClipAtPoint(audioClips[UnityEngine.Random.Range(0, audioClips.Count)], point, volume);
		}
	}
}