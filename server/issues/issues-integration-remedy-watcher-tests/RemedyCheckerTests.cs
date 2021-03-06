﻿using NUnit.Framework;
using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using CoE.Issues.Core.ServiceBus;
using Microsoft.Extensions.Options;
using AutoMapper;

namespace CoE.Issues.Remedy.Watcher.Tests
{
    public class RemedyCheckerTests
    {
        [SetUp]
        public void Setup()
        {
            Mock<IRemedyService> mockRemedyService = new Mock<IRemedyService>();
            remedyService = mockRemedyService.Object;

            Mock<IIssueMessageSender> issueMock = new Mock<IIssueMessageSender>();
            issueMessageSender = issueMock.Object;

            Mock<Serilog.ILogger> loggerMock = new Mock<Serilog.ILogger>();
            logger = loggerMock.Object;

            RemedyCheckerOptions options = new RemedyCheckerOptions()
            {
                ServiceUserName = "user",
                ServicePassword = "password",
                TemplateName = "",
                ApiUrl = "localhost/api",
                TempDirectory = "./Poll Files"
            };

            Mock<IOptions<RemedyCheckerOptions>> optionsMock = new Mock<IOptions<RemedyCheckerOptions>>();
            optionsMock.Setup(opt => opt.Value).Returns(options);
            remedyOptions = optionsMock.Object;

            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<RemedyIssueMappingProfile>();
            });
            mapper = Mapper.Instance;
        }

        private IRemedyService remedyService;
        private IIssueMessageSender issueMessageSender;
        private IMapper mapper;
        private Serilog.ILogger logger;
        private IOptions<RemedyCheckerOptions> remedyOptions;

        [Test]
        public void GetPollTimeTest()
        {
            //IRemedyChecker checker = new RemedyChecker(remedyService, issueMessageSender, mapper, logger, remedyOptions);
            DateTime actualTime = DateTimeOffset.Parse("2018-06-08T12:53:03-06:00").UtcDateTime;
            //DateTime recentFile = checker.TryReadLastPollTime();
           // recentFile.Should().Be(actualTime);
        }

        [Test]
        public void LogsRemedyServiceErrorsTest()
        {
            //IRemedyChecker checker = new RemedyChecker(remedyService, issueMessageSender, mapper, logger, remedyOptions);
            //RemedyPollResult result = await checker.PollAsync(DateTime.Now);
        }
    }
}