using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIBaseStateClass : MonoBehaviour
{
    public GhostsAIController ghost;
   
    public virtual void UpdateLogic() { }
}
