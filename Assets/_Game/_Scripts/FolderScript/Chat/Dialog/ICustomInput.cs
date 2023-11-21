
using System.Collections.Generic;

public interface ICustomInput : IAnswer
{
     void Set(SecondChatModel secondChatModel,ErrorText errorText);
     void Set(SecondChatModel secondChatModel,List<string> variant,ErrorText errorText);
     List<string> GetAnswer();
}