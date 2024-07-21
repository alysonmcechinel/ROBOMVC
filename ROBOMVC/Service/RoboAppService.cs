using ROBOMVC.Models;

namespace ROBOMVC.Service;

public class RoboAppService
{
    public RoboViewModel EstadoIncialRobo() =>
        new RoboViewModel
        {
            Cabeca = new CabecaViewModel { Rotacao = RotacaoCabeca.EmRepouso, Inclinacao = InclinacaoCabeca.EmRepouso },
            BracoEsquerdo = new BracoViewModel { Cotovelo = Cotovelo.EmRepouso, Pulso = Pulso.EmRepouso },
            BracoDireito = new BracoViewModel { Cotovelo = Cotovelo.EmRepouso, Pulso = Pulso.EmRepouso }
        };
    

    public string ValidarMovimento(RoboViewModel estadoAtual, RoboViewModel viewModel)
    {
        // Verificar se o Pulso pode ser movimentado
        if (viewModel.BracoEsquerdo.Pulso != estadoAtual.BracoEsquerdo.Pulso ||
            viewModel.BracoDireito.Pulso != estadoAtual.BracoDireito.Pulso)
        {
            if (viewModel.BracoEsquerdo.Cotovelo != Cotovelo.FortementeContraido ||
                viewModel.BracoDireito.Cotovelo != Cotovelo.FortementeContraido)
            {
                return "O Pulso só pode ser movimentado caso o Cotovelo esteja Fortemente Contraído.";
            }
        }

        // Verificar se a Cabeça pode ser rotacionada
        if (viewModel.Cabeca.Rotacao != estadoAtual.Cabeca.Rotacao &&
            viewModel.Cabeca.Inclinacao == InclinacaoCabeca.ParaBaixo)
        {
            return "A Cabeça não pode ser rotacionada se a Inclinação estiver Para Baixo.";
        }

        // Verificar a progressão dos estados
        if (!VerificarProgressoDeEstados(estadoAtual, viewModel))
        {
            return "A progressão dos estados deve ser crescente ou decrescente, sem pular estados.";
        }

        return null;
    }

    // Privates

    private bool VerificarProgressoDeEstados(RoboViewModel estadoAtual, RoboViewModel novoEstado)
    {
        return VerificarProgressaoDeEstado(estadoAtual.Cabeca.Rotacao, novoEstado.Cabeca.Rotacao) &&
               VerificarProgressaoDeEstado(estadoAtual.Cabeca.Inclinacao, novoEstado.Cabeca.Inclinacao) &&
               VerificarProgressaoDeEstado(estadoAtual.BracoEsquerdo.Cotovelo, novoEstado.BracoEsquerdo.Cotovelo) &&
               VerificarProgressaoDeEstado(estadoAtual.BracoEsquerdo.Pulso, novoEstado.BracoEsquerdo.Pulso) &&
               VerificarProgressaoDeEstado(estadoAtual.BracoDireito.Cotovelo, novoEstado.BracoDireito.Cotovelo) &&
               VerificarProgressaoDeEstado(estadoAtual.BracoDireito.Pulso, novoEstado.BracoDireito.Pulso);
    }

    private bool VerificarProgressaoDeEstado<T>(T estadoAtual, T novoEstado) where T : Enum
    {
        var estados = Enum.GetValues(typeof(T)).Cast<T>().ToList();
        var indexAtual = estados.IndexOf(estadoAtual);
        var indexNovo = estados.IndexOf(novoEstado);

        return Math.Abs(indexAtual - indexNovo) <= 1;
    }

}

