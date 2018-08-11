using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEffectsManager : MonoBehaviour
{
    public GameObject graphics;
    private SpriteRenderer spriteRenderer;

    private void OnEnable()
    {
        PlayerEffectsController.OnDirectionChangedEvent += ChangeLookDirection;
    }

    private void OnDisable()
    {
        PlayerEffectsController.OnDirectionChangedEvent -= ChangeLookDirection;
    }

    void Start ()
    {
        spriteRenderer = graphics.GetComponent<SpriteRenderer>();
	}
	
    public void ChangeLookDirection(int direction)
    {
        switch(direction)
        {
            case 1:
                ResetSpriteRenderer();
                spriteRenderer.sortingOrder = -1;
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

    private void ResetSpriteRenderer()
    {
        spriteRenderer.flipX = false;
        spriteRenderer.flipY = false;
        spriteRenderer.sortingOrder = 0;
    }
}
