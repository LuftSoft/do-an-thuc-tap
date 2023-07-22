namespace phone_shop_server.Business.DTO.Phone
{
    public class PhoneFilterDto
    {
        public string? Search { get; set; }
        //true = thap->cao, false= cao->thap
        public bool? PriceSort { get; set; }
        public string? Brand { get; set; }
        public string? RAM { get; set; }
        public string? ROM { get; set; }
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
        //nhu cau khac
        //Choi game, sac nhanh, chup anh quay phim, pin trau
        public string? OtherBenefit { get; set; }
    }
}
