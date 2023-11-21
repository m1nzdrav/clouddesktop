

internal class Singleton<TSingleton> where TSingleton:ISingleton
 {
    public void Register(TSingleton singleton) 
    {
        singleton?.CheckRegister();
    }
    
}