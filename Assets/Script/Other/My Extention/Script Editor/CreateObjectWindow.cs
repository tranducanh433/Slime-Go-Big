using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
namespace MyExtension.MenuPlugins
{
    public class CreateObjectWindow : EditorWindow
    {
        KindOfMovement kindOfMovement;
        Sprite objectImage;
        string objectName = "New Object";
        bool twoBoxCollider = true;

        [MenuItem("GameObject/Create Other/Create Game Object")]
        public static void ShowWindow()
        {
            GetWindow<CreateObjectWindow>("Create Object Window");
        }
        private void OnEnable()
        {
            objectImage = Resources.Load<Sprite>("DefaultObjectSprite");
        }
        private void OnGUI()
        {
            kindOfMovement = (KindOfMovement)EditorGUILayout.EnumPopup("Kind Of Game", kindOfMovement);
            objectName = EditorGUILayout.TextField("Object Name", objectName);
            objectImage = EditorGUILayout.ObjectField("Object Image", objectImage, typeof(Sprite), true) as Sprite;
            twoBoxCollider = EditorGUILayout.Toggle("Two Box Collider", twoBoxCollider);

            if (GUILayout.Button("Create"))
            {
                CreateCharacterGameObject();
                Close();
            }
        }



        public void CreateCharacterGameObject()
        {
            //Spawn New Object
            GameObject parentObj = new GameObject();
            GameObject spriteObj = new GameObject();

            if (objectName == "New Object")
            {
                parentObj.name = objectImage.name;
                spriteObj.name = objectImage.name + " Sprite";
            }
            else
            {
                parentObj.name = objectName;
                spriteObj.name = objectName + " Sprite";
            }

            spriteObj.transform.parent = parentObj.transform;
            //Add Component to sprite object
            SpriteRenderer sr = spriteObj.AddComponent<SpriteRenderer>();
            sr.sprite = objectImage;
            spriteObj.AddComponent<BoxCollider2D>();
            if (twoBoxCollider)
            {
                BoxCollider2D bc2d = spriteObj.AddComponent<BoxCollider2D>();
                bc2d.isTrigger = true;
            }
            spriteObj.AddComponent<Animator>();
            spriteObj.AddComponent<AudioSource>();

            //Add componemt to parent object
            Rigidbody2D rb = parentObj.AddComponent<Rigidbody2D>();

            //Setting The Object
            rb.freezeRotation = true;
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

            if(kindOfMovement == KindOfMovement.topDown)
            {
                rb.gravityScale = 0;
            }
        }
    }
}
#endif