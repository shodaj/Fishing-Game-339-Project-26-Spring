using Game339.Shared.Services.Implementation;
using NUnit.Framework;

namespace Game339.Tests;

public class GameParametersTests
{
    
    [Test]
    public void GetSpeed_ReturnsFive()
    {
        // Act & Assert
        Assert.That(GameParameters.GetSpeed(), Is.EqualTo(5));
    }
    
    [Test]
    public void GetSpeed_DoesntReturnFive()
    {
        // Act & Assert
        Assert.That(GameParameters.GetSpeed(), !Is.EqualTo(2));
    }
    
    [Test]
    public void GetSize_ReturnsEight()
    {
        // Act & Assert
        Assert.That(GameParameters.GetSize(), Is.EqualTo(8));
    }
}