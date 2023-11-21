using Sirenix.OdinInspector;

public class MytIcon : MytCore
{
    [Button]
    public void CreateFolder()
    {
        myWorkspaceEntityMember.CreateMain(inputField.text, updaterValue);
    }
}