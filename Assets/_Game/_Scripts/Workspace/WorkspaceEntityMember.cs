using Sirenix.OdinInspector;
using UnityEngine;

public class WorkspaceEntityMember : MonoBehaviour
{
    #region Prefab

    [SerializeField, FoldoutGroup("Prefab")]
    private MytIcon iconModelPrefab;

    [SerializeField, FoldoutGroup("Prefab")]
    private MytFolder folderModelPrefab;

    #endregion

    #region Link

    [SerializeField, FoldoutGroup("Link")] private MytIcon myIcon;
    [SerializeField, FoldoutGroup("Link")] private MytFolder myFolder;

    public MytIcon MyIcon => myIcon;

    public MytFolder MyFolder => myFolder;

    #endregion

    [SerializeField] private ChildSystem childSystem;

    public ChildSystem ChildSystem
    {
        get
        {
            return childSystem;
        }
    }

    public WorkspaceEntityMember()
    {
        childSystem = new ChildSystem() { my = this };
    }

    public void Set(string key, string value, WorkspaceEntityMember parent, IUpdaterValue updaterValue = null)
    {
        gameObject.name = key;
        childSystem.my = this;
        childSystem.parent = parent;
        CreateIcon(value, updaterValue, parent.childSystem.my.myFolder.ParentIcon);
    }

    void CreateIcon(string value, IUpdaterValue updaterValue, Transform transform)
    {
        myIcon = Instantiate(iconModelPrefab, transform);
        SetMytCore(myIcon, value, updaterValue);
    }

    public void CreateMain(string value, IUpdaterValue updaterValue = null)
    {
        CreateMain(value, updaterValue, childSystem.parent.myFolder.ParentFolder);
    }

    void CreateMain(string value, IUpdaterValue updaterValue, Transform transform)
    {
        myFolder = Instantiate(folderModelPrefab, transform);
        SetMytCore(myFolder, value, updaterValue);
    }

    private void SetMytCore(MytCore mytCore, string value, IUpdaterValue updaterValue)
    {
        mytCore.SetText(value);
        mytCore.SetUpdater(updaterValue);
        mytCore.SetWorkspaceEntityMember(this);
        mytCore.Set();
    }

    private void CreateChild(string value, IUpdaterValue updaterValue)
    {
        CreateIcon(value, updaterValue, myFolder.ParentIcon);
    }
}