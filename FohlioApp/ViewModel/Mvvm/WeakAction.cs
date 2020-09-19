using System;
using System.Reflection;

namespace Fohlio.RevitReportsIntegration.ViewModel.Mvvm
{
    public class WeakAction
    {
        private Action staticAction;

        protected WeakAction()
        {
        }

        public WeakAction(Action action)
            : this(action.Target, action)
        {
        }

        public virtual string MethodName => staticAction?.Method.Name ?? Method.Name;

        public bool IsStatic => staticAction != null;

        public virtual bool IsAlive
        {
            get
            {
                if (staticAction == null && Reference == null)
                    return false;

                if (staticAction != null && Reference == null)
                    return true;

                return Reference.IsAlive;
            }
        }

        public object Target => Reference?.Target;

        protected MethodInfo Method { get; set; }

        protected WeakReference ActionReference { get; set; }

        protected WeakReference Reference { get; set; }

        protected object ActionTarget => ActionReference?.Target;

        public WeakAction(object target, Action action)
        {
            if (action.Method.IsStatic)
            {
                staticAction = action;
                if (target == null)
                    return;
                Reference = new WeakReference(target);
            }
            else
            {
                Method = action.Method;
                ActionReference = new WeakReference(action.Target);
                Reference = new WeakReference(target);
            }
        }

        public virtual void Execute()
        {
            if (staticAction != null)
                staticAction();
            else
            {
                if (!IsAlive || !(Method != null) || (ActionReference == null || ActionTarget == null))
                    return;
                Method.Invoke(ActionTarget, null);
            }
        }

        public virtual void MarkForDeletion()
        {
            Reference = null;
            ActionReference = null;
            Method = null;
            staticAction = null;
        }
    }

    public class WeakAction<T> : WeakAction, IExecuteWithObject
    {
        private Action<T> staticAction;

        public WeakAction(Action<T> action)
            : this(action.Target, action)
        {
        }

        public WeakAction(object target, Action<T> action)
        {
            if (action.Method.IsStatic)
            {
                staticAction = action;
                if (target == null)
                    return;
                Reference = new WeakReference(target);
            }
            else
            {
                Method = action.Method;
                ActionReference = new WeakReference(action.Target);
                Reference = new WeakReference(target);
            }
        }

        public override string MethodName => staticAction?.Method.Name ?? Method.Name;

        public override bool IsAlive
        {
            get
            {
                if (staticAction == null && Reference == null)
                    return false;

                if (staticAction == null)
                    return Reference.IsAlive;

                return Reference == null || Reference.IsAlive;
            }
        }

        public override void Execute() => Execute(default(T));

        public void Execute(T parameter)
        {
            if (staticAction != null)
                staticAction(parameter);
            else
            {
                if (!IsAlive || !(Method != null) || ActionReference == null)
                    return;

                Method.Invoke(ActionTarget, new object[]
                    {
                        parameter
                    });
            }
        }

        public void ExecuteWithObject(object parameter) => Execute((T)parameter);

        public override void MarkForDeletion()
        {
            staticAction = null;

            base.MarkForDeletion();
        }
    }
}