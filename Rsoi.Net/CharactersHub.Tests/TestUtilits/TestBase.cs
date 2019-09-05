using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace CharactersHub.Tests.TestUtilits
{
    public class TestBase
    {
        protected readonly Random random = new Random();
        private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        protected string GenerateRandomString()
        {
            return new string(Enumerable.Repeat(chars, random.Next(25)).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        protected TEnum GenerateRandomEnumValue<TEnum>(IEnumerable<TEnum> values = null, IEnumerable<TEnum> except = null) where TEnum : Enum
        {
            values = values ?? Enum.GetValues(typeof(TEnum)) as IEnumerable<TEnum>;
            if (except != null)
                values = values.Except(except);

            var index = random.Next(0, values.Count());
            return values.ElementAt(index);
        }

        protected List<T> GenerateRandomList<T>(int minLen, int maxLen, Func<T> objectFacory)
        {
            int length = random.Next(minLen, maxLen);
            var lst = new List<T>();
            for (int i = 0; i < length; i++)
            {
                lst.Add(objectFacory());
            }
            return lst;
        }

        protected void AssertEqualLists<T1, T2>(IEnumerable<T1> collection1, IEnumerable<T2> collection2, Action<T1, T2> assertFunction)
        {
            Assert.Equal(collection1.Count(), collection2.Count());
            for (int i = 0; i < collection1.Count(); i++)
            {
                assertFunction(collection1.ElementAt(i), collection2.ElementAt(i));
            }
        }
    }
}
