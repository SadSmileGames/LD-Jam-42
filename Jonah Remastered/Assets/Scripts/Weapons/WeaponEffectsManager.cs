using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEffectsManager : MonoBehaviour
{
    public GameObject graphics;

    [Range(0, 0.2f)]
    public float recoilAmount = 1f;

    private SpriteRenderer spriteRenderer;
    private Vector3 recoilSmoothDamp;

    private void OnEnable()
    {
        PlayerEffectsController.OnDirectionChangedEvent += ChangeLookDirection;
    }

    private void OnDisable()
    {
        PlayerEffectsController.OnDirectionChangedEvent -= ChangeLookDirection;
    }

    private void Start ()
    {
        spriteRenderer = graphics.GetComponent<SpriteRenderer>();
	}

    private void Update()
    {
        UpdateRecoil();
    }

    public void ChangeLookDirection(int direction)
    {
        switch(direction)
        {
            case 1:
                ResetSpriteRenderer();
                spriteRenderer.sortingLayerName = "Obstacle";
                break;
            case 2:
                ResetSpriteRenderer();
                break;
            case 3:
                ResetSpriteRenderer();
                break;
            case 4:
                ResetSpriteRenderer();
                spriteRenderer.flipY = true;
                break;
            default:
                Debug.LogWarning("The event fired made no sense");
                break;
        }
    }

    public void Recoil()
    {
        graphics.transform.localPosition -= Vector3.right * recoilAmount;
    }

    private void UpdateRecoil()
    {
        graphics.transform.localPosition = Vector3.SmoothDamp(graphics.transform.localPosition, Vector3.zero, ref recoilSmoothDamp, 0.1f);
    }

    private void ResetSpriteRenderer()
    {
        spriteRenderer.flipX = false;
        spriteRenderer.flipY = false;
        spriteRenderer.sortingOrder = 0;
        spriteRenderer.sortingLayerName = "Weapons";
    }
}
