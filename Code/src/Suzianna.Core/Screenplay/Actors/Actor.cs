using System;
using System.Collections.Generic;
using System.Linq;
using NFluent;
using Suzianna.Core.Events;
using Suzianna.Core.Screenplay.Actors.Events;
using Suzianna.Core.Screenplay.Questions;

namespace Suzianna.Core.Screenplay.Actors
{
    public class Actor
    {
        private readonly List<IAbility> _abilities;
        private Notepad notepad;
        public IReadOnlyList<IAbility> Abilities => _abilities;
        public string Name { get; private set; }
        public Actor(string name)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name), ExceptionFormats.ActorMustHaveAName);
            this._abilities = new List<IAbility>();
            this.notepad = new Notepad();
        }
        public static Actor Named(string name)
        {
            return new Actor(name);
        }
        public Actor Can<T>(T ability) where T : IAbility
        {
            this._abilities.Add(ability);
            return this;
        }
        public Actor Can<T>(List<T> abilities) where T : IAbility
        {
            abilities.ForEach(ability => this.Can(ability));
            return this;
        }
        public Actor WhoCan<T>(T ability) where T: IAbility
        {
            return Can(ability);
        }
        public Actor WhoCan<T>(List<T> abilities) where T : IAbility
        {
            abilities.ForEach(ability => this.WhoCan(ability));
            return this;
        }
        public T FindAbility<T>() where T : IAbility
        {
            var ability =  this._abilities.OfType<T>().FirstOrDefault();
            if (ability == null)
                throw new ActorIsUnableException<T>(this);
            return ability;
        }
        public void AttemptsTo(params IPerformable[] tasks)
        {
            Broadcaster.Publish(new ActorBeginsPerformanceEvent(this.Name));
            foreach (var performable in tasks)
            {
                performable.PerformAs(this);
            }
        }
        public T AsksFor<T>(IQuestion<T> question)
        {
            return question.AnsweredBy(this);
        }
        public ICheck<T> Should<T>(IConsequence<T> question)
        {
            return this.AsksFor(question);
        }

        public void Remember(string key, object value)
        {
            notepad.WriteDown(key,value);
        }

        public T Remember<T>(string key, IQuestion<T> question)
        {
            var answer =  question.AnsweredBy(this);
            Remember(key, answer);
            return answer;
        }

        public T Recall<T>(string key)
        {
            return notepad.Read<T>(key);
        }
    }
}
