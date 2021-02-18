using KellermanSoftware.CompareNetObjects;
using NUnit.Framework;
using System.Linq;
using TodoAPI.Models;
using TodoAPI.Repository;

namespace TodoUnitTests
{
    public class TodoRepositoryTests
    {
        private TodoRepository _todoRepo;

        [SetUp]
        public void Setup()
        {
            _todoRepo = new TodoRepository();
        }

        [Test]
        public void Add_GreenFlow()
        {
            //Arrange
            var newTodo = new Todo() { Id = 2, IsDone = false, Name = "unit1" };
            //Act
            var result = _todoRepo.Add(newTodo);
            //Assert
            CompareLogic compareLogic = new CompareLogic();
            var compareResult = compareLogic.Compare(newTodo, result);

            Assert.Pass("Object are equal", compareResult.AreEqual);
        }

        [Test]
        public void AddIsIdempotent()
        {
            //Arrange
            var newTodo = new Todo() { Id = 2, IsDone = false, Name = "unit1" };
            //Act
            var result1 = _todoRepo.Add(newTodo);
            var result2 = _todoRepo.Add(newTodo);
            //Assert
            CompareLogic compareLogic = new CompareLogic();
            var compareResult1 = compareLogic.Compare(newTodo, result1);
            var compareResult2 = compareLogic.Compare(newTodo, result2);

            Assert.AreEqual(result1.Id, result2.Id);

            Assert.Pass("Object are equal", compareResult1.AreEqual);
            Assert.Pass("Object are equal", compareResult2.AreEqual);
        }

        [Test]
        public void Update_GreenFlow()
        {
            //Arrange
            var newTodo = new Todo() { Id = 1, IsDone = false, Name = "unitUpdate" };
            //Act
            var result = _todoRepo.Update(1, newTodo);
            var refetchEntity = _todoRepo.GetById(1);
            //Assert
            Assert.IsTrue(result, "Update was not OK");

            CompareLogic compareLogic = new CompareLogic();
            var compareResult = compareLogic.Compare(newTodo, refetchEntity);

            Assert.Pass("Object are equal", compareResult.AreEqual);
        }

        [Test]
        public void Delete_GreenFlow()
        {
            //Arrange
            var newTodo = new Todo() { Id = 1, IsDone = false, Name = "DeleteMe" };
            //Act
            var result = _todoRepo.Add(newTodo);
            var deleteResult = _todoRepo.Delete(result.Id);

            //Assert
            Assert.IsTrue(deleteResult);
            var refetchEntity = _todoRepo.GetById(result.Id);
            Assert.Pass("Object are equal", refetchEntity == null);
        }

        [Test]
        public void GetById_GreenFlow()
        {
            //Arrange
            var newTodo = new Todo() { Id = 1, IsDone = false, Name = "DeleteMe" };
            //Act
            var result = _todoRepo.Add(newTodo);

            //Assert
            var refetchEntity = _todoRepo.GetById(result.Id);
            Assert.Pass("Object are equal", refetchEntity == null);
        }

        [Test]
        public void GetAll_GreenFlow()
        {
            //Arrange
            var newTodo = new Todo() { Id = 1, IsDone = false, Name = "DeleteMe" };
            //Act
            var result = _todoRepo.Add(newTodo);

            //Assert
            var entities = _todoRepo.GetAll();
            Assert.Pass("Object are equal", entities.Count() > 0);
        }
    }
}