using OpenTK.Graphics.OpenGL;
using CG_Biblioteca;

namespace gcgcg
{
    internal class Circulo : ObjetoGeometria
    {

        public Circulo(char rotulo, Objeto pai, Ponto4D ptoCentro, long raio) : base(rotulo, pai)
        {
            Ponto4D ponto = new Ponto4D();
            for (double i = ptoCentro.X; i < ptoCentro.X + 360; i += 5) {
                ponto = Matematica.GerarPtosCirculo(i, raio);
                ponto.X += ptoCentro.X;
                ponto.Y += ptoCentro.Y;
                base.PontosAdicionar(ponto);  
            }
        }
        protected override void DesenharObjeto()
        {
            GL.PointSize(3);
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
            return null;
        }
    }
}