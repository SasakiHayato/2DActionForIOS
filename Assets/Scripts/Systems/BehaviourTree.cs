using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BehaviourTree : MonoBehaviour
{
    [SerializeReference, SubclassSelector]
    List<IConditional> m_conditions = new List<IConditional>();

    public void Repeator()
    {

    }
}

class Selector
{
    public void Select(List<IConditional> iCondition)
    {
        Conditional m_condition = new Conditional();
        m_condition.Or(iCondition);
    }
}

class Conditional
{
    public bool Or(List<IConditional> iCondition)
    {
        bool check = iCondition.Any(e => e.Request());
        return check;
    }

    public void And()
    {

    }
}

