using System.Runtime.Serialization;

namespace Store.S_02.Core.Entities.Order;

public enum OrderStatus
{
    [EnumMember(Value = "Pending")] Pending,
    [EnumMember(Value = "PaymentReceived")] PaymentReceived,
    [EnumMember(Value = "PaymentFailed")] PaymentFailed,
}