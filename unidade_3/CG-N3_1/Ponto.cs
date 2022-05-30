/**
  Autor: Dalton Solano dos Reis
**/

using OpenTK.Graphics.OpenGL;
using CG_Biblioteca;

namespace gcgcg
{
  internal class Ponto : ObjetoGeometria
  {
    public Ponto(char rotulo, Objeto paiRef, Ponto4D ponto) : base(rotulo, paiRef)
    {
      base.PontosAdicionar(ponto);
    }

    public void atualizar(Ponto4D ponto) {
      base.pontosLista.Clear();
      base.PontosAdicionar(ponto);
    }

    protected override void DesenharObjeto()
    {
      GL.PointSize(10);
      GL.Begin(PrimitiveType.Points);
      foreach (Ponto4D pto in pontosLista)
      {
        GL.Vertex2(pto.X, pto.Y);
      }
      GL.End();
    }
    
    //TODO: melhorar para exibir não só a lista de pontos (geometria), mas também a topologia ... poderia ser listado estilo OBJ da Wavefrom
    public override string ToString()
    {
      string retorno;
      retorno = "__ Objeto Retangulo: " + base.rotulo + "\n";
      for (var i = 0; i < pontosLista.Count; i++)
      {
        retorno += "P" + i + "[" + pontosLista[i].X + "," + pontosLista[i].Y + "," + pontosLista[i].Z + "," + pontosLista[i].W + "]" + "\n";
      }
      return (retorno);
    }

  }
}
