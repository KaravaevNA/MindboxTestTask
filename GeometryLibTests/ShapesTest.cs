using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeometryLib;

namespace GeometryLibTests
{
    [TestClass]
    public class CircleTests
    {
        [TestMethod]
        // Zero radius
        [DataRow(0f)]
        // Negative radius
        [DataRow(-1.1f)]
        public void CirlceCreation_InvalidRadius_ThrowsArgumentException(float radius)
        {
            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => new Circle(radius),
                $"Expected an ArgumentException for radius: {radius}");
        }

        [TestMethod]
        public void CirlceCreation_ValidRadius_CreatesCircleObject()
        {
            // Arrange
            float radius = 3.5f;

            // Act
            Circle circle = new Circle(radius);

            // Assert
            Assert.IsNotNull(circle);
            Assert.AreEqual(radius, circle.Radius);
        }

        [TestMethod]
        public void GetArea_ValidRadius_ReturnsCorrectArea()
        {
            // Arrange
            float radius = 5f;
            Circle circle = new Circle(radius);

            // Act
            float area = circle.GetArea();

            // Assert
            Assert.AreEqual(78.53982f, area, 0.00001f);
        }
    }

    [TestClass]
    public class TriangleTests
    {
        [TestMethod]
        // Zero side
        [DataRow(0f, 2.22f, 3.32f)]
        [DataRow(1.11f, 0f, 3.32f)]
        [DataRow(1.11f, 2.22f, 0f)]
        // Negative side
        [DataRow(-1.11f, 2.22f, 3.32f)]
        [DataRow(1.11f, -2.22f, 3.32f)]
        [DataRow(1.11f, 2.22f, -3.32f)]
        // Not triangle
        [DataRow(1.11f, 2.22f, 3.33f)]
        [DataRow(1.11f, 3.34f, 2.22f)]
        [DataRow(55.54f, 22.22f, 33.32f)]
        public void TriangleCreation_InvalidSides_ThrowsArgumentException(float a, float b, float c)
        {
            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => new Triangle(a, b, c),
                $"Expected an ArgumentException for sides: A: {a}, B: {b}, C: {c}");
        }

        [TestMethod]
        public void TriangleCreation_ValidSides_CreatesTriangleObject()
        {
            // Arrange
            float a = 3f;
            float b = 4f;
            float c = 5f;

            // Act
            Triangle triangle = new Triangle(a, b, c);

            // Assert
            Assert.IsNotNull(triangle);
            Assert.AreEqual((a, b, c), (triangle.A, triangle.B, triangle.C));
        }

        [TestMethod]
        public void GetArea_ValidSides_ReturnsCorrectArea()
        {
            // Arrange
            float a = 3f;
            float b = 4f;
            float c = 5f;
            Triangle triangle = new Triangle(a, b, c);

            // Act
            float area = triangle.GetArea();

            // Assert
            Assert.AreEqual(6f, area);
        }

        [TestMethod]
        public void IsRightTriangle_RightTriangle_ReturnsTrue()
        {
            // Arrange
            float a = 3;
            float b = 4;
            float c = 5;
            Triangle triangle = new Triangle(a, b, c);

            // Act
            bool isRightTriangle = triangle.IsRightTriangle();

            // Assert
            Assert.IsTrue(isRightTriangle);
        }

        [TestMethod]
        public void IsRightTriangle_NotRightTriangle_ReturnsFalse()
        {
            // Arrange
            float a = 2;
            float b = 3;
            float c = 4;
            Triangle triangle = new Triangle(a, b, c);

            // Act
            bool isRightTriangle = triangle.IsRightTriangle();

            // Assert
            Assert.IsFalse(isRightTriangle);
        }
    }
}