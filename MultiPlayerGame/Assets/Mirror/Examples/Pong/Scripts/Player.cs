using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Mirror.Examples.Pong
{
    public class Player : NetworkBehaviour
    {
        public float speed = 30;
        public Rigidbody2D rigidbody2d;
        private Button Upbtn;
        private Button Downbtn;
        private float verticalInput = 0;

        // need to use FixedUpdate for rigidbody
        void FixedUpdate()
        {
            // only let the local player control the racket.
            // don't control other player's rackets
            if (isLocalPlayer)
            {
                float input = Input.GetAxisRaw("Vertical") + verticalInput;
#if UNITY_6000_0_OR_NEWER
                rigidbody2d.linearVelocity = new Vector2(0, input) * speed * Time.fixedDeltaTime;
#else
                rigidbody2d.velocity = new Vector2(0, input) * speed * Time.fixedDeltaTime;
#endif
            }
        }
        private void Start()
        {
            // Find Buttons
            Upbtn = GameObject.Find("UpButton").GetComponent<Button>();
            Downbtn = GameObject.Find("DownButton").GetComponent<Button>();

            // Add EventTriggers for Up Button
            EventTrigger upTrigger = Upbtn.gameObject.AddComponent<EventTrigger>();

            // PointerDown for pressing up
            EventTrigger.Entry upPressEntry = new EventTrigger.Entry();
            upPressEntry.eventID = EventTriggerType.PointerDown;
            upPressEntry.callback.AddListener((eventData) => { OnPressUp(); });
            upTrigger.triggers.Add(upPressEntry);

            // PointerUp for releasing up
            EventTrigger.Entry upReleaseEntry = new EventTrigger.Entry();
            upReleaseEntry.eventID = EventTriggerType.PointerUp;
            upReleaseEntry.callback.AddListener((eventData) => { OnRelease(); });
            upTrigger.triggers.Add(upReleaseEntry);

            // Add EventTriggers for Down Button
            EventTrigger downTrigger = Downbtn.gameObject.AddComponent<EventTrigger>();

            // PointerDown for pressing down
            EventTrigger.Entry downPressEntry = new EventTrigger.Entry();
            downPressEntry.eventID = EventTriggerType.PointerDown;
            downPressEntry.callback.AddListener((eventData) => { OnPressDown(); });
            downTrigger.triggers.Add(downPressEntry);

            // PointerUp for releasing down
            EventTrigger.Entry downReleaseEntry = new EventTrigger.Entry();
            downReleaseEntry.eventID = EventTriggerType.PointerUp;
            downReleaseEntry.callback.AddListener((eventData) => { OnRelease(); });
            downTrigger.triggers.Add(downReleaseEntry);
        }
        public void OnPressUp()
        {
            verticalInput = 1;
        }

        public void OnPressDown()
        {
            verticalInput = -1;
        }

        public void OnRelease()
        {
            verticalInput = 0;
        }
    }
}
