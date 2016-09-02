using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.async_await
{
    class MyClass
    {
        public int Get10()
        {
            return 10;
        }
        public async Task DoWorkAsync()
        {
            Func<int> ten = new Func<int>(Get10);
            int a = await Task.Run(ten);
            int b = await Task.Run(new Func<int>(Get10));
            int c = await Task.Run(() => { return 10; });
        }
    }
}
