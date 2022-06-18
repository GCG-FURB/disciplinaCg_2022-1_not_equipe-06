/**
  Autor: Dalton Solano dos Reis
**/

using System.Collections.Generic;
using CG_Biblioteca;

namespace gcgcg
{
  internal abstract class ObjetoGeometria : Objeto
  {
    protected List<Ponto4D> pontosLista = new List<Ponto4D>();

    public ObjetoGeometria(char rotulo, Objeto paiRef) : base(rotulo, paiRef) { }

    public Ponto4D pontoAlterar = null;

    protected override void DesenharGeometria()
    {
      DesenharObjeto();
    }
    protected abstract void DesenharObjeto();
    public void PontosAdicionar(Ponto4D pto)
    {
      pontosLista.Add(pto);
      if (pontosLista.Count.Equals(1))
        base.BBox.Atribuir(pto);
      else
        base.BBox.Atualizar(pto);
      base.BBox.ProcessarCentro();
    }

    public void PontosRemoverUltimo()
    {
      pontosLista.RemoveAt(pontosLista.Count - 1);
    }

    protected void PontosRemoverTodos()
    {
      pontosLista.Clear();
    }

    protected void PontosRemoverAt(int posicao){
      pontosLista.RemoveAt(posicao);
    }

    public Ponto4D PontosUltimo()
    {
      return pontosLista[pontosLista.Count - 1];
    }

    public void PontosAlterar(Ponto4D pto, int posicao)
    {
      pontosLista[posicao] = pto;
    }

    public void VerticeMaisProximo(Ponto4D pontoMouse, bool isRemove){
      double menorDistancia = Matematica.DistanciaEuclidiana(pontosLista[0], pontoMouse);
      double d = 0;
      int posicaoMenorDistancia = 0;
      for (int i = 1; i < pontosLista.Count; i++){
        d = Matematica.DistanciaEuclidiana(pontosLista[i], pontoMouse);
        if(d < menorDistancia){
          menorDistancia = d;
          posicaoMenorDistancia = i;
        }  
      }
      if(isRemove == true){
        PontosRemoverAt(posicaoMenorDistancia);
      } else {
        PontosAlterar(pontoMouse, posicaoMenorDistancia);
        pontoAlterar = pontosLista[posicaoMenorDistancia];
      }
    }

     public double ScanlineIntesec(double y, double y1, double y2){
      return (y - y1) / (y2-y1);
    }

    public double ScanlineCalcularXi(double x1, double x2, double t){
      return (x1 + (x2-x1) * t);
    }

    public bool ScanLine(Ponto4D pontoMouse){
      return ScanLine(pontoMouse, pontosLista);
    }

    public bool ScanLine(Ponto4D pontoMouse, List<Ponto4D> listaPontos){
      int numeroInterseccoes = 0;
      bool estaDentro = true;
      for (int i = 0; i < listaPontos.Count; i++){
        Ponto4D ponto1 = listaPontos[i];
        int num = (i+1)%listaPontos.Count;
        Ponto4D ponto2 = listaPontos[num];
        
        if(ponto1.Y == ponto2.Y){
          if(pontoMouse.Y == ponto1.Y && pontoMouse.X > Math.Min(ponto1.X, ponto2.X) && pontoMouse.X < Math.Max(ponto1.X, ponto2.X)){
            return estaDentro;
          }
        }

        double t = ScanlineIntesec(pontoMouse.Y, ponto1.Y, ponto2.Y);

        if(t >= 0 && t <= 1){
          double x = ScanlineCalcularXi(ponto1.X, ponto2.X, t);
          if(x > pontoMouse.X && pontoMouse.Y > Math.Min(ponto1.Y, ponto2.Y) && pontoMouse.Y < Math.Max(ponto1.Y, ponto2.Y)){
            numeroInterseccoes++;
          }
        }
      }
      return numeroInterseccoes%2==1;
    }

    public override string ToString()
    {
      string retorno;
      retorno = "__ Objeto: " + base.rotulo + "\n";
      for (var i = 0; i < pontosLista.Count; i++)
      {
        retorno += "P" + i + "[" + pontosLista[i].X + "," + pontosLista[i].Y + "," + pontosLista[i].Z + "," + pontosLista[i].W + "]" + "\n";
      }
      return (retorno);
    }
  }
}