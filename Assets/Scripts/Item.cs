using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Vector3 itemPosition;
    public float speed = 5f;
    public float timeToLive = 5f;
    private float timeElapsed = 0f;
    private SpriteRenderer spriteRenderer;

    private Vector3 targetLanePosition;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if(spriteRenderer != null)
        {
            spriteRenderer.color = GetRandomColor();
        }
    }

    void Start()
    {
        itemPosition = new Vector3(0, 0, 0);
        transform.position = itemPosition;
        transform.localScale = new Vector3(0.1f, 0.1f, 1);

        int lane = Random.Range(0, 3);
        switch (lane)
        {
            case 0:
                targetLanePosition = new Vector3(-6f, -6.34f, 0);
                break;
            case 1:
                targetLanePosition = new Vector3(0f, -6.34f, 0);
                break;
            case 2:
                targetLanePosition = new Vector3(6f, -6.34f, 0);
                break;
        }
    }

    void Update()
    {
        itemPosition = Vector3.MoveTowards(itemPosition, targetLanePosition, speed * Time.deltaTime);

        float perspective = Mathf.Lerp(0.1f, 1f, timeElapsed / timeToLive);
        transform.localScale = new Vector3(perspective, perspective, 1);
        transform.position = new Vector3(itemPosition.x, itemPosition.y, itemPosition.z);

        timeElapsed += Time.deltaTime;

        if (timeElapsed >= timeToLive)
        {
            Destroy(gameObject);
        }
    }

    private Color GetRandomColor()
    {
        var rRand = Random.Range(0f, 1f);
        var gRand = Random.Range(0f, 1f);
        var bRand = Random.Range(0f, 1f);
        return new Color(rRand, gRand, bRand);
    }
}