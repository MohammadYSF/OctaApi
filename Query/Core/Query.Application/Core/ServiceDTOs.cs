namespace Query.Application.Core;

public sealed record ServiceDTO(int RowNumber, string Code, string Title, long Price, Guid Id);
