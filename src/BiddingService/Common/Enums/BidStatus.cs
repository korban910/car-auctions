using Ardalis.SmartEnum;

namespace BiddingService.Common.Enums;

public class BidStatus(string name, int value, string html) : SmartEnum<BidStatus>(name, value)
{
    public static readonly BidStatus Unknown =  new BidStatus(nameof(Unknown), 0, "Unknown");
    public static readonly BidStatus Accepted =  new BidStatus(nameof(Accepted), 1, "Accepted");
    public static readonly BidStatus AcceptedBelowReserved =  new BidStatus(nameof(AcceptedBelowReserved), 2, "Accepted Below Reserved");
    public static readonly BidStatus TooLow =  new BidStatus(nameof(TooLow), 3, "Too Low");
    public static readonly BidStatus Finished =  new BidStatus(nameof(Finished), 4, "Finished");
    
    public string Html { get; set; } = html;
}