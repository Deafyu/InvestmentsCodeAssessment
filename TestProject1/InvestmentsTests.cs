using InvestmentsCodeAssessment.DB;
using InvestmentsCodeAssessment.Models;

namespace TestProject1
{
    public class InvestmentsTests
    {
        [Fact]
        public void AddShares_ShouldAddSharesToContext()
        {
            //Arrange
            var fakeContext = new InvestmentsFakeDbContext();
            fakeContext.Investments.Clear();
            var repository = new InvestmentsRepository(fakeContext);
            int sharesAmount = 10;
            int sharePrice = 100;

            //Act
            repository.AddShares(sharesAmount, sharePrice);

            //Assert
            Assert.Single(fakeContext.Investments);
            var investment = fakeContext.Investments.Peek();
            Assert.Equal(sharesAmount, investment.Amount);
            Assert.Equal(sharePrice, investment.PricePerShare);
        }

        [Fact]
        public void RemoveShares_ShouldRemoveSharesAndReturnCorrectProfitAndBasis()
        {
            //Arrange
            var fakeContext = new InvestmentsFakeDbContext();
            fakeContext.Investments.Clear();
            var repository = new InvestmentsRepository(fakeContext);
            fakeContext.Investments.Enqueue(new Investment(10, 100));
            fakeContext.Investments.Enqueue(new Investment(5, 200));  

            int soldAmount = 10;
            int sellPrice = 300;

            //Act
            var result = repository.RemoveShares(soldAmount, sellPrice);

            //Assert
            Assert.Equal(2000, result.TotalProfit); 
            Assert.Equal(100, result.SharesBasis);
            
            Assert.Single(fakeContext.Investments);
            var remainingInvestment = fakeContext.Investments.Peek();
            Assert.Equal(5, remainingInvestment.Amount);
            Assert.Equal(200, remainingInvestment.PricePerShare);
        }

        [Fact]
        public void GetAllShares_ShouldReturnAllSharesInContext()
        {
            //Arrange
            var fakeContext = new InvestmentsFakeDbContext();
            fakeContext.Investments.Clear();
            var repository = new InvestmentsRepository(fakeContext);
            fakeContext.Investments.Enqueue(new Investment(10, 100));
            fakeContext.Investments.Enqueue(new Investment(5, 200));

            //Act
            var result = repository.GetAllShares();

            //Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void GetSharesBasis_ShouldReturnCorrectSharesBasis()
        {
            //Arrange
            var fakeContext = new InvestmentsFakeDbContext();
            fakeContext.Investments.Clear();
            var repository = new InvestmentsRepository(fakeContext);
            fakeContext.Investments.Enqueue(new Investment(10, 100));
            fakeContext.Investments.Enqueue(new Investment(5, 200));  

            //Act
            var result = repository.GetSharesBasis();

            //Assert
            Assert.Equal(133.33m, result);
        }

        [Fact]
        public void CountSoldSharesValues_ShouldThrowExceptionIfNotEnoughShares()
        {
            //Arrange
            var fakeContext = new InvestmentsFakeDbContext();
            fakeContext.Investments.Clear();
            var repository = new InvestmentsRepository(fakeContext);

            int soldAmount = 10;
            int sellPrice = 300;

            //Assert
            Assert.Throws<InvalidOperationException>(() => repository.RemoveShares(soldAmount, sellPrice));
        }
    }
}
