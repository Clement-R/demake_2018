using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions {

	public static IEnumerator Fade(this CanvasGroup canvasGroup, float time, bool fadeIn, bool unscaledTime = true) {
		float from = fadeIn ? 0 : 1;
		float to = 1 - from;
		canvasGroup.interactable = false;
		yield return null;
		for (float f = 0; f < time; f += unscaledTime ? Time.unscaledDeltaTime : Time.deltaTime) {
			canvasGroup.alpha = Mathf.Lerp(from, to, f / time);
			yield return null;
		}

		canvasGroup.alpha = to;
		canvasGroup.interactable = fadeIn;
		canvasGroup.blocksRaycasts = fadeIn;
	}
}
