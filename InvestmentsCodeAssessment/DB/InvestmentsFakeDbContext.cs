using InvestmentsCodeAssessment.Models;

namespace InvestmentsCodeAssessment.DB;

public class InvestmentsFakeDbContext
{
    public Queue<Investment> Investments = new(new[]
    {
        new Investment(100, 20),
        new Investment(150, 30),
        new Investment(120, 10)
    });
}