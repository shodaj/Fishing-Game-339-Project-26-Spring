using NUnit.Framework;
using Game339.Shared.Services.Implementation;

namespace Game339.Tests
{
    [TestFixture]
    public class PlayerInfoTests
    {
        private PlayerInfo _playerInfo;

        [SetUp]
        public void SetUp()
        {
            _playerInfo = new PlayerInfo();
        }

        [Test]
        public void GetName_DefaultValue_ReturnsNull()
        {
            Assert.That(_playerInfo.GetName(), Is.Null);
        }

        [Test]
        public void SetName_ValidName_NameIsUpdated()
        {
            _playerInfo.SetName("Larry");
            Assert.That(_playerInfo.GetName(), Is.EqualTo("Larry"));
        }

        [Test]
        public void SetName_EmptyString_NameIsEmpty()
        {
            _playerInfo.SetName("");
            Assert.That(_playerInfo.GetName(), Is.EqualTo(""));
        }

        [Test]
        public void SetName_NullValue_NameIsNull()
        {
            _playerInfo.SetName("Larry");
            _playerInfo.SetName(null);
            Assert.That(_playerInfo.GetName(), Is.Null);
        }

        [Test]
        public void SetName_CalledMultipleTimes_NameIsLastValue()
        {
            _playerInfo.SetName("Larry");
            _playerInfo.SetName("Jeff");
            Assert.That(_playerInfo.GetName(), Is.EqualTo("Jeff"));
        }
        
        [Test]
        public void SetAge_ValidAge_AgeIsSet()
        {
            _playerInfo.SetAge(20);
            Assert.That(_playerInfo.GetAge(), Is.EqualTo(20));
        }

        [Test]
        public void GetAge_DefaultValue_ReturnsZero()
        {
            Assert.That(_playerInfo.GetAge(), Is.EqualTo(0));
        }

        [Test]
        public void AgeOneYear_CalledOnce_AgeIsOne()
        {
            _playerInfo.AgeOneYear();
            Assert.That(_playerInfo.GetAge(), Is.EqualTo(1));
        }

        [Test]
        public void AgeOneYear_CalledMultipleTimes_AgeIncrementsCorrectly()
        {
            _playerInfo.AgeOneYear();
            _playerInfo.AgeOneYear();
            _playerInfo.AgeOneYear();
            Assert.That(_playerInfo.GetAge(), Is.EqualTo(3));
        }
    }
}