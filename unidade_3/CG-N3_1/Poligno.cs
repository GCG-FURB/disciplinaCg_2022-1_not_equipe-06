using OpenTK.Graphics.OpenGL;
using CG_Biblioteca;

namespace gcgcg
{
    internal class Poligno : ObjetoGeometria
    {

        public Poligno(char rotulo, Objeto pai) : base(rotulo, pai)
        {

        }
        protected override void DesenharObjeto()
        {
            GL.PointSize(3);
            GL.Begin(base.PrimitivaTipo);
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
         public static double DistanciaEuclidiana(Ponto4D pontoLista, Ponto4D pontoMouse){
            double distancia = Math.Sqrt((Math.Pow(pontoLista.X - pontoMouse.X, 2) + Math.Pow(pontoLista.Y - pontoMouse.Y, 2)));
            return distancia;
        }

        public int VerticeMaisProximo(List<Ponto4D> pontosLista, Ponto4D pontoMouse){
            double menorDistancia = 600;
            double d = 0;
            int posicaoMenorDistancia = 0;
            for (int i = 0; i < pontosLista.Count; i++){
                d = DistanciaEuclidiana(pontosLista[i], pontoMouse);
                if(d < menorDistancia){
                menorDistancia = d;
                posicaoMenorDistancia = i;
                }
            }
            return posicaoMenorDistancia;
        }
    }
}