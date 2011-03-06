// Type: System.Web.HttpContextBase
// Assembly: System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// Assembly location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\System.Web.dll

using System;
using System.Collections;
using System.Globalization;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Web.Caching;
using System.Web.Profile;
using System.Web.SessionState;

namespace System.Web
{
    [TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
    public abstract class HttpContextBase : IServiceProvider
    {
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        protected HttpContextBase();

        public virtual Exception[] AllErrors { get; }
        public virtual HttpApplicationStateBase Application { get; }
        public virtual HttpApplication ApplicationInstance { get; set; }
        public virtual Cache Cache { get; }
        public virtual IHttpHandler CurrentHandler { get; }
        public virtual RequestNotification CurrentNotification { get; }
        public virtual Exception Error { get; }
        public virtual IHttpHandler Handler { get; set; }
        public virtual bool IsCustomErrorEnabled { get; }
        public virtual bool IsDebuggingEnabled { get; }
        public virtual bool IsPostNotification { get; }
        public virtual IDictionary Items { get; }
        public virtual IHttpHandler PreviousHandler { get; }
        public virtual ProfileBase Profile { get; }
        public virtual HttpRequestBase Request { get; }
        public virtual HttpResponseBase Response { get; }
        public virtual HttpServerUtilityBase Server { get; }
        public virtual HttpSessionStateBase Session { get; }
        public virtual bool SkipAuthorization { get; set; }
        public virtual DateTime Timestamp { get; }
        public virtual TraceContext Trace { get; }
        public virtual IPrincipal User { get; set; }

        #region IServiceProvider Members

        public virtual object GetService(Type serviceType);

        #endregion

        public virtual void AddError(Exception errorInfo);
        public virtual void ClearError();
        public virtual object GetGlobalResourceObject(string classKey, string resourceKey);
        public virtual object GetGlobalResourceObject(string classKey, string resourceKey, CultureInfo culture);
        public virtual object GetLocalResourceObject(string virtualPath, string resourceKey);
        public virtual object GetLocalResourceObject(string virtualPath, string resourceKey, CultureInfo culture);
        public virtual object GetSection(string sectionName);
        public virtual void RemapHandler(IHttpHandler handler);
        public virtual void RewritePath(string path);
        public virtual void RewritePath(string path, bool rebaseClientPath);
        public virtual void RewritePath(string filePath, string pathInfo, string queryString);
        public virtual void RewritePath(string filePath, string pathInfo, string queryString, bool setClientFilePath);
        public virtual void SetSessionStateBehavior(SessionStateBehavior sessionStateBehavior);
    }
}
