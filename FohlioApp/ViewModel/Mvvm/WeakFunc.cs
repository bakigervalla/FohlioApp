using System;
using System.Reflection;

namespace Fohlio.RevitReportsIntegration.ViewModel.Mvvm
{
    public class WeakFunc<TResult>
    {
        private Func<TResult> staticFunc;

        protected MethodInfo Method { get; set; }

        public bool IsStatic => staticFunc != null;

        public virtual string MethodName
        {
            get
            {
                if (staticFunc != null)
                    return staticFunc.Method.Name;
                return Method.Name;
            }
        }

        protected WeakReference FuncReference { get; set; }

        protected WeakReference Reference { get; set; }

        public virtual bool IsAlive
        {
            get
            {
                if (staticFunc == null && Reference == null)
                    return false;
                if (staticFunc != null && Reference == null)
                    return true;
                return Reference.IsAlive;
            }
        }

        public object Target => Reference?.Target;

        protected object FuncTarget => FuncReference?.Target;

        protected WeakFunc()
        {
        }

        public WeakFunc(Func<TResult> func)
            : this(func.Target, func)
        {
        }

        public WeakFunc(object target, Func<TResult> func)
        {
            if (func.Method.IsStatic)
            {
                staticFunc = func;
                if (target == null)
                    return;
                Reference = new WeakReference(target);
            }
            else
            {
                Method = func.Method;
                FuncReference = new WeakReference(func.Target);
                Reference = new WeakReference(target);
            }
        }

        public virtual TResult Execute()
        {
            if (staticFunc != null)
                return staticFunc();

            if (IsAlive && Method != null && FuncReference != null)
                return (TResult)Method.Invoke(FuncTarget, null);

            return default(TResult);
        }

        public virtual void MarkForDeletion()
        {
            Reference = null;
            FuncReference = null;
            Method = null;
            staticFunc = null;
        }
    }

    public class WeakFunc<T, TResult> : WeakFunc<TResult>, IExecuteWithObjectAndResult
    {
        private Func<T, TResult> staticFunc;

        public WeakFunc(Func<T, TResult> func)
            : this(func.Target, func)
        {
        }

        public WeakFunc(object target, Func<T, TResult> func)
        {
            if (func.Method.IsStatic)
            {
                staticFunc = func;
                if (target == null)
                    return;
                Reference = new WeakReference(target);
            }
            else
            {
                Method = func.Method;
                FuncReference = new WeakReference(func.Target);
                Reference = new WeakReference(target);
            }
        }

        public override string MethodName => staticFunc?.Method.Name ?? Method.Name;

        public override bool IsAlive
        {
            get
            {
                if (staticFunc == null && Reference == null)
                    return false;
                if (staticFunc == null)
                    return Reference.IsAlive;
                if (Reference != null)
                    return Reference.IsAlive;
                return true;
            }
        }

        public override TResult Execute()
        {
            return Execute(default(T));
        }

        public TResult Execute(T parameter)
        {
            if (staticFunc != null)
                return staticFunc(parameter);

            if (!IsAlive || !(Method != null) || FuncReference == null)
                return default(TResult);

            return (TResult) Method.Invoke(FuncTarget, new object[]
                {
                    parameter
                });
        }

        public object ExecuteWithObject(object parameter) => Execute((T)parameter);

        public override void MarkForDeletion()
        {
            staticFunc = null;

            base.MarkForDeletion();
        }
    }

}