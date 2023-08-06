using System;

namespace BuilderPatterns;

public class FacetedBuilder
{
    public class Person
    {
        // address
        public string StreetAddress, PostCode, City;
        
        // employment
        public string CompanyName, Position;
        public int AnnualIncome;

        public override string ToString()
        {
            return $"{nameof(CompanyName)}: {CompanyName}," +
                   $" {nameof(Position)}: {Position}," +
                   $" {nameof(AnnualIncome)}: {AnnualIncome}," +
                   $" {nameof(StreetAddress)}: {StreetAddress}," +
                   $" {nameof(PostCode)}: {PostCode}," +
                   $" {nameof(City)}: {City}";
        }
    }

    public class PersonBuilder // facade
    {
        // reference!
        protected Person person = new();

        public PersonJobBuilder Works => new PersonJobBuilder(person);
        public PersonAddressBuilder Lives => new PersonAddressBuilder(person);

        public static implicit operator Person(PersonBuilder pb)
        {
            return pb.person;
        }
    }

    public class PersonAddressBuilder : PersonBuilder
    {
        public PersonAddressBuilder(Person person)
        {
            this.person = person;
        }
        
        public PersonAddressBuilder At(string streetAddress)
        {
            person.StreetAddress = streetAddress;
            return this;
        }

        public PersonAddressBuilder WithPostCode(string postCode)
        {
            person.PostCode = postCode;
            return this;
        }

        public PersonAddressBuilder In(string city)
        {
            person.City = city;
            return this;
        }
    }
  
    public class PersonJobBuilder : PersonBuilder
    {
        public PersonJobBuilder(Person person)
        {
            this.person = person;
        }
        
        public PersonJobBuilder At(string companyName)
        {
            person.CompanyName = companyName;
            return this;
        }

        public PersonJobBuilder AsA(string position)
        {
            person.Position = position;
            return this;
        }

        public PersonJobBuilder Earning(int amount)
        {
            person.AnnualIncome = amount;
            return this;
        }
    }

    public static void Main(string[] args)
    {
        var pb = new PersonBuilder();
        Person person = pb.Works.At("Verion").AsA("Engineer").Earning(10000).Lives.At("Rua Arnaldo Carvalho").WithPostCode("11075").In("Santos");
        Console.WriteLine(person.ToString());
    }
}