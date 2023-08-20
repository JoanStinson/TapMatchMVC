using NUnit.Framework;
using JGM.Game;

namespace JGM.GameTests
{
    public class CoordinateTest
    {
        [Test]
        public void When_CoordinatesAreEqual_Expect_EqualsMethodReturnsTrue()
        {
            Coordinate coord1 = new Coordinate(1, 2);
            Coordinate coord2 = new Coordinate(1, 2);

            bool areEqual = coord1.Equals(coord2);

            Assert.IsTrue(areEqual);
        }

        [Test]
        public void When_CoordinatesAreNotEqual_Expect_EqualsMethodReturnsFalse()
        {
            Coordinate coord1 = new Coordinate(1, 2);
            Coordinate coord2 = new Coordinate(3, 4);

            bool areEqual = coord1.Equals(coord2);

            Assert.IsFalse(areEqual);
        }

        [Test]
        public void When_CoordinatesAreEqual_Expect_HashCodesAreEqual()
        {
            Coordinate coord1 = new Coordinate(1, 2);
            Coordinate coord2 = new Coordinate(1, 2);

            int hashCode1 = coord1.GetHashCode();
            int hashCode2 = coord2.GetHashCode();

            Assert.AreEqual(hashCode1, hashCode2);
        }

        [Test]
        public void When_CoordinatesAreNotEqual_Expect_HashCodesAreDifferent()
        {
            Coordinate coord1 = new Coordinate(1, 2);
            Coordinate coord2 = new Coordinate(3, 4);

            int hashCode1 = coord1.GetHashCode();
            int hashCode2 = coord2.GetHashCode();

            Assert.AreNotEqual(hashCode1, hashCode2);
        }

        [Test]
        public void When_VisitedPropertySet_Expect_VisitedPropertyCanBeAccessed()
        {
            Coordinate coordinate = new Coordinate(1, 2);

            coordinate.visited = true;
            bool isVisited = coordinate.visited;

            Assert.IsTrue(isVisited);
        }
    }
}