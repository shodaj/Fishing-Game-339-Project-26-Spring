using Game339.Shared.Services.Implementation;
using NUnit.Framework;

namespace Game339.Tests;

public class ExampleUnitTests
{
    // define any class variables you need here
    // ex: private StringService _svc;

    [SetUp]
    public void SetUp()
    {
        // instantiate any variables here that you'll need for the whole class
        // ex: _svc = new StringService(EmptyGameLog.Instance);
    }

    // example testcase (the type of test where you can give it a bunch of cases all at once)
    /*TestCase("hello", "olleh")]
    [TestCase("", "")]
    [TestCase("a", "a")]
    [TestCase("racecar", "racecar")]
    public void Reverse_ReturnsExpectedString(string input, string expected)
    {
        // Act
        var result = _svc.Reverse(input);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }*/

    // example test type of unit test where you dont pass in params
    /*[Test]
    public void Reverse_NullString_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<System.ArgumentNullException>(() => _svc.Reverse(null));
    }*/
}