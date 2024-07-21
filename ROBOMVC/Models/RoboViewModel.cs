namespace ROBOMVC.Models
{
    public class RoboViewModel
    {
        public CabecaViewModel Cabeca { get; set; }
        public BracoViewModel BracoEsquerdo { get; set; }
        public BracoViewModel BracoDireito { get; set; }
    }

    public class CabecaViewModel
    {
        public RotacaoCabeca Rotacao { get; set; }
        public InclinacaoCabeca Inclinacao { get; set; }
    }

    public class BracoViewModel
    {
        public Cotovelo Cotovelo { get; set; }
        public Pulso Pulso { get; set; }
    }

    public enum Cotovelo
    {
        EmRepouso,
        LevementeContraido,
        Contraido,
        FortementeContraido
    }

    public enum Pulso
    {
        RotacaoParaMenos90,
        RotacaoParaMenos45,
        EmRepouso,
        RotacaoPara45,
        RotacaoPara90,
        RotacaoPara135,
        RotacaoPara180
    }

    public enum RotacaoCabeca
    {
        RotacaoMenos90,
        RotacaoMenos45,
        EmRepouso,
        RotacaoPara45,
        RotacaoPara90
    }

    public enum InclinacaoCabeca
    {
        ParaCima,
        EmRepouso,
        ParaBaixo
    }

}
