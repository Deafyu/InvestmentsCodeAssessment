using InvestmentsCodeAssessment.Models;

namespace InvestmentsCodeAssessment.Services;

public interface IInvestmentsService
{
    public void BuyShares(int sharesAmount, int sharesPrice);

    public SharesSell SellShares(int sharesAmount, int sharesPrice);

    public Queue<Investment> GetAllShares();
    public decimal GetSharesBasis();
}