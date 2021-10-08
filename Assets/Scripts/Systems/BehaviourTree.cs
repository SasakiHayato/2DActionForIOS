using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using IEnemysBehaviour;

public class BehaviourTree : MonoBehaviour
{
    [SerializeReference, SubclassSelector]
    List<IConditional> m_conditions = new List<IConditional>();

    [SerializeReference, SubclassSelector]
    List<IBehaviourAction> m_actions = new List<IBehaviourAction>();

    public void Repeator()
    {
        SelectorNode selector = new SelectorNode();
        selector.Select(m_conditions, m_actions);
    }
}

class SelectorNode
{
    public void Select(List<IConditional> iCondition, List<IBehaviourAction> actions)
    {
        ConditionalNode condition = new ConditionalNode();
        SequenceNode sequence = new SequenceNode();

        condition.Or(iCondition);
        sequence.Sequence(condition, actions);
    }
}

class ConditionalNode
{
    IConditional m_set;
    public bool Or(List<IConditional> iCondition)
    {
        bool check = iCondition.Any(e => e.Request());

        return check;
    }

    public bool And()
    {
        bool check = m_set.Request();
        return check;
    }

    public List<ActionType> GetAnswer(List<IBehaviourAction> actions)
    {
        List<ActionType> answer = m_set.Answer();
        return answer;
    }
}

class SequenceNode
{
    public void Sequence(ConditionalNode condition, List<IBehaviourAction> actions)
    {
        ActionNode action = new ActionNode();
        bool check = condition.And();
        if (check)
        {
            List<ActionType> get = condition.GetAnswer(actions);
            action.Set(actions, get);
        }
    }
}

class ActionNode
{
    public void Set(List<IBehaviourAction> actions, List<ActionType> types)
    {

    }
}

