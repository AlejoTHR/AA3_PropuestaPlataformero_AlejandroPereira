using UnityEngine;

public class Parallax_Script : MonoBehaviour
{


     // PARALLAX TESTING; ONLY TO DEBUG HOW TO WOEK AND HOW IT LOOKS WHILE MOVING
    Material _mtrl;
    float distance;

    [Range(0f, 0.5f)] public float Speed;

    private void Start()
    {
        _mtrl = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        distance += Time.deltaTime * Speed;
        _mtrl.SetTextureOffset("_MainTex", Vector2.right * distance);
    }

}
