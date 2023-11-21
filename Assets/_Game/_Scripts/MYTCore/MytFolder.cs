using Sirenix.OdinInspector;
using UnityEngine;

public class MytFolder : MytCore
{
    [SerializeField, FoldoutGroup("Component link")]
    private MytPrefabColorTypes mytPrefabColorTypes;

    [SerializeField, FoldoutGroup("Component link")]
    private TopPanelSetParametrs topPanelSetParametrs;

    [SerializeField, FoldoutGroup("Parent Transform")]
    private Transform parentIcon;

    [SerializeField, FoldoutGroup("Parent Transform")]
    private Transform parentFolder;


    public Transform ParentIcon => parentIcon;

    public Transform ParentFolder => parentFolder;

    public override void Set()
    {
        topPanelSetParametrs.SetParametrs(gameObject, myWorkspaceEntityMember.MyIcon.gameObject);
    }
}