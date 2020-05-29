using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movimento")]
    [SerializeField] private float velocidade;
    [Header("Camera")]
    [SerializeField] private float sensibilidade;
    [SerializeField] private float anguloVMax;

    private Vector3 movimento, mouseCamera;
    private Rigidbody rbPlayer;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rbPlayer = this.GetComponent<Rigidbody>();    
    }

    void Update()
    {
        #region InputMovimento
        movimento = new Vector3(Input.GetAxisRaw("Horizontal"), 0 ,Input.GetAxisRaw("Vertical")).normalized * velocidade ;
        #endregion
        #region InputMouseCamera
        mouseCamera = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        #endregion
    }

    private void FixedUpdate()
    {
        #region Movimento
        rbPlayer.velocity = this.transform.forward * movimento.z + this.transform.right * movimento.x + rbPlayer.velocity.y * Vector3.up;
        #endregion
        #region MouseCamera
        this.transform.eulerAngles += mouseCamera.x * sensibilidade * Vector3.up;

        Camera.main.transform.localEulerAngles = Mathf.Clamp((Camera.main.transform.localEulerAngles.x <= 180 ? Camera.main.transform.localEulerAngles.x : Camera.main.transform.localEulerAngles.x - 360) - mouseCamera.y * sensibilidade, -anguloVMax, anguloVMax) * Vector2.right;
        #endregion
    }
}
