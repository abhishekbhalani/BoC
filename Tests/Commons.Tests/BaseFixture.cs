using System;
using System.Collections.Generic;
using BoC.InversionOfControl;
using BoC.UnitOfWork;
using Moq;

namespace BoC.Tests
{
    public abstract class BaseIoCFixture : IDisposable
    {
        protected readonly Mock<TestableDependencyResolver> resolver;

        protected BaseIoCFixture()
        {
            resolver = new Mock<TestableDependencyResolver>();
            resolver.CallBase = true;
        }
        
        protected Mock<T> SetupResolve<T>() where T : class
        {
            var repository = new Mock<T>();
            resolver.Setup(r => r.Resolve<T>()).Returns(repository.Object);
            return repository;
        }

        public virtual void Dispose()
        {
            resolver.Object.Dispose();
        }
    }

    public class TestableDependencyResolver: IDependencyResolver
    {
        public virtual void Dispose()
        {
            
        }

        public virtual void RegisterInstance<T>(T instance)
        {
            
        }

        public void RegisterSingleton<TFrom, TTo>() where TTo : TFrom
        {
            RegisterSingleton(typeof(TFrom), typeof(TTo));
        }

        public virtual void RegisterSingleton(Type from, Type to)
        {
        }

        public void RegisterType<TFrom, TTo>() where TTo : TFrom
        {
            RegisterType(typeof(TFrom), typeof(TTo));
        }

        public virtual void RegisterType(Type from, Type to)
        {
        }

        public virtual void Inject<T>(T existing)
        {
        }

        public virtual object Resolve(Type type)
        {
            return null;
        }

        public virtual object Resolve(Type type, string name)
        {
            return null;
        }

        public T Resolve<T>()
        {
            var result = Resolve(typeof (T));
            if (result != null)
                return (T) result;
            return default(T);
        }

        public virtual T Resolve<T>(string name)
        {
            var result = Resolve(typeof(T), name);
            if (result != null)
                return (T)result;
            return default(T);
        }

        public virtual IEnumerable<T> ResolveAll<T>()
        {
            return null;
        }

        public virtual bool IsRegistered(Type type)
        {
            return false;
        }

        public bool IsRegistered<T>()
        {
            return IsRegistered(typeof (T));
        }
    }
}