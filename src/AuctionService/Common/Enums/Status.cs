using Ardalis.SmartEnum;

namespace AuctionService.Common.Enums;

public class Status(string name, int value, string html) : SmartEnum<Status>(name, value)
{
    public static readonly Status Unknown = new(nameof(Unknown), 0, "Unknown");
    public static readonly Status Live = new Status(nameof(Live), 1, "Live");
    public static readonly Status Finished = new Status(nameof(Finished), 2, "Finished");
    public static readonly Status ReserveNotMet = new Status(nameof(ReserveNotMet), 3, "Reserve Not Met");

    public string Html { get; private set; } = html;
}