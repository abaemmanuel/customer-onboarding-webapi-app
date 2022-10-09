using CustomerOnboarding.ApplicationService.Dtos;
using CustomerOnboarding.ApplicationService.Services.Implementations;
using CustomerOnboarding.ApplicationService.Services.Interfaces;
using CustomerOnboarding.Core.Entities;
using CustomerOnboarding.Repositories.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerOnboarding.ApplicationService.UnitTest.Services.Implementations
{
    [TestFixture]
    public class OnboardCustomerAppServiceTests
    {
        private MockRepository mockRepository;

        private Mock<IOtpService> mockOtpService;
        private Mock<IRepository<Customer>> mockRepositoryCustomer;
        private Mock<IRepository<OnboardingStatus>> mockRepositoryOnboardingStatus;
        private Mock<IRepository<LocalGovernmentArea>> mockRepositoryLocalGovernmentArea;
        private Mock<IRepository<State>> mockRepositoryState;
        private Mock<IPasswordHasher> mockPasswordHasher;

        [SetUp]
        public void SetUp()
        {
            this.mockRepository = new MockRepository(MockBehavior.Default);

            this.mockOtpService = this.mockRepository.Create<IOtpService>();
            this.mockRepositoryCustomer = this.mockRepository.Create<IRepository<Customer>>();
            this.mockRepositoryOnboardingStatus = this.mockRepository.Create<IRepository<OnboardingStatus>>();
            this.mockRepositoryLocalGovernmentArea = this.mockRepository.Create<IRepository<LocalGovernmentArea>>();
            this.mockRepositoryState = this.mockRepository.Create<IRepository<State>>();
            this.mockPasswordHasher = this.mockRepository.Create<IPasswordHasher>();
        }

        private OnboardCustomerAppService CreateService()
        {
            return new OnboardCustomerAppService(
                this.mockOtpService.Object,
                this.mockRepositoryCustomer.Object,
                this.mockRepositoryOnboardingStatus.Object,
                this.mockRepositoryLocalGovernmentArea.Object,
                this.mockRepositoryState.Object,
                this.mockPasswordHasher.Object);
        }

        [Test]
        public async Task OnboardCustomer_CustomerDto_Has_All_Fields_Valid_And_LGA_Maps_Well_To_State_Returns_True()
        {
            var service = this.CreateService();

            
            CustomerDto customerDto = new CustomerDto 
            {
                Password = "Kunle@45",
                PhoneNumber = "08062619188",
                Email = "kunle_edun@gmail.com",
                LGA = "Ikeja",
                StateOfResidence = "Lagos`"
            };

            #region Mock Customer

            List<Customer> custList = new List<Customer>();

            IQueryable<Customer> customerFromDbAsQueryable = Queryable.AsQueryable(custList);

            this.mockRepositoryCustomer.Setup(c => c.GetByWhere(cr => cr.Email == customerDto.Email))
                .ReturnsAsync(customerFromDbAsQueryable);

            Customer customerFromDb = customerFromDbAsQueryable.SingleOrDefault();


            #endregion

            #region Mock Otp
            this.mockOtpService
                .Setup(e => e.SendOTP(customerDto.PhoneNumber))
                .ReturnsAsync(true);

            this.mockOtpService
                .Setup(e => e.VerifiedOTP(customerDto.PhoneNumber))
                .ReturnsAsync(true);
            #endregion

            #region Mock State
            State state = new State { Id = 1, Name = "Lagos" };
            
            List<State> stateList = new List<State> { state };
            IQueryable<State> stateFromDbAsQueryable = Queryable.AsQueryable(stateList);

            this.mockRepositoryState
            .Setup(s => s.GetByWhere(x => x.Name == customerDto.StateOfResidence))
            .ReturnsAsync(stateFromDbAsQueryable);

            State stateFromDb = stateFromDbAsQueryable.SingleOrDefault();

            #endregion

            #region Mock LGA
            LocalGovernmentArea lga = new LocalGovernmentArea 
            { Id = 1, Lga = "Agege", StateId = 1 };

            List<LocalGovernmentArea> lgaList = new List<LocalGovernmentArea> { lga };
            IQueryable<LocalGovernmentArea> lgaFromDbAsQueryable =
                                        Queryable.AsQueryable(lgaList);

            var stateId = stateFromDb.Id;

            this.mockRepositoryLocalGovernmentArea
            .Setup(l => l.GetByWhere(x => x.StateId == stateId && x.Lga == customerDto.LGA))
            .ReturnsAsync(lgaFromDbAsQueryable);

            LocalGovernmentArea lgaMappedToState = lgaFromDbAsQueryable.SingleOrDefault();

            #endregion

            var result = await service.OnboardCustomer(
                customerDto);

            Assert.That(customerDto.StateOfResidence == stateFromDb.Name);
            Assert.That(customerDto.LGA == lgaMappedToState.Lga);
            this.mockRepository.VerifyAll();
        }
    }
}
