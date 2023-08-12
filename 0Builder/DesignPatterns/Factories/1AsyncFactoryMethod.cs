using System;
using System.Threading.Tasks;

namespace Factories
{
    public class AsyncFactoryMethod
    {
        public class Foo
        {
            private Foo()
            {
            }

            public static Task<Foo> CreateAsync()
            {
                var result = new Foo();
                return result.InitAsync();
            }

            private async Task<Foo> InitAsync()
            {
                await Task.Delay(1000);
                return this;
            }
        }

        public async Task Main(string[] args)
        {
            // var foo = new Foo();
            // await foo.InitAsync();

            Foo x = await Foo.CreateAsync();
        }
    }
}