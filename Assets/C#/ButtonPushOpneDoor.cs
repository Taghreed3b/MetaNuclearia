using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonPushOpenDoor : MonoBehaviour
{
    public GameObject door; // مرجع للباب
    private bool isOpen = false;
    public float moveDistance = 2.0f; // المسافة التي يتحرك بها الباب
    public float moveSpeed = 1.0f; // سرعة تحرك الباب
    private Vector3 closedPosition;
    private Vector3 openPosition;

    [SerializeField]
    private AudioSource doorAudioSource; // مصدر الصوت للباب

    [SerializeField]
    private GameObject objectBeforeMoving; // الكائن الذي سيكون نشطًا قبل تحرك الباب

    [SerializeField]
    private GameObject objectAfterMoving; // الكائن الذي سيكون نشطًا بعد تحرك الباب

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<XRSimpleInteractable>().selectEntered.AddListener(x => ToggleDoorOpen());

        if (door != null)
        {
            closedPosition = door.transform.position;
            openPosition = new Vector3(closedPosition.x + moveDistance, closedPosition.y, closedPosition.z);
        }

        // تفعيل الكائن الأول وإيقاف الثاني في بداية اللعبة
        if (objectBeforeMoving != null) objectBeforeMoving.SetActive(true);
        if (objectAfterMoving != null) objectAfterMoving.SetActive(false);
    }

    void Update()
    {
        if (door != null)
        {
            if (isOpen)
            {
                // تحرك الباب للفتح
                door.transform.position = Vector3.MoveTowards(door.transform.position, openPosition, moveSpeed * Time.deltaTime);
            }
            else
            {
                // تحرك الباب للإغلاق
                door.transform.position = Vector3.MoveTowards(door.transform.position, closedPosition, moveSpeed * Time.deltaTime);
            }
        }
    }

    public void ToggleDoorOpen()
    {
        isOpen = !isOpen;
        if (doorAudioSource != null && !doorAudioSource.isPlaying)
        {
            doorAudioSource.Play(); // تشغيل الصوت
        }

        // تبديل حالة الكائنات بناءً على حالة الباب
        if (objectBeforeMoving != null) objectBeforeMoving.SetActive(!isOpen);
        if (objectAfterMoving != null) objectAfterMoving.SetActive(isOpen);
    }
}
