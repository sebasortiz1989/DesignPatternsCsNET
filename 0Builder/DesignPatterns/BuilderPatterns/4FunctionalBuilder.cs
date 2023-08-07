using System;
using System.Collections.Generic;
using System.Linq;

namespace BuilderPatterns
{
    public class FunctionalBuilder
    {
        public class Person
        {
            public string Name, Position;
        }

        public sealed class PersonBuilder : FunctionalBuilder<Person, PersonBuilder>
        {
            public PersonBuilder Called(string name) => Do(p => p.Name = name);
        }

        public void Main(string[] args)
        {
            var person = new PersonBuilder().Called("Sarah").WorksAs("Engineer").Build();
        }
    }
    
    public static class PersonBuilderExtensions
    {
        public static FunctionalBuilder.PersonBuilder WorksAs(this FunctionalBuilder.PersonBuilder builder, string position)
        {
            return builder.Do(p => p.Position = position);
        }
    }
    
    public abstract class FunctionalBuilder<TSubject, TSelf>
        where TSelf : FunctionalBuilder<TSubject, TSelf>
        where TSubject : new()
    {
        private readonly List<Func<FunctionalBuilder.Person, FunctionalBuilder.Person>> _actions = new();

        public TSelf Do(Action<FunctionalBuilder.Person> action) => AddAction(action);
            
        public FunctionalBuilder.Person Build()
        {
            return _actions.Aggregate(new FunctionalBuilder.Person(), (p, f) => f(p));
        }
            
        private TSelf AddAction(Action<FunctionalBuilder.Person> action)
        {
            _actions.Add(p =>
            {
                action(p);
                return p;
            });
            
            return (TSelf)this;
        }        
    }
}

// public sealed class PersonBuilder
// {
//     private readonly List<Func<Person, Person>> actions = new();
//
//     public PersonBuilder Called(string name) => Do(p => p.Name = name);
//
//     public PersonBuilder Do(Action<Person> action) => AddAction(action);
//
//     public Person Build()
//     {
//         return actions.Aggregate(new Person(), (p, f) => f(p));
//     }
//     
//     private PersonBuilder AddAction(Action<Person> action)
//     {
//         actions.Add(p =>
//         {
//             action(p);
//             return p;
//         });
//
//         return this;
//     }
// }