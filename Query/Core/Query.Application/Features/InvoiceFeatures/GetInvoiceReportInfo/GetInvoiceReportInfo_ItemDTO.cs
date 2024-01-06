namespace OctaApi.Application.Features.InvoiceFeatures.GetInvoiceReportInfo
{
    public sealed record GetInvoiceReportInfo_ItemDTO(string RowNumber, string Name, string Count, string UnitPrice, string TotalPrice);
}
