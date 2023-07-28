using UnityEngine.Events;
using System.Collections.Generic;
public interface IGroundBehaviour
{
    UnityEvent OnFinishGame { get; }

    public void SetAllowList(List<string> allowList);
}