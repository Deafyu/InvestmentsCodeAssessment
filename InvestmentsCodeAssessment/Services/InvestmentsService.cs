using InvestmentsCodeAssessment.DB;
using InvestmentsCodeAssessment.Models;

namespace InvestmentsCodeAssessment.Services;

public class InvestmentsService : IInvestmentsService
{
    public InvestmentsRepository _investmentsRepository;

    public InvestmentsService(InvestmentsRepository investmentsRepository)
    {
        _investmentsRepository = investmentsRepository;
    }

    public void BuyShares(int sharesAmount, int sharesPrice)
    {
        _investmentsRepository.AddShares(sharesAmount, sharesPrice);
    }
    
    public SharesSell SellShares(int sharesAmount, int sharesPrice)
    {
        return _investmentsRepository.RemoveShares(sharesAmount, sharesPrice);
    }

    public Queue<Investment> GetAllShares()
    {
        return _investmentsRepository.GetAllShares();
    }
    
    public decimal GetSharesBasis()
    {
        return _investmentsRepository.GetSharesBasis();
    }
}