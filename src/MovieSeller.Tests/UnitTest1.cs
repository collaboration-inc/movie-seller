using NUnit.Framework;
using FluentAssertions;

namespace MovieSeller.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void PassingTest()
        {
            true.Should().BeTrue();
        }

        [Test]
        public void FailingTest()
        {
            true.Should().BeFalse();
        }
    }
}
