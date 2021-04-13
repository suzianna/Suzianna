using System;
using System.Collections.Generic;
using System.Text;
using NFluent.Helpers;
using OpenQA.Selenium;
using Suzianna.Core.Functional;
using Suzianna.Core.Screenplay.Actors;

namespace Suzianna.WebDriver.Screenplay.Targets
{
    public abstract class Target
    {
        protected string TargetElementName { get; private set; }
        protected Maybe<TimeSpan> Timeout { get; private set; } 
        protected Target(string targetElementName) : this(targetElementName, Maybe<TimeSpan>.None) { }
        protected Target(string targetElementName, Maybe<TimeSpan> timeout)
        {
            this.TargetElementName = targetElementName;
            this.Timeout = timeout;
        }
        public static TargetBuilder The(string targetElementName)
        {
            return new TargetBuilder(targetElementName);
        }
        public abstract IWebElement ResolveFor(Actor actor);

        //public abstract List<WebElementFacade> resolveAllFor(Actor actor);

        //public abstract Target called(String name);

        //public abstract Target of(String...parameters);

        //public abstract String getCssOrXPathSelector();

        //public abstract Target waitingForNoMoreThan(Duration timeout);
    }
}
