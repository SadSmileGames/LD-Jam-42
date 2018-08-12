using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeTransform : MonoBehaviour
{
    public class ShakeEvent
    {
        float duration;
        float timeRemaining;

        ShakeTransformEventData data;

        public ShakeTransformEventData.Target target
        {
            get
            {
                return data.target;
            }
        }

        Vector3 noiseOffset;
        public Vector3 noise;

        public ShakeEvent(ShakeTransformEventData data)
        {
            this.data = data;
            this.duration = data.duration;
            this.timeRemaining = duration;

            float rand = 32.0f;

            noiseOffset.x = Random.Range(0.0f, rand);
        }

        public void Update()
        {
            float deltaTime = Time.deltaTime;
            timeRemaining -= deltaTime;

            float noiseOffsetDelta = deltaTime * data.frequency;

            noiseOffset.x += noiseOffsetDelta;
            noiseOffset.y += noiseOffsetDelta;
            noiseOffset.z += noiseOffsetDelta;

            noise.x = Mathf.PerlinNoise(noiseOffset.x, 0.0f);
            noise.y = Mathf.PerlinNoise(noiseOffset.y, 1.0f);
            noise.z = Mathf.PerlinNoise(noiseOffset.z, 2.0f);

            noise -= Vector3.one * 0.5f;

            noise *= data.amplitude;

            float agePercent = 1.0f - (timeRemaining / duration);
            noise *= data.blendOverLifeTime.Evaluate(agePercent);
        }

        public bool IsAlive()
        {
            return timeRemaining > 0.0f;
        }
    }

    List<ShakeEvent> shakeEvents = new List<ShakeEvent>();

    public void AddShakeEvent(ShakeTransformEventData data)
    {
        shakeEvents.Add(new ShakeEvent(data));
    }

    public void AddShakeEvent(float amplitude, float frequency, float duration, AnimationCurve blendOverLifeTime, ShakeTransformEventData.Target target)
    {
        ShakeTransformEventData data = ScriptableObject.CreateInstance<ShakeTransformEventData>();
        data.Init(amplitude, frequency, duration, blendOverLifeTime, target);

        AddShakeEvent(data);
    }

    private void LateUpdate()
    {
        Vector3 positionOffset = Vector3.zero;
        Vector3 rotattionOffset = Vector3.zero;

        for (int i = shakeEvents.Count - 1; i != -1; i--)
        {
            ShakeEvent shakeEvent = shakeEvents[i];
            shakeEvent.Update();

            if(shakeEvent.target == ShakeTransformEventData.Target.Position)
            {
                positionOffset += shakeEvent.noise;
            }
            else
            {
                rotattionOffset += shakeEvent.noise;
            }

            if(!shakeEvent.IsAlive())
            {
                shakeEvents.RemoveAt(i);
            }
        }

        transform.localPosition = positionOffset;
        transform.localEulerAngles = rotattionOffset;
    }
}
