using System;
using System.Linq;

namespace FuncPtrTests
{
    class Program
    {
        static void Main(string[] args)
        {
            Test2();
        }

        private struct Foo
        {
            private readonly int Yes;

            public Foo(int yes)
            {
                Yes = yes;
            }
            
            public void Print(string String)
            {
                Console.WriteLine(String);
            }
            
            public static void PrintRef(ref Foo Foo, string String)
            {
                Console.WriteLine($"{Foo.Yes} | {String}");
            }
            
            public static unsafe void PrintPtr(Foo* Foo, string String)
            {
                Console.WriteLine($"{Foo->Yes} | {String}");
            }
        }
        
        static unsafe void Test2()
        {
            //var Method = typeof(Foo).GetMethod("PrintRef", BindingFlags.Public | BindingFlags.NonPublic);

            var Method = typeof(Foo).GetMethods().First(x => x.IsStatic && x.Name == "PrintRef");
            
            var Yes = (delegate*<ref Foo, string, void>) Method!.MethodHandle.GetFunctionPointer();
            
            var Foo = new Foo(69);

            Yes(ref Foo, "Fuck you");
        }
    }
}