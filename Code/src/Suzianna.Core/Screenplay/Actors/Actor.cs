using System;
using System.Collections.Generic;
using System.Linq;
using NFluent;
using Suzianna.Core.Events;
using Suzianna.Core.Screenplay.Actors.Events;
using Suzianna.Core.Screenplay.Questions;

namespace Suzianna.Core.Screenplay.Actors
{
    /// <summary>
    ///     Represents a person, persona or system using the system under test (SUT).
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Actors are at the heart of the Screenplay pattern. Each actor has one or more Abilities. Actors can also
    ///         perform tasks by interacting with the application or asks questions about the state of the application.
    ///     </para>
    /// </remarks>
    public class Actor
    {
        private readonly List<IAbility> _abilities;
        private readonly Notepad notepad;

        /// <summary>
        /// Initializes a new instance of the <see cref="Actor" />
        /// </summary>
        /// <param name="name">Actor's name</param>
        /// <exception cref="ArgumentNullException">Thrown when name is null</exception>
        public Actor(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name), ExceptionFormats.ActorMustHaveAName);
            _abilities = new List<IAbility>();
            notepad = new Notepad();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Actor" />
        /// </summary>
        /// <param name="name">Actor's name</param>
        /// <exception cref="ArgumentNullException">Thrown when name is null</exception>
        /// <returns>an instance of actor with provided name</returns>
        public static Actor Named(string name)
        {
            return new Actor(name);
        }

        /// <summary>
        /// Gets the abilities of the actor as a read-only list.
        /// </summary>
        public IReadOnlyList<IAbility> Abilities => _abilities;

        /// <summary>
        /// Gets the name of the actor.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Adds an ability to actor
        /// </summary>
        /// <typeparam name="T">A type that inherits from the <see cref="IAbility"/> interface</typeparam>
        /// <param name="ability">ability to be added to actor</param>
        /// <returns>the actor instance which ability is added to (self)</returns>
        public Actor Can<T>(T ability) where T : IAbility
        {
            _abilities.Add(ability);
            return this;
        }

        /// <summary>
        /// Adds abilities to actor
        /// </summary>
        /// <typeparam name="T">A type that inherits from the <see cref="IAbility"/> interface</typeparam>
        /// <param name="abilities">abilities to be added to actor</param>
        /// <returns>the actor instance which ability is added to (self)</returns>
        public Actor Can<T>(List<T> abilities) where T : IAbility
        {
            abilities.ForEach(ability => Can(ability));
            return this;
        }

        /// <summary>
        /// Adds an ability to actor
        /// </summary>
        /// <typeparam name="T">A type that inherits from the <see cref="IAbility"/> interface</typeparam>
        /// <param name="ability">ability to be added to actor</param>
        /// <returns>the actor instance which ability is added to (self)</returns>
        public Actor WhoCan<T>(T ability) where T : IAbility
        {
            return Can(ability);
        }

        /// <summary>
        /// Adds abilities to actor
        /// </summary>
        /// <typeparam name="T">A type that inherits from the <see cref="IAbility"/> interface</typeparam>
        /// <param name="abilities">abilities to be added to actor</param>
        /// <returns>the actor instance which ability is added to (self)</returns>
        public Actor WhoCan<T>(List<T> abilities) where T : IAbility
        {
            abilities.ForEach(ability => WhoCan(ability));
            return this;
        }

        /// <summary>
        /// Finds an ability (by type) in actor
        /// </summary>
        /// <typeparam name="T">A type that inherits from the <see cref="IAbility"/> interface</typeparam>
        /// <exception cref="ActorIsUnableException{T}">Thrown when requested ability is not in the abilities of actor</exception>
        /// <returns>The founded ability</returns>
        public T FindAbility<T>() where T : IAbility
        {
            var ability = _abilities.OfType<T>().FirstOrDefault();
            if (ability == null)
                throw new ActorIsUnableException<T>(this);
            return ability;
        }

        public void AttemptsTo(params IPerformable[] tasks)
        {
            Broadcaster.Publish(new ActorBeginsPerformanceEvent(Name));
            foreach (var performable in tasks) performable.PerformAs(this);
            Broadcaster.Publish(new ActorEndsPerformanceEvent(Name));
        }

        public T AsksFor<T>(IQuestion<T> question)
        {
            return question.AnsweredBy(this);
        }

        public ICheck<T> Should<T>(IConsequence<T> question)
        {
            return AsksFor(question);
        }

        public void Remember(string key, object value)
        {
            notepad.WriteDown(key, value);
        }

        public T Remember<T>(string key, IQuestion<T> question)
        {
            var answer = question.AnsweredBy(this);
            Remember(key, answer);
            return answer;
        }

        public T Recall<T>(string key)
        {
            return notepad.Read<T>(key);
        }

        public bool CanRecall(string key)
        {
            return notepad.HasWroteDown(key);
        }
    }
}