namespace Pharmacy.Infrastructure.Business.CQS
{
    public class CQRSResponse<T>
    {
        public T? Model { get; set; } = default;
        public string Message { get; set; } = string.Empty;
        public bool IsSuccess { get; set; } = false;
    }
}
