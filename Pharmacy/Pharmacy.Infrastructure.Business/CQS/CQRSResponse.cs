namespace Pharmacy.Infrastructure.Business.CQS
{
    public class CQRSResponse<T>: ICQRSResponse
    {
        public T? Model { get; set; } = default;
        public string Message { get; set; } = string.Empty;
        public bool IsSuccess { get; set; } = false;
    }

    public interface ICQRSResponse { }
}
