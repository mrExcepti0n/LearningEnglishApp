using System;
using System.Linq;
using System.Threading.Tasks;
using Data.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VocabularyApi.Controllers;
using VocabularyApi.Dtos.Training;
using VocabularyApi.Infrastructure.DataAccess;
using VocabularyApi.IntegrationTests.Builders;
using VocabularyApi.Models;
using VocabularyApi.Services;

namespace VocabularyApi.IntegrationTests
{
    [TestClass]
    public class TrainingControllerTest
    {
        [TestMethod]
        public async Task GetTrainingQuestionsAsync_ChooseTranslateInMemoryDb_ReturnsWords()
        {
            var userId = Guid.NewGuid();
            var identityServiceMock = new Mock<IIdentityService>();

            identityServiceMock.Setup(s => s.GetUserIdentity())
                                .Returns(userId);

            var trainingController = new TrainingController(GetInMemoryContext(userId), identityServiceMock.Object);
            var actionResult = await trainingController.GetTrainingQuestions(TrainingTypeEnum.ChooseTranslate, false);

            Assert.IsTrue(actionResult.Value.Any());
            Assert.IsTrue(actionResult.Value.First() is QuestionWithOptionsDto);
        }

        private VocabularyContext GetInMemoryContext(Guid userId)
        {
            var builder = new DbContextOptionsBuilder<VocabularyContext>();
            builder.UseInMemoryDatabase("Vocabulary");
            var options = builder.Options;

            using (var context = new VocabularyContext(options))
            {
                UserVocabulary userVocabulary = new UserVocabularyBuilder().WithStandardWords().WithUser(userId);
                context.UserVocabularies.Add(userVocabulary);
                context.SaveChanges();
            }

            return new VocabularyContext(options);
        }
    }
}
