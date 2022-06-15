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
              PontosRemoverAt(posicaoMenorDistancia);
              PontosAdicionar(pontoMouse);
            }
    
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