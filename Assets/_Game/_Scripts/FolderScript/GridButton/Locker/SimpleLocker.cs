public class SimpleLocker : LockButton
{
    public override void TryOffLocker()
    {
        locked = false;
    }
}