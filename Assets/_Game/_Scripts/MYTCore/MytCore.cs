using UnityEngine;
using UnityEngine.UI;


public class MytCore : MonoBehaviour
{
    [SerializeField] protected WorkspaceEntityMember myWorkspaceEntityMember;
    [SerializeField] protected InputField inputField;
    [SerializeField] protected Image image;
    protected IUpdaterValue updaterValue;

    private void Start()
    {
        inputField.onValueChanged.AddListener(UpdateText);
    }

    public void SetUpdater(IUpdaterValue updaterValue)
    {
        this.updaterValue = updaterValue;
    }

    public void SetText(string text)
    {
        inputField.SetTextWithoutNotify(text);
        gameObject.name = text;
    }

    public void SetSprite(Sprite sprite)
    {
        image.sprite = sprite;
    }

    public void SetColor(Color color)
    {
        image.color = color;
    }

    public void SetWorkspaceEntityMember(WorkspaceEntityMember workspaceEntityMember)
    {
        myWorkspaceEntityMember = workspaceEntityMember;
    }

    private void UpdateText(string test)
    {
        // RegisterSingleton
    }

    public virtual void Set()
    {
    }
}