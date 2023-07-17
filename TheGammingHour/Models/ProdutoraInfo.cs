namespace UTAD.LAB._2022.TheGammingHour.Models
{
    public class ProdutoraInfo
    {
        public long? id;
        public Produtora produtora { get; set; }
        public IFormFile platFile { get; set; }

        public ProdutoraInfo()
        {

        }
        public ProdutoraInfo(Produtora produtora, IFormFile platFile)
        {
            this.produtora = produtora;
            this.platFile = platFile;
        }
    }
}
