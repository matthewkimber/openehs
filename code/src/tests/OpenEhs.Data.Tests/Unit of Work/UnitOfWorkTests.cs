using System.Reflection;
using Moq;
using NUnit.Framework;

namespace OpenEhs.Data.Tests.Unit_of_Work
{
    [TestFixture]
    public class UnitOfWorkTests
    {
        private IUnitOfWorkFactory _factory;
        private IUnitOfWork _unitOfWork;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CanStartUnitOfWork()
        {
            var mockFactory = new Mock<IUnitOfWorkFactory>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockFactory.Setup(factory => factory.Create()).Returns(mockUnitOfWork.Object);

            var fieldInfo = typeof (UnitOfWork).GetField("_unitOfWorkFactory",
                                                         BindingFlags.Static | BindingFlags.SetField |
                                                         BindingFlags.NonPublic);
            fieldInfo.SetValue(null, mockFactory.Object);

            var uow = UnitOfWork.Start();
        }
    }
}
