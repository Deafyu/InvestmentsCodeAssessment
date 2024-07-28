using InvestmentsCodeAssessment.Models;

namespace InvestmentsCodeAssessment.DB;

public class InvestmentsRepository
{
    private InvestmentsFakeDbContext _context;

    public InvestmentsRepository(InvestmentsFakeDbContext investmentsFakeDbContext)
    {
        _context = investmentsFakeDbContext ?? throw new ArgumentNullException(nameof(investmentsFakeDbContext));
    }

    public void AddShares(int sharesAmount, int sharePrice)
    {
        _context.Investments.Enqueue(new Investment(sharesAmount, sharePrice));
    }

    public SharesSell RemoveShares(int soldAmount, int sellPrice)
    {
        var stocksSell = new SharesSell();
        int basis = 0;

        stocksSell.TotalProfit = Math.Round(CountSoldSharesValues(soldAmount, sellPrice, 0, ref basis), 2);
        ;
        stocksSell.SharesBasis = Math.Round((decimal)basis / soldAmount, 2);

        return stocksSell;
    }

    public Queue<Investment> GetAllShares()
    {
        return _context.Investments;
    }

    public decimal GetSharesBasis()
    {
        var stocksAmount = 0;
        var stocksBasis = 0;
        foreach (var investment in _context.Investments)
        {
            stocksBasis += investment.Amount * investment.PricePerShare;
            stocksAmount += investment.Amount;
        }

        return stocksAmount == 0 ? 0 : Math.Round((decimal)stocksBasis / stocksAmount, 2);
    }

    private decimal CountSoldSharesValues(int soldAmount, int sellPrice, int income, ref int basis)
    {
        if (_context.Investments.Count == 0)
            throw new InvalidOperationException("Cannot sell more shares than it is available.");
        var investment = _context.Investments.Peek();
        if (investment.Amount >= soldAmount)
        {
            income += (sellPrice - investment.PricePerShare) * soldAmount;
            basis += investment.PricePerShare * soldAmount;
            investment.Amount -= soldAmount;
            if (investment.Amount == 0)
            {
                _context.Investments.Dequeue();
            }
        }
        else if (investment.Amount < soldAmount)
        {
            income += (sellPrice - investment.PricePerShare) * investment.Amount;
            basis += investment.PricePerShare * investment.Amount;
            soldAmount -= investment.Amount;
            _context.Investments.Dequeue();
            return CountSoldSharesValues(soldAmount, sellPrice, income, ref basis);
        }

        return income;
    }
}