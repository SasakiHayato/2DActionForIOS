using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTrees
{
    public interface IBehaviorTree
    {
        void Set(IAction action);
    }

    public interface IConditional
    {
        bool Check();
    }

    public interface IAction
    {
        void Action();
        bool End();
        bool Reset { set; }
    }

    public class BehaviorTree : MonoBehaviour
    {
        enum State
        {
            Running,
            Setting,

            Start,
            End,

            None,
        }

        State _state = State.None;

        [SerializeReference, SubclassSelector] List<IConditional> Conditional;
        [SerializeReference, SubclassSelector] List<IAction> Action;

        void Start()
        {
            if (Conditional.Count != Action.Count)
                Debug.LogError("IConditional ‚Æ IAction ‚ª“¯‚¶‚Å‚ ‚è‚Ü‚¹‚ñ");
        }

        SequenceNode _sqN;
        SelectorNode _stN;
        ActionNode _aN;

        public void Repeater<T>(T set) where T : IBehaviorTree
        {
            if (_state == State.Running || _state == State.Start)
            {
                _aN.Set(set, Action[_sqN.ActionId], ref _state);
                if (_state == State.End) return;
                _sqN.Run(ref _state);
            }
            else if (_state == State.End || _state == State.None)
            {
                _sqN = new SequenceNode();
                _stN = new SelectorNode();
                _aN = new ActionNode();
                
                _state = State.Setting;
            }
            else
                _stN.Set(Conditional, _sqN, ref _state);
        }

        class SelectorNode
        {
            public void Set(List<IConditional> cList, SequenceNode sqN,ref State state)
            {
                ConditionalNade cN = new ConditionalNade();
                IConditional result = cN.Set(cList);
                if (result == null)
                {
                    state = State.None;
                    return;
                }
                else sqN.Set(result, cN.SetId,ref state);
            }
        }

        class SequenceNode
        {
            IConditional _c;

            public int ActionId { get; private set; } = 0;

            public void Set(IConditional c, int id,ref State state)
            {
                ActionId = id;
                _c = c;
                state = State.Start;
            }
            public void Run(ref State state)
            {
                if (_c == null) state = State.None;
                
                if (_c.Check()) state = State.Running;
                else
                {
                    _c = null;
                    state = State.None;
                }
            }

            public void Reset()
            {
                _c = null;
                ActionId = 0;
            }
        }

        class ConditionalNade
        {
            public int SetId { get; private set; } = 0;

            public IConditional Set(List<IConditional> cList)
            {
                IConditional set = null;
                bool check = false;
                int index = 0;
                cList.ForEach(c =>
                {
                    bool result = c.Check();
                    if (result && !check)
                    {
                        check = true;
                        set = cList[index];
                        SetId = index;
                    }
                    else
                    {
                        index++;
                    }
                });

                return set;
            }
        }

        class ActionNode
        {
            public void Set(IBehaviorTree ai, IAction a,ref State state)
            {
                ai.Set(a);
                if (a.End())
                {
                    a.Reset = false;
                    state = State.End;
                }
            }
        }
    }
}

