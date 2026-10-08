using UnityEngine;
using UnityEngine.InputSystem;

//===========
//戦車のコントロールクラス
//===========

public class Tank : MonoBehaviour
{
  // === Serialize FIeld ===
    [Header("=== Tank Joints ===")]
    [SerializeField] private Transform topJoint;  //上部のジョイントの参照
    [SerializeField] private Transform cannonJoint;  //砲身のジョイントの参照

    [Header("== 弾丸設定 ===")]
    [SerializeField] private GameObject bulletPrefab;   //弾丸のプレハブ
    [SerializeField] private Transform shotPoint;       //弾丸の発射位置

    private Vector3 topAngles = Vector3.zero;       //上部のジョイントの角度
    private Vector3 cannonAngles = Vector3.zero;    //砲身のジョイントの角度
  
    void Start()
    {
        
    }

    void Update()
    {
        if (Keyboard.current.wKey.isPressed == true)
        {
            transform.Translate(Vector3.forward * 5 * Time.deltaTime);
        }
        if (Keyboard.current.sKey.isPressed == true)
        {
            transform.Translate(Vector3.forward * -5 * Time.deltaTime);
        }
        if (Keyboard.current.aKey.isPressed == true)
        {
            transform.Rotate(Vector3.up * -90 * Time.deltaTime);
        }
        if (Keyboard.current.dKey.isPressed == true)
        {
            transform.Rotate(Vector3.up * 90 * Time.deltaTime);
        }

        //マウスの移動量を取得する
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();
        Debug.Log($"Mouse Delta: {mouseDelta}");

        //角度の増減
        topAngles.y += mouseDelta.x * 0.1f;
        cannonAngles.x -= mouseDelta.y * 0.1f;
        cannonAngles.x = Mathf.Clamp(cannonAngles.x, -10f, 30f);

        //各ジョイントに角度を反映する
        topJoint.localEulerAngles = topAngles;
        cannonJoint.localEulerAngles = cannonAngles;

        //弾丸の発射
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            //弾丸を生成する
            GameObject bullet = Instantiate(bulletPrefab, shotPoint.position, shotPoint.rotation);

            bullet.GetComponent<Rigidbody>()
                .AddForce(shotPoint.forward * 25f, ForceMode.Impulse);

            Destroy(bullet, 5f);
        }
    }
}
