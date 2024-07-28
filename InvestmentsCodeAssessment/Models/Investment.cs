namespace InvestmentsCodeAssessment.Models;

public class Investment

{
    public int Amount { get; set; }
    public int PricePerShare { get; set; }

    public Investment(int sharesAmount, int sharePrice)
    {
        Amount = sharesAmount;
        PricePerShare = sharePrice;
    }
}