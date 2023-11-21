using _Game._Scripts;
using _Game._Scripts.Events.StartEnd;
using DG.Tweening;
using UnityEngine;

public class MoveForCursor : SubscribeFirstSecond
{
    // [SerializeField] private Transform objToMove;
    [SerializeField] private float speedMoving = .5f;
    [SerializeField] private float timeToScale = .5f;
    [SerializeField] private Camera camera;

    private void Start()
    {
        camera = Camera.main;
        AddMethodToListener(StartEvent,EndEvent );
        RegisterEvent((RegisterSingleton._instance.GetSingleton(typeof(CursorMoveChecker)) as CursorMoveChecker));
    }


    public void EndEvent()
    {
        transform.DOScale(Vector3.one, timeToScale);
    }

    public void StartEvent()
    {
        Vector3 mousePosition =
            new Vector3(Input.mousePosition.x, Input.mousePosition.y,
                transform.position.z); // переменной записываються координаты мыши по иксу и игрику


        Vector3
            objPosition =
                camera.ScreenToWorldPoint(
                    mousePosition); // переменной - объекту присваиваеться переменная с координатами мыши

        transform.DOMove(objPosition, speedMoving);
        transform.DOScale(Config.HALF_VECTOR_ONE / 2, timeToScale);
        // transform.position = Vector3.Lerp(transform.position, objPosition, .5f);
        // transform.position = objPosition; // и собственно объекту записываються координаты
    }
}