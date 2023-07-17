namespace UTAD.LAB._2022.TheGammingHour.Models
{
    public class ModelIndex
    {
        public List<JogoInfo> JogosInfo { get; set; } = new List<JogoInfo>();
        public List<Categoria> Categorias { get; set; } = new List<Categoria>();
        
        public ModelIndex()
        {
            JogosInfo = new List<JogoInfo>();
            Categorias = new List<Categoria>();
        }
        public ModelIndex(List<JogoInfo> jogosInfo, List<Categoria> categorias)
        {
            JogosInfo = jogosInfo;
            Categorias = categorias;
        }
    }
}
