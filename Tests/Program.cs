using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.async_await;

namespace Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            Task t = (new MyClass()).DoWorkAsync();
            t.Wait();
        }
    }
}
