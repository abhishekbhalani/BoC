﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Web;
using BoC.InversionOfControl;
using NHibernate;
using NHibernate.Context;
using NHibernate.Engine;

namespace BoC.Persistence.NHibernate
{
    public class AutoContextSessionContext : MapBasedSessionContext
    {
        [ThreadStatic]
        protected static IDictionary map;
        private const string SessionFactoryMapKey = "NHibernate.Context.WebSessionContext.SessionFactoryMapKey";

        public AutoContextSessionContext(ISessionFactoryImplementor factory)
            : base(factory)
        {
        }

        #region Implementation of ICurrentSessionContext

        protected override IDictionary GetMap()
        {
            if (HttpContext.Current != null)
                return HttpContext.Current.Items[SessionFactoryMapKey] as IDictionary;
            else
                return map;
        }

        protected override void SetMap(IDictionary value)
        {
            if (HttpContext.Current != null)
                HttpContext.Current.Items[SessionFactoryMapKey] = value;
            else
                map = value;
        }

        #endregion
    }
}