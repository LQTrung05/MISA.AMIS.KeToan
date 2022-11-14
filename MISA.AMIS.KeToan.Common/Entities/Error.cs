namespace MISA.AMIS.KeToan.Common.Entities
{
    public class Error
    {
        /// <summary>
        /// Mã lỗi 
        /// </summary>
        //public int ErrorCode {
        //   get; set; 
        //}
        /// <summary>
        /// Thông báo lỗi cho bên dev
        /// </summary>
        public string? DevMsg { get; set; }

        /// <summary>
        /// Thông báo lỗi cho phía người dùng
        /// </summary>
        public string? UsersMsg { get; set; }

        /// <summary>
        /// Thông tin chi tiết về lỗi
        /// </summary>
        public string? MoreInfo { get; set; }
        public object Data { get; set; }

        /// <summary>
        /// Mã để tra cứu thông tin log
        /// </summary>
        public Guid TraceId { get; set; }
    }
}
