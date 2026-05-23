using BiddingService.Common.Enums;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace BiddingService.Common.Serializers;

public class BidStatusSerializer : SerializerBase<BidStatus>
{
    public override BidStatus Deserialize(
        BsonDeserializationContext context,
        BsonDeserializationArgs args)
    {
        var value = context.Reader.ReadInt32();
        
        return BidStatus.FromValue(value);
    }

    public override void Serialize(
        BsonSerializationContext context, 
        BsonSerializationArgs args, 
        BidStatus value)
    {
        context.Writer.WriteInt32(value.Value);
    }
}