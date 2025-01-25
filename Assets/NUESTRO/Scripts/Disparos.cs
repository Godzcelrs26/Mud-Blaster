using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class Disparos : MonoBehaviour
{
    [SerializeField]
    private float Burbujas = 10f;

    public PlayerMotor mr;
    void Awake()
    {
        mr =this.GetComponent<PlayerMotor>();
    }

    void Update()
    {
        
    }
   
}
