using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [SerializeField] Transform playerBody;      // �÷��̾��� ��ü.

    [Range(0.5f, 4.0f)]
    [SerializeField] float sensitivityX;        // ���� ����.

    [Range(0.5f, 4.0f)]
    [SerializeField] float sensitivityY;        // ���� ����.


    [SerializeField] float minX;
    [SerializeField] float maxX;


    bool isLockMouse = false;        // ���콺 ��� ����.
    float xRotation = 0f;            // x�� ȸ�� ��.

    private void Awake()
    {
        OnLockMouse();
    }
    private void Update()
    {
        if (!isLockMouse)
            return;

        float mouseX = Input.GetAxis("Mouse X") * sensitivityX;  // ������ ���� ���콺�� x�� ������.
        float mouseY = Input.GetAxis("Mouse Y") * sensitivityY;  // ������ ���� ���콺�� y�� ������.

        OnMouseLook(new Vector2(mouseX, mouseY));
    }

    private void OnMouseLook(Vector2 axis)
    {
        // ���� ȸ��.
        playerBody.Rotate(Vector3.up * axis.x);

        // ���� ȸ��.
        xRotation = Mathf.Clamp(xRotation - axis.y, minX, maxX);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    public void OnLockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        isLockMouse = true;
    }
    public void OnUnlockMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        isLockMouse = false;
    }
}