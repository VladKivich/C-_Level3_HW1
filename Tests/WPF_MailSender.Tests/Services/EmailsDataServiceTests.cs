using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WPF_MailSender.Services;
using WPF_MailSender.Models;

namespace WPF_MailSender.Tests.Services
{
    //Экземпляр класса модульного теста создаеться на каждый из методов теста!
    [TestClass]
    public class EmailsDataServiceTests
    {
        /// <summary>
        /// Будет выполнен в начале каждого теста
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {

        }

        /// <summary>
        /// Будет выполнен при завершении теста
        /// </summary>
        [TestCleanup]
        public void TestClear()
        {

        }

        /// <summary>
        /// Метод запускаеться сразу после создания класса
        /// </summary>
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {

        }

        [TestMethod]
        public void Creation()
        {
            var EmailsDataService = new EmailsDataService();
        }

        [TestMethod]
        public void AddNewEmailMessage_NewID()
        {
            //Arrange - Размещение предпологаемых данных
            var EmailsDataService = new EmailsDataService();
            var EmailMessage = new EmailMessage();
            var Exp_ID = 1;
            var Exp_Count = 1;

            //Action - Дейсвтие над данными
            EmailsDataService.Add(EmailMessage);
            var Email_DB = EmailsDataService.GetById(1);
            var Act_ID = Email_DB.ID;
            var Act_Count = EmailsDataService.GetAll().Count;

            //Assert - Заявление(Утверждение) правильности данных
            Assert.AreEqual(Exp_ID, Act_ID);
            Assert.AreEqual(Exp_Count, Act_Count);
        }
    }
}
