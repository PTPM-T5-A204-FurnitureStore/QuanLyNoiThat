namespace WebsiteNoiThat.Models
{
    public class LoaiHang
    {

        string MaLoai;
        string TenLoai;
        string Anh;

        

        public string MaLoai1 { get => MaLoai; set => MaLoai = value; }
        public string TenLoai1 { get => TenLoai; set => TenLoai = value; }
        public string Anh1 { get => Anh; set => Anh = value; }

        public LoaiHang(string maLoai, string tenLoai, string anh)
        {
            MaLoai = maLoai;
            TenLoai = tenLoai;
            Anh = anh;
        }
    }
}
