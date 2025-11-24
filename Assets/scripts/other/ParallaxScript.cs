using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ParallaxScript : MonoBehaviour
{
    [System.Serializable]
    public class ParalaxLayer
    {
        public Transform layer;
        [Range(0, 1)] public float parallaxFactor;
    }

    public ParalaxLayer[] layers;

    public Transform camTransform;
    private Vector3 lastCameraPosition;


    void Start()
    {
        lastCameraPosition = camTransform.position;
    }

    void LateUpdate()
    {
        Vector3 cameraDelta = camTransform.position - lastCameraPosition;

        foreach (ParalaxLayer layer in layers)
        {
            float moveX = cameraDelta.x * layer.parallaxFactor;
            float moveY = cameraDelta.y * layer.parallaxFactor;

            layer.layer.position += new Vector3(moveX, moveY, 0);
        }

        lastCameraPosition = camTransform.position;
    }
}

