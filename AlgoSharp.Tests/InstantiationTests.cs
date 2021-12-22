using AlgoSharp.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AlgoSharp.Tests
{
    public class InstantiationTests
    {
        [Fact]
        public void CreatesNew()
        {
            var analyzer = new AlgoAnalyzer();
        }
    }
}