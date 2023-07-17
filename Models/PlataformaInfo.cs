
namespace UTAD.LAB._2022.TheGammingHour.Models
{
    public class PlataformaInfo
    {
        public long? id;
        public Plataforma plataforma { get; set; }
        public IFormFile platFile { get; set; }

        public PlataformaInfo()
        {

        }
        public PlataformaInfo(Plataforma plataforma, IFormFile platFile)
        {
            this.plataforma = plataforma;
            this.platFile = platFile;
        }
    }
}
