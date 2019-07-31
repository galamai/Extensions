using System;
using System.Threading.Tasks;
using Xunit;

namespace Galamai.Extensions.Context.Tests
{
    public class OperationContextTests
    {
        [Fact]
        public async Task TestSetAndGet()
        {
            var name = "name";
            var value = "value";

            var result = await AsyncMethod(name, value);

            Assert.Equal(value, result);
        }

        [Fact]
        public async Task TestSetAndGetOnManyTasks()
        {
            var name = "name";
            var value1 = "value1";
            var value2 = "value2";

            var task1 = AsyncMethod(name, value1);
            var task2 = AsyncMethod(name, value2);

            await Task.WhenAll(task1, task2);

            Assert.Equal(value1, task1.Result);
            Assert.Equal(value2, task2.Result);
        }

        [Fact]
        public async Task TestReplaceOnIncludedContext()
        {
            var name = "name";
            var value1 = "value1";
            var value2 = "value2";

            var result = await Task.Run(async () =>
            {
                using (OperationContext.Set(name, value1))
                {
                    return await AsyncMethod(name, value2);
                }
            });

            Assert.Equal(value2, result);
        }

        [Fact]
        public void TestGetDefaultValue()
        {
            var result = OperationContext.Get<string>("test");

            Assert.Null(result);

            var result2 = OperationContext.Get<int>("test");

            Assert.Equal(0, result2);
        }

        private async Task<string> AsyncMethod(string name, string value)
        {
            using (OperationContext.Set(name, value))
            {
                return await Task.Run(() => OperationContext.Get<string>(name));
            }
        }
    }
}
