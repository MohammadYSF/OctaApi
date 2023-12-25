namespace OctaApi.Application.DomainModels;

public sealed record InventoryItemDTO(int RowNumber, string Code, string Title, float? Count, float? Limit, long? BuyPrice, long? SellPrice , Guid Id);
